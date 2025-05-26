using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoroutineVideo : MonoBehaviour
{
    /// <summary>
    /// ��������� ��������
    /// </summary>
    public void StartTimerVideo()
    {
        StartCoroutine(TimerVideo());
    }

    /// <summary>
    /// ��� ����������� ��������� ����� ����� 30 ������
    /// </summary>
    /// <returns></returns>
    public IEnumerator TimerVideo()
    {
        for (int i = 30; i >= 0; i--) yield return new WaitForSeconds(1f);
        ShowVideo.isCanShowVideo = true;
    }
}
