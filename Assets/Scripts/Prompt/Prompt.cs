using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static ChangeLanguage;

public class Prompt : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string keyUIElement;

    Dictionary<string, string> prompts;

    string keyLanguage;
    bool isHovered;

    ManagerPrompt manager;

    /// <summary>
    /// ������������ ������ �� json
    /// </summary>
    void Start()
    {
        manager = FindObjectOfType<ManagerPrompt>();

        string fileName = "prompt.json";
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        string data = File.ReadAllText(path);
        PromptData jsonData = JsonConvert.DeserializeObject<PromptData>(data);

        prompts = jsonData.Prompts[keyUIElement];

    }

    /// <summary>
    /// ���� UI ������ ��� ������������� ������ ���������
    /// </summary>
    private void OnDisable()
    {
        //Debug.Log($"������ ��-�� ����������� {gameObject.name}");
        if (manager.PanelPrompt != null) manager.PanelPrompt.SetActive(false);
        isHovered = false;
    }

    /// <summary>
    /// ��������� ��������� ������� �� ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log($"��������� �� {gameObject.name}");
        isHovered = true;
        StartCoroutine(Timer());
    }

    /// <summary>
    /// ��������� �������� ������� � �������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        manager.PanelPrompt.SetActive(false);
        //Debug.Log($"��� ������ ������� ������������ {gameObject.name}");
    }

    /// <summary>
    /// ������ �� 2 ���
    /// ���� ������ ��� ���� � ������� - ���������� ��������
    /// ����� ������� ��������� ���������
    /// </summary>
    /// <returns></returns>
    IEnumerator Timer()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            if (!isHovered)
            {
                manager.PanelPrompt.SetActive(false);
                //Debug.Log($"� �������� ������������ {gameObject.name}");
                yield break;
            }
        }
        
        manager.PanelPrompt.SetActive(true);
        Text text = manager.PanelPrompt.GetComponentInChildren<Text>();

        keyLanguage = Language == Languages.English ? "en" : "ru";
        text.text = prompts[keyLanguage];

        for (float i = 0; i <= 1; i += 0.01f)
        {
            Color color = text.color;
            color.a = i;
            text.color = color;

            yield return new WaitForSeconds(0.0005f);
        }
        
    }
}