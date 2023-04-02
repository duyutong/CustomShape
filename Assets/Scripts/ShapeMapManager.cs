using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using D.Unity3dTools;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ShapeMapManager : MonoBehaviour
{
    public Transform mapGrid;
    public int mapSize = 16;

    public Transform gridRoot;
    public Transform colorRoot;
    public Transform depthRoot;
    public Transform baseColorPrefab;
    public Transform colorToggle;

    private List<SGridData> cmList = new List<SGridData>();
    // Start is called before the first frame update
    void Start()
    {
        InitGridRoot();
        InitColorRoot();
    }

    private void InitColorRoot()
    {
        cmList.Clear();
        Material[] materials = baseColorPrefab.GetComponentInChildren<MeshRenderer>().materials;
        foreach (Material _material in materials)
        {
            Color _color = _material.color;
            cmList.Add(new SGridData() { color = _color, material = _material });
        }

        colorRoot.RemoveAllChildren();
        foreach (SGridData gridData in cmList) 
        {
            GameObject newGo = new GameObject();
            newGo.AddComponent<RectTransform>();
            Image image = newGo.transform.GetOrAddComponent<Image>();
            image.color = gridData.color;

           
        }
    }

    private void InitGridRoot()
    {
        gridRoot.RemoveAllChildren();
        RectTransform rectTransform = gridRoot.GetOrAddComponent<RectTransform>();
        rectTransform.sizeDelta = 800 * Vector2.one;

        GridLayoutGroup gridLayout = gridRoot.GetOrAddComponent<GridLayoutGroup>();
        gridLayout.cellSize = 800 / mapSize * Vector2.one;
        gridLayout.startCorner = GridLayoutGroup.Corner.LowerLeft;
        gridLayout.childAlignment = TextAnchor.LowerLeft;

        for (int i = 0; i < Mathf.Pow(mapSize,2); i++)
        {
            int row = i / 16;
            int col = i % 16;

            Transform newTrans = Instantiate<Transform>(mapGrid);
            MapGrid newGrid = newTrans.GetComponent<MapGrid>();
            newGrid.row = row;
            newGrid.col = col;

            newTrans.SetParent(gridRoot);
            newTrans.Reset();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
