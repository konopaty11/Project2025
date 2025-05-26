using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;



public class ChangeLanguage : MonoBehaviour 
{
    public enum Languages
    {
        Russian,
        English
    }

    [SerializeField] Dropdown dropdown;
    [SerializeField] AudioSource audioSource;

    public static Languages Language { get; set; }

    void Start()
    {
        // �������� �� ������� ��������� ��������
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    /// <summary>
    /// ����������� ��� ��������� �������� � ���������� ������ ��������� �����
    /// </summary>
    /// <param name="selectedIndex"> ������ ���������� �������� � ������ ��������� </param>
    public void OnDropdownValueChanged(int selectedInd)
    {
        string fileName = "language.json";
        string path = Path.Combine(Application.streamingAssetsPath, fileName);    // ��������� ���� � json �����

        string data = File.ReadAllText(path); // ������ json �����
        LanguageRoots rootData = JsonConvert.DeserializeObject<LanguageRoots>(data); // ��������� ��������� ��������

        string selectedText = dropdown.options[selectedInd].text;             // �������� �������� ����������� ������
        ManagerSettingsWindow.LanguageNew = selectedText == "English" ? Languages.English : Languages.Russian;  // ����� ������ ��������

        ManagerLanguage changeLanguage = FindObjectOfType<ManagerLanguage>();
        changeLanguage.ChangeTextLanguage(rootData.Languages[selectedText]);

        // � ������ ������� ������ �� �� ������� ������� ������ ���������� �����
        // � ����������� ����
        if (audioSource.isActiveAndEnabled == true)
            audioSource.Play();
        dropdown.value = selectedInd;
    }
}
