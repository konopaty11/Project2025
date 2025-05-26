using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChangeScreenResolution;
using static ChangeLanguage;
using System.IO;
using Newtonsoft.Json;

public class ManagerSettingsWindow : MonoBehaviour, IWritable
{
    [SerializeField] GameObject settingsWindow;
    [SerializeField] GameObject saveSettingsWindow;

    string fileName = "UserGameSettings.json";
    string path;

    // старые настройки
    static ScrResolution resolutionOld { get; set; }
    static bool isWindowedOld { get; set; }
    static bool isActiveSoundOld { get; set; }
    static Languages languageOld { get; set; }

    // выбранные настройки
    public static ScrResolution ResolutionNew { get; set; } = ScrResolution.R1920x1080;
    public static bool IsWindowedNew { get; set; } = false;
    public static bool IsActiveSoundNew { get; set; } = true;
    public static Languages LanguageNew { get; set; } = Languages.English;

    ChangeScreenResolution scrRes;
    WinModeToggle winMode;
    SoundAndMusicToggle soundAndMus;
    ChangeLanguage changeLan;

    private void Start()
    {
        // определение путя к файла
        path = Path.Combine(Application.streamingAssetsPath, fileName);

        // объекты UI окна настроек
        scrRes = FindObjectOfType<ChangeScreenResolution>(true);
        winMode = FindObjectOfType<WinModeToggle>(true);
        soundAndMus = FindObjectOfType<SoundAndMusicToggle>(true);
        changeLan = FindObjectOfType<ChangeLanguage>(true);

        // установка значений по умолчанию
        ReadJson();
        SetSettings(resolutionOld, isWindowedOld, isActiveSoundOld, languageOld);
    }

    /// <summary>
    /// устанавливает настройки по умолчанию
    /// </summary>
    public void SetSettings(ScrResolution resolution, bool isWindowed, bool isActiveSound, Languages language)
    {
        scrRes.ChangeResolution((int)resolution);
        winMode.ChangeWindowMode(isWindowed);
        soundAndMus.ChangeVolumeSoundsAndMusic(isActiveSound);
        changeLan.OnDropdownValueChanged((int)language);
    }

    /// <summary>
    /// Активирует окно подтверждения сохранения настроек
    /// при условии, что настройки были изменены
    /// </summary>
    /// <returns> были ли изменены настройки </returns>
    public bool SaveSettings()
    {
        ReadJson();

        if (resolutionOld != ResolutionNew || isWindowedOld != IsWindowedNew ||
            isActiveSoundOld != IsActiveSoundNew || languageOld != LanguageNew)
        {
            saveSettingsWindow.SetActive(true);
            return false;
        }
        return true;
    }

    /// <summary>
    /// приравнивание новых настроек старым
    /// </summary>
    public void WriteData()
    {
        Settings stgs = new Settings((int)ResolutionNew, IsWindowedNew, IsActiveSoundNew, (int)LanguageNew);
        string data = JsonUtility.ToJson(stgs, true);
        File.WriteAllText(path, data);
    }

    /// <summary>
    /// возвращает старые настройки
    /// </summary>
    public void ReturnOldSettings()
    {
        ReadJson();
        SetSettings(resolutionOld, isWindowedOld, isActiveSoundOld, languageOld);
    }

    /// <summary>
    /// Читает json и записывает отформатированные значения в поля
    /// </summary>
    void ReadJson()
    {
        string data = File.ReadAllText(path);
        Settings setgs = JsonConvert.DeserializeObject<Settings>(data);

        resolutionOld = (ScrResolution)setgs.resolutionOld;
        isWindowedOld = setgs.isWindowedOld;
        isActiveSoundOld = setgs.isActiveSoundOld;
        languageOld = (Languages)setgs.languageOld;
    }

    /// <summary>
    /// деактивирует окно настроек
    /// </summary>
    public void SetActiveSettingsWindowFalse()
    {
        settingsWindow.SetActive(false);
    }

    /// <summary>
    /// деактивирует окно сохранения настроек
    /// </summary>
    public void SetActiveSaveSettingsWindowFalse()
    {
        saveSettingsWindow.SetActive(false);
    }
}
