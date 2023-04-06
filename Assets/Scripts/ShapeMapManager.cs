using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using D.Unity3dTools;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

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
    private List<SGridData> cmList = new List<SGridData>();
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
            SUnitCube unitCube = UnitShapeClass.GetUnitCube(_shapeIndex, _depth, _colorId);
            cubeList.Add(unitCube);
        }
        SUnitCube _unitCube = new SUnitCube();
        Vector3[] vertices = new Vector3[cubeList.Count * _unitCube.vertexCount];
        int[] triangles = new int[cubeList.Count * _unitCube.triangleCount];
        Vector2[] uv = new Vector2[cubeList.Count * _unitCube.uvCount];

        foreach (SUnitCube unitCube in cubeList)
        {
            unitCube.vertices.CopyTo(vertices, _unitCube.vertexCount * unitCube.shapeIndex.index);
            unitCube.triangles.CopyTo(triangles, _unitCube.triangleCount * unitCube.shapeIndex.index);
            unitCube.uv.CopyTo(uv, _unitCube.uvCount * unitCube.shapeIndex.index);
        }

        GameObject newObj = new GameObject("Test");
        MeshFilter meshFilter = newObj.transform.GetOrAddComponent<MeshFilter>();
        MeshRenderer meshRenderer = newObj.transform.GetOrAddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        Material material = new Material(Shader.Find("Standard"));
        meshRenderer.material = material;

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
        cmList.Clear();
        Material[] materials = baseColorPrefab.GetComponentInChildren<MeshRenderer>().materials;
        int _colorId = 0;
        foreach (Material _material in materials)
        {
            Color _color = _material.color;
            cmList.Add(new SGridData() { color = _color, material = _material, colorId = _colorId });
            _colorId++;
        }

        colorRoot.RemoveAllChildren();
        foreach (SGridData gridData in cmList)
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
                currGridData.material = gridData.material;
            });
            toggle.group = colorRoot.GetComponent<ToggleGroup>();
            newTrans.SetParent(colorRoot);
            newTrans.Reset();
        }
    }

    private void InitCurrGridData()
    {
        currGridData = cmList[0];
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
