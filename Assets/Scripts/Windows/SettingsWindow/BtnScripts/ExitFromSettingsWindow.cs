using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromSettingsWindow : MonoBehaviour, IClickable
{
    [SerializeField] GameObject window;

    /// <summary>
    /// вызывает возможное создание окна сохранения настроек
    /// и по если оно не создаётся закрывает окно настроек
    /// </summary>
    public void OnBtnClick()
    {
        ManagerSettingsWindow manager = FindObjectOfType<ManagerSettingsWindow>();
        if(manager.SaveSettings()) manager.SetActiveSettingsWindowFalse();
    }
}
