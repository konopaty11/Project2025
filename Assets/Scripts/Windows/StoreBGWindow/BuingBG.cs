using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class BuingBG : MonoBehaviour
{
    [SerializeField] Text text; // ���� ����

    [SerializeField] GameObject prefabPrice;        // ������ ����
    [SerializeField] GameObject prefabCheckMark;    // ������ �������

    [SerializeField] Image selectedImage;   // ��������� ���

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
    /// ��������� ����������� ��������
    /// ��������� � ��������� ���� �� json
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
    /// ������ ���� ���� 
    /// �� ������� ���� ���� ���� ������ �������
    /// � �� ����� ���� �����
    /// </summary>
    void Update()
    {
        isCanBuyBG = managerCoins.CountCoins >= priceBG;

        if (isCanBuyBG) text.color = Color.white;
        else text.color = Color.red;
    }

    /// <summary>
    /// ������������ ������� �� ���
    /// ���� ��� �� ������, � ������ ��� �������� �� ���������� ������� � ����������
    /// ���� ��� ����� ���������� �������� ����
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

            switch (Background) // �������� � ����� ��� ������������ ����, ��� ��� ������ ���
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

            switch (Background) // �������� � ����� ��� ������������ ����, ��� ��� ������ ���
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
    /// ������ ��� ��������� 
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
    /// ������ ��� ���������
    /// </summary>
    public void SetActiveTrue()
    {
        prefabPrice.SetActive(false);
        isBuying = true;
    }
}
