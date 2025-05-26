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
        // подписка на событие изменения значения
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    /// <summary>
    /// срабатывает при изменении значения в выпадающем списке настройки языка
    /// </summary>
    /// <param name="selectedIndex"> индекс выбранного элемента в списке элементов </param>
    public void OnDropdownValueChanged(int selectedInd)
    {
        string fileName = "language.json";
        string path = Path.Combine(Application.streamingAssetsPath, fileName);    // получение путя к json файлу

        string data = File.ReadAllText(path); // чтение json файла
        LanguageRoots rootData = JsonConvert.DeserializeObject<LanguageRoots>(data); // получение корнегого значения

        string selectedText = dropdown.options[selectedInd].text;             // активное значение выпадающего списка
        ManagerSettingsWindow.LanguageNew = selectedText == "English" ? Languages.English : Languages.Russian;  // смена нового значения

        ManagerLanguage changeLanguage = FindObjectOfType<ManagerLanguage>();
        changeLanguage.ChangeTextLanguage(rootData.Languages[selectedText]);

        // в случае запуска метода не по событию вручную менять активность тогла
        // и проигрывать звук
        if (audioSource.isActiveAndEnabled == true)
            audioSource.Play();
        dropdown.value = selectedInd;
    }
}
