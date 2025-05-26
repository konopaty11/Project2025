using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;

public class ManagerCoins : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Canvas canvas;
    [SerializeField] AudioSource pickupCoin;
    [SerializeField] AudioSource addCoin;

    System.Random rnd = new System.Random();
    string str;
    public int CountCoins { get; set; }

    public void CreateCoins(float posY, int countCoins)
    {
        for (int i = 0; i < Math.Sqrt(countCoins) / 2; i++)
        {
            Vector3 pos = new Vector3(rnd.Next(550, 950), rnd.Next((int)posY - 200, (int)posY + 200), transform.position.z);
            GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity);
            coin.SetActive(false);
            coin.transform.SetParent(canvas.transform);

            StartCoroutine(AnimationCoins(coin));
        }

        CountCoins += countCoins;
        str = GetStringCountCoins();
        text.text = str;

        
    }

    IEnumerator AnimationCoins(GameObject coin)
    {
        Vector3 targetScale = new Vector3(0.1f, 0.1f, 0.1f);
        float elapsed = 0;
        float duration = 0.07f;

        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(rnd.Next(0, 70) / 100f);
        coin.SetActive(true);
        pickupCoin.Play();

        while (elapsed < duration)
        {
            coin.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Vector3 startPos = coin.transform.position;
        Vector3 targetPos = new Vector3(1770, 1015, 0);
        elapsed = 0;
        duration = 0.5f;

        while (elapsed < duration)
        {
            coin.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        

        Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f);
        elapsed = 0;
        duration = 0.07f;

        yield return new WaitForSeconds(0.1f);
        addCoin.Play();

        while (elapsed < duration)
        {
            coin.transform.localScale = Vector3.Lerp(targetScale, Vector3.zero, elapsed / duration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        Destroy(coin);
    }

    public void SetCoins(int countCoins)
    {
        CountCoins = countCoins;
        str = GetStringCountCoins();
        text.text = str;
    }

    public string GetStringCountCoins()
    {
        return CountCoins >= 1000 ? $"{Math.Round(CountCoins / 1000.0, 3)}k" : CountCoins.ToString();
    }
}
