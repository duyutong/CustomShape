using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using D.Unity3dTools;
using UnityEngine.UI;

public class ShapeMapManager : MonoBehaviour
{
    public Transform mapGrid;
    public int mapSize = 16;
    // Start is called before the first frame update
    void Start()
    {
        InitMap();
    }

    private void InitMap()
    {
        transform.RemoveAllChildren();
        RectTransform rectTransform = transform.GetOrAddComponent<RectTransform>();
        rectTransform.sizeDelta = 800 * Vector2.one;

        GridLayoutGroup gridLayout = transform.GetOrAddComponent<GridLayoutGroup>();
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

            newTrans.SetParent(transform);
            newTrans.Reset();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
