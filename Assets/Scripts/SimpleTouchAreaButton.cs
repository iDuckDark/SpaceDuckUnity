using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool touched, canFire;
    private int pointerID;

    void Awake()
    {
        this.touched = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            this.canFire = this.touched = true;
            this.pointerID = data.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (comparePointerID(data)) this.canFire = this.touched = false;
    }

    private bool comparePointerID(PointerEventData data)
    {
        return this.pointerID == data.pointerId;
    }

    public bool CanFire()
    {
        return this.canFire;
    }

}
