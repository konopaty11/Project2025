using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScreenResolution : MonoBehaviour
{
    /// <summary>
    /// перечисление разрешений экрана
    /// </summary>
    public enum ScrResolution
    {
        R1280x720,
        R1366x768, 
        R1600x900, 
        R1920x1080
    }

    [SerializeField] Dropdown dropdown; 

    private void Start()
    {
        // подписка на события
        dropdown.onValueChanged.AddListener(ChangeResolution);
    }

    /// <summary>
    /// Смена разрешения экрана по выбору в dropdown
    /// </summary>
    /// <param name="selectedInd"></param>
    public void ChangeResolution(int selectedInd)
    {
        // словарь разрешений
        var resolutions = new Dictionary<ScrResolution, Vector2Int>()
        {
            { ScrResolution.R1280x720, new Vector2Int(1280, 720) },
            { ScrResolution.R1366x768, new Vector2Int(1366, 768) },
            { ScrResolution.R1600x900, new Vector2Int(1600, 900) },
            { ScrResolution.R1920x1080, new Vector2Int(1920, 1080) }
        };

        ScrResolution selectedResolution = (ScrResolution)selectedInd;
        // смена разрешения
        Screen.SetResolution(resolutions[selectedResolution].x, resolutions[selectedResolution].y, Screen.fullScreen);

        // в случае запуска метода не по событию вручную менять активность тогла
        dropdown.value = selectedInd;
        ManagerSettingsWindow.ResolutionNew = selectedResolution;
        
    }
}
