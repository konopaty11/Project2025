using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : MonoBehaviour, IClickable
{
    public void OnBtnClick()
    {
        Application.Quit();
    }
}
