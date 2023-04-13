using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using D.Unity3dTools;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using UnityEditor;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using D.Unity3dTools.EditorTool;
using LitJson;

public class ShapeMapManager : MonoBehaviour
{
    public Transform mapGrid;
    public int mapSize = 16;

    public Transform gridRoot;
    public Transform colorRoot;
    public Transform depthRoot;
    public Transform baseColorPrefab;
    public Transform colorToggle;
    public Transform depthToggle;

    public SGridData currGridData = new SGridData();
    public static ShapeMapManager instantiate = null;
    private List<SGridData> gridDataList = new List<SGridData>();
    private MapGrid[][] map;

    private void Awake()
    {
        if (instantiate == null) instantiate = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitGridRoot();
        InitColorRoot();
        InitDepthRoot();
        InitCurrGridData();
    }
    public void OnClickLoadDefaultShape(string shapeName) 
    {
        ShapeMapData mapData = null;
        string jsonSavePath = Application.dataPath + "/" + shapeName + ".json";
        string json = File.ReadAllText(jsonSavePath);
        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ShapeMapData));
        using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            mapData = (ShapeMapData)jsonSerializer.ReadObject(stream);
        }
    }
    public void OnClickSaveShape(string shapeName) 
    {
        ShapeMapData mapData = new ShapeMapData();
        mapData.shapeName = shapeName;

        SMapGridData[][] saveMap = new SMapGridData[mapSize][];
        for (int i = 0; i < mapSize; i++) saveMap[i] = new SMapGridData[mapSize];
        for (int i = 0; i < Mathf.Pow(mapSize, 2); i++)
        {
            int row = i / 16;
            int col = i % 16;
            saveMap[col][row].row = row;
            saveMap[col][row].col = col;
            saveMap[col][row].gridData = map[col][row].gridData;
        }
        mapData.map = saveMap;

        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ShapeMapData));
        string json = "";
        using (MemoryStream stream = new System.IO.MemoryStream())
        {
            jsonSerializer.WriteObject(stream, mapData);
            json = Encoding.UTF8.GetString(stream.ToArray());
        }
        //Debug.Log(json);

        string jsonSavePath = Application.dataPath + "/"+ shapeName + ".json";
        FileInfo saveInfo = new FileInfo(jsonSavePath);
        DirectoryInfo dir = saveInfo.Directory;
        if (!dir.Exists) dir.Create();
        byte[] decBytes = Encoding.UTF8.GetBytes(json);

        FileStream fileStream = saveInfo.Create();
        fileStream.Write(decBytes, 0, decBytes.Length);
        fileStream.Flush();
        fileStream.Close();
        AssetDatabase.Refresh();
    }
    public void OnClickCreatShape()
    {
        List<MapGrid> mapGridData = new List<MapGrid>();
        for (int i = 0; i < Mathf.Pow(mapSize, 2); i++)
        {
            int row = i / 16;
            int col = i % 16;

            if (map[col][row].gridData.depth == 0) continue;
            mapGridData.Add(map[col][row]);
        }

        List<SUnitCube> cubeList = new List<SUnitCube>();
        for (int i = 0; i < mapGridData.Count; i++)
        {
            int _index = i;
            MapGrid grid = mapGridData[_index];

            SShapeIndex _shapeIndex = new SShapeIndex();
            _shapeIndex.col = grid.col;
            _shapeIndex.row = grid.row;
            _shapeIndex.index = _index;

            int _depth = grid.gridData.depth;

            int _colorId = grid.gridData.colorId;
            //懒得写了，默认背面是8号颜色得了，反正知道我支持双面赋色就可以了
            SUnitCube unitCube = UnitShapeClass.GetUnitCube(_shapeIndex, _depth, _colorId, 8);
            cubeList.Add(unitCube);
        }
        SUnitCube _unitCube = new SUnitCube();
        Vector3[] vertices = new Vector3[cubeList.Count * _unitCube.vertexCount];
        int[] triangles = new int[cubeList.Count * _unitCube.triangleCount];
        Vector2[] uv = new Vector2[cubeList.Count * _unitCube.uvCount];
        List<SMeshData> meshDatas = new List<SMeshData>();

        foreach (SUnitCube unitCube in cubeList)
        {
            unitCube.vertices.CopyTo(vertices, _unitCube.vertexCount * unitCube.shapeIndex.index);
            unitCube.triangles.CopyTo(triangles, _unitCube.triangleCount * unitCube.shapeIndex.index);
            unitCube.uv.CopyTo(uv, _unitCube.uvCount * unitCube.shapeIndex.index);
            List<SMeshData> datas = unitCube.GetMeshDatas();
            foreach (SMeshData _data in datas) meshDatas.Add(_data);
        }

        GameObject newObj = new GameObject("Test");
        MeshFilter meshFilter = newObj.transform.GetOrAddComponent<MeshFilter>();
        MeshRenderer meshRenderer = newObj.transform.GetOrAddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        Dictionary<int, List<int>> trianglesDic = new Dictionary<int, List<int>>();
        foreach (SMeshData _data in meshDatas)
        {
            int materialId = _data.materialId;
            if (trianglesDic.ContainsKey(materialId))
            {
                foreach (int _index in _data.triangles)
                    trianglesDic[materialId].Add(_index);
            }
            else if (!trianglesDic.ContainsKey(materialId))
            {
                trianglesDic.Add(materialId, _data.triangles);
            }
        }
        Material[] materials = baseColorPrefab.GetComponentInChildren<MeshRenderer>().materials;
        Material[] newMaterials = new Material[trianglesDic.Count()];
        int mateIndex = 0;
        foreach (KeyValuePair<int, List<int>> keyValuePair in trianglesDic)
        {
            int mateId = keyValuePair.Key;
            Material material = materials[mateId];
            newMaterials[mateIndex] = material;
            mateIndex++;
        }
        meshRenderer.materials = newMaterials;
        mesh.subMeshCount = meshRenderer.materials.Length;
        int startIndex = 0;
        int endIndex = 0;
        int submesh = 0;
        foreach (KeyValuePair<int, List<int>> keyValuePair in trianglesDic)
        {
            int[] _triangles = keyValuePair.Value.ToArray();
            endIndex = _triangles.Length;
            mesh.SetTriangles(_triangles, submesh);
            mesh.SetSubMesh(submesh, new SubMeshDescriptor(startIndex, endIndex));
            startIndex += endIndex;
            submesh++;
        }
        meshFilter.mesh = mesh;
    }
    private void InitDepthRoot()
    {
        for (int i = 0; i < 8; i++)
        {
            Transform newTrans = Instantiate(depthToggle);
            Toggle toggle = newTrans.GetComponent<Toggle>();
            int index = i;
            int depth = index + 1;
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((isOn) => { if (isOn) currGridData.depth = depth; });
            toggle.GetComponentInChildren<Text>().text = depth.ToString();
            newTrans.SetParent(depthRoot);
            newTrans.Reset();
        }
    }

    private void InitColorRoot()
    {
        gridDataList.Clear();
        Material[] materials = baseColorPrefab.GetComponentInChildren<MeshRenderer>().materials;
        int _colorId = 0;
        foreach (Material _material in materials)
        {
            Color _color = _material.color;
            gridDataList.Add(new SGridData() { color = _color, colorId = _colorId });
            _colorId++;
        }

        colorRoot.RemoveAllChildren();
        foreach (SGridData gridData in gridDataList)
        {
            Transform newTrans = Instantiate(colorToggle);
            Toggle toggle = newTrans.GetComponent<Toggle>();
            toggle.targetGraphic.color = gridData.color;
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((isOn) =>
            {
                if (!isOn) return;
                currGridData.color = gridData.color;
                currGridData.colorId = gridData.colorId;
            });
            toggle.group = colorRoot.GetComponent<ToggleGroup>();
            newTrans.SetParent(colorRoot);
            newTrans.Reset();
        }
    }

    private void InitCurrGridData()
    {
        currGridData = gridDataList[0];
        currGridData.depth = 1;
    }

    private void InitGridRoot()
    {
        map = new MapGrid[mapSize][];
        for (int i = 0; i < mapSize; i++) map[i] = new MapGrid[mapSize];

        gridRoot.RemoveAllChildren();
        RectTransform rectTransform = gridRoot.GetOrAddComponent<RectTransform>();
        rectTransform.sizeDelta = 800 * Vector2.one;

        GridLayoutGroup gridLayout = gridRoot.GetOrAddComponent<GridLayoutGroup>();
        gridLayout.cellSize = 800 / mapSize * Vector2.one;
        gridLayout.startCorner = GridLayoutGroup.Corner.LowerLeft;
        gridLayout.childAlignment = TextAnchor.LowerLeft;

        for (int i = 0; i < Mathf.Pow(mapSize, 2); i++)
        {
            int row = i / 16;
            int col = i % 16;

            Transform newTrans = Instantiate<Transform>(mapGrid);
            MapGrid newGrid = newTrans.GetComponent<MapGrid>();
            newGrid.row = row;
            newGrid.col = col;

            newTrans.SetParent(gridRoot);
            newTrans.Reset();

            map[col][row] = newGrid;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}
