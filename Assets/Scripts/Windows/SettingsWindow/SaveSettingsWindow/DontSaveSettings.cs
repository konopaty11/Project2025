using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontSaveSettings : MonoBehaviour, IClickable
{
    [SerializeField] Text text;

    ManagerSettingsWindow manager;

    private void Start()
    {
        manager = FindObjectOfType<ManagerSettingsWindow>();
    }

    /// <summary>
    /// ������ �������� ��� ��������� ���� ���������� ��������
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    /// <summary>
    /// �������� ���� 
    /// ��������� ��������
    /// � �� ���������� ��������
    /// </summary>
    public void OnBtnClick()
    {
        StopCoroutine(Timer());

        manager.ReturnOldSettings();
        manager.SetActiveSettingsWindowFalse();
        manager.SetActiveSaveSettingsWindowFalse();
    }

    /// <summary>
    /// ��������� ������ �� 10�
    /// �� ��������� ��������� ����
    /// </summary>
    /// <returns></returns>
    public IEnumerator Timer()
    {
        for (int i = 9; i >= 0; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        OnBtnClick();
    }
}
