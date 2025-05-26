using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveWin : MonoBehaviour
{
    [SerializeField] GameObject SettingsLvlWin;
    [SerializeField] GameObject SettingsCarWin;

    public void SetActive()
    {
        if (SettingsCarWin.activeSelf == false)
        {
            SettingsLvlWin.SetActive(true);
            SettingsCarWin.SetActive(true);
        }
        else SettingsCarWin.SetActive(false);
    }
}
