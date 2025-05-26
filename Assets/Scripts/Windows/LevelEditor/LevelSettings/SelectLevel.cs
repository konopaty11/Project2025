using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SelectLevel : MonoBehaviour, IClickable
{
    ManagerLevel managerLvl;
    Text text;
    private void Start()
    {
        managerLvl = FindObjectOfType<ManagerLevel>();
        text = gameObject.GetComponentInChildren<Text>();
    }

    public void OnBtnClick()
    {
        MatchCollection matches = Regex.Matches(text.text, @"\d+");
        if (int.TryParse(matches[0].Value, out int res))
            managerLvl.ActiveLevel = --res;
        else
            Debug.Log($"»з {text.text} регул€рка выдала {matches[0].Value} - преобразвание в int не прошло");
    }
}
