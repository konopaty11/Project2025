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
        // �������� �� �������
        togWinMode.onValueChanged.AddListener(ChangeWindowMode);
    }

    /// <summary>
    /// ������� ����� ����������
    /// </summary>
    /// <param name="isActive">������� �� ������� ����� </param>
    public void ChangeWindowMode(bool isActive)
    {
        Screen.fullScreenMode = isActive ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;

        Image togImage = togWinMode.GetComponent<Image>();
        togImage.sprite = isActive ? togOn : togOff;

        // � ������ ������� ������ �� �� ������� ������� ������ ���������� �����
        togWinMode.isOn = isActive;

        ManagerSettingsWindow.IsWindowedNew = isActive;
    }

}
