using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWindowBtn : MonoBehaviour, IClickable
{
    [SerializeField] GameObject videoWin;

    /// <summary>
    /// делает видео активным
    /// </summary>
    public void OnBtnClick()
    {
        if (ShowVideo.isCanShowVideo)
        {
            ShowVideo.isCanShowVideo = false;
            videoWin.SetActive(true);

        }
    }
}
