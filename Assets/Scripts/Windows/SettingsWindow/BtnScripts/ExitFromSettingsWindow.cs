using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromSettingsWindow : MonoBehaviour, IClickable
{
    [SerializeField] GameObject window;

    /// <summary>
    /// �������� ��������� �������� ���� ���������� ��������
    /// � �� ���� ��� �� �������� ��������� ���� ��������
    /// </summary>
    public void OnBtnClick()
    {
        ManagerSettingsWindow manager = FindObjectOfType<ManagerSettingsWindow>();
        if(manager.SaveSettings()) manager.SetActiveSettingsWindowFalse();
    }
}
