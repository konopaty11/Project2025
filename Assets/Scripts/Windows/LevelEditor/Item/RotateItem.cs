using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        ManagerItems.IsRotate = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ManagerItems.IsRotate = false;
    }
}
