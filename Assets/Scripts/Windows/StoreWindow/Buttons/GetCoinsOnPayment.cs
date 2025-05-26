using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class GetCoinsOnPayment : MonoBehaviour
{
    [SerializeField] GameObject paymentWindow;
    [SerializeField] int countCoins;

    /// <summary>
    /// активирует окно
    /// </summary>
    public void OnBtnClick()
    {
        paymentWindow.SetActive(true);
        StartCoroutine(Sleep());
    }

    /// <summary>
    /// ждёт две секунды и вызывает анимацию монеток
    /// </summary>
    /// <returns></returns>
    IEnumerator Sleep()
    {
        yield return new WaitForSeconds(2f);
        paymentWindow.SetActive(false);

        ManagerCoins manager = FindObjectOfType<ManagerCoins>();
        manager.CreateCoins(transform.position.y, countCoins);
    }
}
