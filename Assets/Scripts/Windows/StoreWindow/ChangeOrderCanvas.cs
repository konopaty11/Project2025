using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrderCanvas : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] int sortOrderEnable;
    [SerializeField] int sortOrderDisable;

    /// <summary>
    /// �������� ������� ���������� ������� ��� ��������� �������
    /// </summary>
    public void OnEnable()
    {
        canvas.sortingOrder = sortOrderEnable;
    }

    /// <summary>
    /// �������� ������� ���������� ������� ��� ����������� �������
    /// </summary>
    public void OnDisable()
    {
        if (canvas != null) canvas.sortingOrder = sortOrderDisable;
    }
}
