using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler {

    public float smoothing;

    private Vector2 origin, direction, smoothDirection;
    private bool touched;
    private int pointerID;

    void Awake(){
        this.direction = Vector2.zero;
        this.touched = false;
    }

    public void OnPointerDown(PointerEventData data) {
        if (!touched) {
            touched = true;
            this.pointerID = data.pointerId;
            origin = data.position;
        }
    }

    public void OnDrag(PointerEventData data) {
        // Prevent multiple touches
        if (comparePointerID(data)) {
            //Compare difference between our start point and current point position
            Vector2 currentPosition = data.position;
            Vector2 directionRaw = currentPosition - origin;
            this.direction = directionRaw.normalized;
        }
    }

    public void OnPointerUp(PointerEventData data) {
        if (comparePointerID(data))
            this.direction = Vector2.zero; //Resets Everything
    }

    private bool comparePointerID(PointerEventData data) {
        return data.pointerId == this.pointerID;
    }

    public Vector2 GetDirection() {
        return smoothDirection = Vector2.MoveTowards(smoothDirection, this.direction, smoothing);
    }
}
