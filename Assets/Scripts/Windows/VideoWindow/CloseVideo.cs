using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseVideo : MonoBehaviour, IClickable
{
    /// <summary>
    /// Запускает таймер видео при закрытии окна видео
    /// </summary>
    public void OnBtnClick()
    {
        CoroutineVideo coroutine = FindObjectOfType<CoroutineVideo>();
        coroutine.StartTimerVideo();
    }
}
