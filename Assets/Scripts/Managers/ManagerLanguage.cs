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
/// ������ ���� ����� ������ ����������
/// </summary>
/// <param name="searchWords"> ������� ������ ���� ��� ������ </param>
/// <param name="activeWords"> ���������� ������ ���� </param>
public void ChangeTextLanguage(Dictionary<string, string> dct)
    {
        // ��������� ���� �������� ����������� Text
        Text[] texts = FindObjectsOfType<Text>(true);
        // ��������� ���� �������� ����������� TMP
        TextMeshProUGUI[] TMPs = FindObjectsOfType<TextMeshProUGUI>(true);

        // ����� �������� ���� text
        foreach (Text text in texts)
            if (dct.ContainsKey(text.text)) text.text = dct[text.text];

        foreach (TextMeshProUGUI tmp in TMPs)
            if (dct.ContainsKey(tmp.text)) tmp.text = dct[tmp.text];
    }
}
