using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinModeToggle : MonoBehaviour
{
    [SerializeField] Toggle togWinMode;
    [SerializeField] Sprite togOn;
    [SerializeField] Sprite togOff;

    void Start()
    {
        // подписка на событие
        togWinMode.onValueChanged.AddListener(ChangeWindowMode);
    }

    /// <summary>
    /// сменяет режим приложения
    /// </summary>
    /// <param name="isActive">активен ли оконный режим </param>
    public void ChangeWindowMode(bool isActive)
    {
        Screen.fullScreenMode = isActive ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;

        Image togImage = togWinMode.GetComponent<Image>();
        togImage.sprite = isActive ? togOn : togOff;

        // в случае запуска метода не по событию вручную менять активность тогла
        togWinMode.isOn = isActive;

        ManagerSettingsWindow.IsWindowedNew = isActive;
    }

}
