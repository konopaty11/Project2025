using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class BuingBG : MonoBehaviour
{
    [SerializeField] Text text; // цена фона

    [SerializeField] GameObject prefabPrice;        // префаб цены
    [SerializeField] GameObject prefabCheckMark;    // префаб галочки

    [SerializeField] Image selectedImage;   // выбранный фон

    [SerializeField] Backgrounds background;
    public Backgrounds Background { get { return background; } set { if ((int)value > 0) background = value; } }

    ManagerCoins managerCoins;
    ManagerBackgrounds managerBG;
    ManagerUserData managerUserData;

    bool isCanBuyBG = false;
    bool isBuying = false;

    int priceBG;

    string fileName = "StoreConfig.json";
    string path;

    /// <summary>
    /// получение компонентов скриптов
    /// получение и установка цены из json
    /// </summary>
    void Awake()
    {
        managerCoins = FindObjectOfType<ManagerCoins>();
        managerBG = FindObjectOfType<ManagerBackgrounds>();
        managerUserData = FindObjectOfType<ManagerUserData>();

        path = Path.Combine(Application.streamingAssetsPath, fileName);
        string data = File.ReadAllText(path);
        PriceOfBG price = JsonConvert.DeserializeObject<PriceOfBG>(data);

        priceBG = price.Prices[background.ToString()];
        text.text = price.Prices[background.ToString()].ToString();
    }

    /// <summary>
    /// меняет цвет цены 
    /// на красный если цена фона больше баланса
    /// и на белый если иначе
    /// </summary>
    void Update()
    {
        isCanBuyBG = managerCoins.CountCoins >= priceBG;

        if (isCanBuyBG) text.color = Color.white;
        else text.color = Color.red;
    }

    /// <summary>
    /// обрабатывает нажатие на фон
    /// если фон не куплен, а купить его возможно то происходит покупка и применение
    /// если фон кулен применение текущего фона
    /// </summary>
    public void SelectBG()
    {
        if (!isBuying && isCanBuyBG)
        {
            if (managerBG.ActiveCheckMark != null)
            {
                managerBG.ActiveCheckMark.SetActive(false);
                Debug.Log(managerBG.ActiveCheckMark);
            }
            Select();
            managerCoins.SetCoins(managerCoins.CountCoins - priceBG);

            switch (Background) // передача в класс для сериализации того, что был куплен фон
            {
                case Backgrounds.Blue:
                    managerUserData.user.Blue = true;
                    managerUserData.user.SelectBackground = Backgrounds.Blue;
                    break;
                case Backgrounds.Red:
                    managerUserData.user.Red = true;
                    managerUserData.user.SelectBackground = Backgrounds.Red;
                    break;
                case Backgrounds.Yellow:
                    managerUserData.user.Yellow = true;
                    managerUserData.user.SelectBackground = Backgrounds.Yellow;
                    break;
            }
        }
        else if (isBuying)
        {
            managerBG.ActiveCheckMark.SetActive(false);
            managerBG.ActiveCheckMark = prefabCheckMark;
            managerBG.SetBackground(Background);

            managerBG.ActiveSprite = selectedImage.sprite;
            prefabCheckMark.SetActive(true);

            switch (Background) // передача в класс для сериализации того, что был выбран фон
            {
                case Backgrounds.Blue:
                    managerUserData.user.SelectBackground = Backgrounds.Blue;
                    break;
                case Backgrounds.Red:
                    managerUserData.user.SelectBackground = Backgrounds.Red;
                    break;
                case Backgrounds.Yellow:
                    managerUserData.user.SelectBackground = Backgrounds.Yellow;
                    break;
            }
        }
    }

    /// <summary>
    /// делает фон выбранным 
    /// </summary>
    public void Select()
    {
        prefabPrice.SetActive(false);
        prefabCheckMark.SetActive(true);

        managerBG.ActiveCheckMark = prefabCheckMark;
        managerBG.SetBackground(Background);

        managerBG.ActiveSprite = selectedImage.sprite;
        isBuying = true;
    }

    /// <summary>
    /// делает фон купленным
    /// </summary>
    public void SetActiveTrue()
    {
        prefabPrice.SetActive(false);
        isBuying = true;
    }
}
