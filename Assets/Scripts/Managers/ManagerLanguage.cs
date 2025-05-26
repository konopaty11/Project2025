using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerLanguage : MonoBehaviour
{

/// <summary>
/// меняет язык всему тексту приложения
/// </summary>
/// <param name="searchWords"> искомый список слов для замены </param>
/// <param name="activeWords"> заменяющий список слов </param>
public void ChangeTextLanguage(Dictionary<string, string> dct)
    {
        // получение всех дочерних компонентов Text
        Text[] texts = FindObjectsOfType<Text>(true);
        // получение всех дочерних компонентов TMP
        TextMeshProUGUI[] TMPs = FindObjectsOfType<TextMeshProUGUI>(true);

        // смена значений поля text
        foreach (Text text in texts)
            if (dct.ContainsKey(text.text)) text.text = dct[text.text];

        foreach (TextMeshProUGUI tmp in TMPs)
            if (dct.ContainsKey(tmp.text)) tmp.text = dct[tmp.text];
    }
}
