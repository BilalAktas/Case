﻿//using Cinemachine;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static FixedTouchField instance;

    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;

    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Pressed)
        {

            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;               
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;         
            }
             
        }
        else
        {
            TouchDist = new Vector2(0,0);
        }      
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;  
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

}