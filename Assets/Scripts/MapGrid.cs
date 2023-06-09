using D.Unity3dTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapGrid : MonoBehaviour
{
    public int row;
    public int col;
    public SGridData gridData;

    private Text txtDepth;
    private Image imgColor;

    private ShapeMapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        EventTrigger eventTrigger = transform.GetOrAddComponent<EventTrigger>();
        eventTrigger.RemoveAllEventListener();
        eventTrigger.AddTrggerEventListener(EventTriggerType.PointerClick, OnPointerClick);

        mapManager = ShapeMapManager.instantiate;
        gridData = new SGridData();

        txtDepth = transform.GetComponentInChildren<Text>();
        imgColor = transform.GetComponentInChildren<Image>();

        RefreshSelf();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // ��������ɫ
            Debug.Log("��������ɫ");
            gridData.colorId = mapManager.currGridData.colorId;
            gridData.color = mapManager.currGridData.color;
            gridData.depth = mapManager.currGridData.depth;
            RefreshSelf();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // �Ҽ��������
            Debug.Log("�Ҽ��������");
            gridData.color = new Color();
            gridData.depth = 0;
            RefreshSelf();
        }
    }
    public void RefreshSelf()
    {
        txtDepth.text = gridData.depth.ToString();
        imgColor.color = gridData.color;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
