using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class ShowVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    // возможность запуска видео
    public static bool isCanShowVideo { get; set; } = true;

    /// <summary>
    /// запускает видео при активации окна
    /// и анимацию монет
    /// </summary>
    private void OnEnable()
    {
        videoPlayer.Play();

        
    }

    private void OnDisable()
    {
        ManagerCoins manager = FindObjectOfType<ManagerCoins>();
        manager.CreateCoins(transform.position.y, 100);
    }




}
