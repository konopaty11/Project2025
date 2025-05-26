using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCoins100InMin : MonoBehaviour, IClickable
{
    [SerializeField] Text text;
    
    //[SerializeField] Animator animator;

    System.Random rnd = new System.Random();
    ManagerCoins manager;

    bool isCanGetCoins = true;

    /// <summary>
    /// обработка нажатия кнопки получения монет
    /// </summary>
    public void OnBtnClick()
    {
        manager = FindObjectOfType<ManagerCoins>();

        if (isCanGetCoins)
        {
            isCanGetCoins = false;

            manager.CreateCoins(transform.position.y, 100);
            StartCoroutine(Timer());
        }
        
    }

    /// <summary>
    /// Таймер на 60 секунд
    /// выводится в формате TimeSpan мм:сс
    /// </summary>
    /// <returns></returns>
    IEnumerator Timer()
    {
        for (int i = 59; i >= 0; i--)
        {
            text.text = TimeSpan.FromSeconds(i).ToString(@"mm\:ss");
            yield return new WaitForSeconds(1f);
        }
        isCanGetCoins = true;
        text.text = "100";
    }
}
