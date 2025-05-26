using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings : MonoBehaviour, IClickable
{
    ManagerSettingsWindow manager;

    private void Start()
    {
        manager = FindObjectOfType<ManagerSettingsWindow>();
    }

    /// <summary>
    /// ��������� ����
    /// ������������� ��������
    /// � ��������� ���������
    /// </summary>
    public void OnBtnClick()
    {
        DontSaveSettings noBtn = FindObjectOfType<DontSaveSettings>();
        StopCoroutine(noBtn.Timer());

        manager.WriteData();
        manager.SetActiveSettingsWindowFalse();
        manager.SetActiveSaveSettingsWindowFalse();
    }
}
