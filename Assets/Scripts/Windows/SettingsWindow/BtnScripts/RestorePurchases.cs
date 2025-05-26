using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChangeScreenResolution;
using static ChangeLanguage;
using System.IO;
using Newtonsoft.Json;

public class RestorePurchases : MonoBehaviour, IClickable
{
    ManagerSettingsWindow manager;

    string fileName = "BaseGameSettings.json";
    string path;

    ScrResolution resolution;
    bool isWindowed;
    bool isActiveSoundAndMusic;
    Languages language;

    private void Start()
    {
        manager = FindObjectOfType<ManagerSettingsWindow>();
        path = Path.Combine(Application.streamingAssetsPath, fileName);

        string data = File.ReadAllText(path);
        Settings setgs = JsonConvert.DeserializeObject<Settings>(data);

        resolution = (ScrResolution)setgs.resolutionOld;
        isWindowed = setgs.isWindowedOld;
        isActiveSoundAndMusic =setgs.isActiveSoundOld;
        language = (Languages)setgs.languageOld;
    }

    /// <summary>
    /// устанавливает настройки по умолчанию
    /// </summary>
    public void OnBtnClick()
    {
        manager.SetSettings(resolution, isWindowed, isActiveSoundAndMusic, language);
    }
}
