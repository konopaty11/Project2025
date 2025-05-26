using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrderCanvas : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] int sortOrderEnable;
    [SerializeField] int sortOrderDisable;

    /// <summary>
    /// изменяет порядок прорисовки канваса при активации объекта
    /// </summary>
    public void OnEnable()
    {
        canvas.sortingOrder = sortOrderEnable;
    }

    /// <summary>
    /// изменяет порядок прорисовки канваса при деактивации объекта
    /// </summary>
    public void OnDisable()
    {
        if (canvas != null) canvas.sortingOrder = sortOrderDisable;
    }
}
