using D.Unity3dTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct SGridData
{
    public int index;
    public Color color;
    public int colorId;
    public Material material;
    public int depth;
}
public class MapGrid : MonoBehaviour
{
    public int row;
    public int col;
<<<<<<< HEAD
    public SGridData gridData;

    private Text txtDepth;
    private Image imgColor;
=======
    public Color color;
>>>>>>> cb3b6c745f555cf535e6c093117b6adc1ec4d4c8
    private ShapeMapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        EventTrigger eventTrigger = transform.GetOrAddComponent<EventTrigger>();
        eventTrigger.RemoveAllEventListener();
        eventTrigger.AddTrggerEventListener(EventTriggerType.PointerClick, OnPointerClick);

        mapManager = ShapeMapManager.instantiate;
<<<<<<< HEAD
        gridData = new SGridData();

        txtDepth = transform.GetComponentInChildren<Text>();
        imgColor = transform.GetComponentInChildren<Image>();

        RefreshSelf();
=======
>>>>>>> cb3b6c745f555cf535e6c093117b6adc1ec4d4c8
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // ×ó¼üµã»÷ÉÏÉ«
            Debug.Log("×ó¼üµã»÷ÉÏÉ«");
            gridData.colorId = mapManager.currGridData.colorId;
            gridData.color = mapManager.currGridData.color;
            gridData.depth = mapManager.currGridData.depth;
            RefreshSelf();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // ÓÒ¼üµã»÷²Á³ý
            Debug.Log("ÓÒ¼üµã»÷²Á³ý");
            gridData.color = new Color();
            gridData.depth = 0;
            RefreshSelf();
        }
    }
    private void RefreshSelf()
    {
        txtDepth.text = gridData.depth.ToString();
        imgColor.color = gridData.color;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
