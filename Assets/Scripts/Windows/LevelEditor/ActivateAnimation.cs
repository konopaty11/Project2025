using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    MoveCamera moveCam;

    void Awake()
    {
        moveCam = FindObjectOfType<MoveCamera>();
    }

    /// <summary>
    /// ��������� �������� ����������� ������ ��� ��������� ������
    /// � ��������� ����������� �� � �����������
    /// </summary>
    void OnEnable()
    {
        moveCam.CurrencyTime = 0;

        moveCam.IsAnimation = true;
        moveCam.IsAnimationReturn = false;
        moveCam.IsLevelEditorActive = true;
    }

    /// <summary>
    /// ��������� �������� ����������� ������ �� ������� ����
    /// </summary>
    void OnDisable()
    {
        moveCam.CurrencyTime = 0;

        moveCam.IsAnimation = false;
        moveCam.IsAnimationReturn = true;
        moveCam.IsLevelEditorActive = false;
    }
}
