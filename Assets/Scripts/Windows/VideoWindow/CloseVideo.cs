using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseVideo : MonoBehaviour, IClickable
{
    /// <summary>
    /// ��������� ������ ����� ��� �������� ���� �����
    /// </summary>
    public void OnBtnClick()
    {
        CoroutineVideo coroutine = FindObjectOfType<CoroutineVideo>();
        coroutine.StartTimerVideo();
    }
}
