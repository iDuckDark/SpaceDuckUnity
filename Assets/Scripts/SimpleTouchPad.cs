using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private Vector2 origin, direction, smoothDirection;
    public float smoothing;

    void Awake()
    {
       this.direction = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData data)
    {
        //Set our start Point
        origin = data.position; 
    }

    public void OnDrag(PointerEventData data)
    {
        //Compare difference between our start point and current point position
        Vector2 currentPosition = data.position;
        Vector2 directionRaw = currentPosition - origin;
        this.direction = directionRaw.normalized;
        Debug.Log("OnDrag Direction :"+ this.direction);
    }

    public void OnPointerUp(PointerEventData data)
    {
        //Resets Everything
        this.direction = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, this.direction, smoothing);
        return smoothDirection;
    }
}
