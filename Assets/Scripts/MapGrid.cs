using D.Unity3dTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct SGridData
{
    public int index;
    public Color color;
    public Material material;
    public int depth;
}
public class MapGrid : MonoBehaviour
{
    public int row;
    public int col;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        EventTrigger eventTrigger = transform.GetOrAddComponent<EventTrigger>();
        eventTrigger.RemoveAllEventListener();
        eventTrigger.AddTrggerEventListener(EventTriggerType.PointerClick, OnPointerClick);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // ×ó¼üµã»÷ÉÏÉ«
            Debug.Log("×ó¼üµã»÷ÉÏÉ«");
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // ÓÒ¼üµã»÷²Á³ý
            Debug.Log("ÓÒ¼üµã»÷²Á³ý");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
