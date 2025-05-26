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

    // ������ ���������
    static ScrResolution resolutionOld { get; set; }
    static bool isWindowedOld { get; set; }
    static bool isActiveSoundOld { get; set; }
    static Languages languageOld { get; set; }

    // ��������� ���������
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
        // ����������� ���� � �����
        path = Path.Combine(Application.streamingAssetsPath, fileName);

        // ������� UI ���� ��������
        scrRes = FindObjectOfType<ChangeScreenResolution>(true);
        winMode = FindObjectOfType<WinModeToggle>(true);
        soundAndMus = FindObjectOfType<SoundAndMusicToggle>(true);
        changeLan = FindObjectOfType<ChangeLanguage>(true);

        // ��������� �������� �� ���������
        ReadJson();
        SetSettings(resolutionOld, isWindowedOld, isActiveSoundOld, languageOld);
    }

    /// <summary>
    /// ������������� ��������� �� ���������
    /// </summary>
    public void SetSettings(ScrResolution resolution, bool isWindowed, bool isActiveSound, Languages language)
    {
        scrRes.ChangeResolution((int)resolution);
        winMode.ChangeWindowMode(isWindowed);
        soundAndMus.ChangeVolumeSoundsAndMusic(isActiveSound);
        changeLan.OnDropdownValueChanged((int)language);
    }

    /// <summary>
    /// ���������� ���� ������������� ���������� ��������
    /// ��� �������, ��� ��������� ���� ��������
    /// </summary>
    /// <returns> ���� �� �������� ��������� </returns>
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
    /// ������������� ����� �������� ������
    /// </summary>
    public void WriteData()
    {
        Settings stgs = new Settings((int)ResolutionNew, IsWindowedNew, IsActiveSoundNew, (int)LanguageNew);
        string data = JsonUtility.ToJson(stgs, true);
        File.WriteAllText(path, data);
    }

    /// <summary>
    /// ���������� ������ ���������
    /// </summary>
    public void ReturnOldSettings()
    {
        ReadJson();
        SetSettings(resolutionOld, isWindowedOld, isActiveSoundOld, languageOld);
    }

    /// <summary>
    /// ������ json � ���������� ����������������� �������� � ����
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
    /// ������������ ���� ��������
    /// </summary>
    public void SetActiveSettingsWindowFalse()
    {
        settingsWindow.SetActive(false);
    }

    /// <summary>
    /// ������������ ���� ���������� ��������
    /// </summary>
    public void SetActiveSaveSettingsWindowFalse()
    {
        saveSettingsWindow.SetActive(false);
    }
}
