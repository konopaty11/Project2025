using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] RectTransform parentOutline;
    [SerializeField] CarSettings carSettings;
    [SerializeField] GameObject UpgradeOutline;

    [Header("Upgrades")]
    [SerializeField] GameObject FunBusterVirtual;
    [SerializeField] GameObject RocketVirtual;
    [SerializeField] GameObject SpikedWheelVirtual;
    [SerializeField] GameObject WheelVirtual;
    [SerializeField] GameObject WingsVirtual;
    [SerializeField] GameObject parentUpgrade;

    [Header("Car")]
    [SerializeField] GameObject Can;
    [SerializeField] GameObject Banana;
    [SerializeField] GameObject MilkCarton;
    [SerializeField] GameObject parentCar;

    ManagerLevel managerLvl;
    MoveCamera moveCam;

    Dictionary<GameObject, bool> upgrades;
    int levelPlay = -2;
    GameObject car;

    List<Transform> upgradesOutline = new();


    /// <summary>
    /// создание транспорта и UI апгрейда
    /// </summary>
    void OnEnable()
    {
        moveCam = FindObjectOfType<MoveCamera>();
        managerLvl = FindObjectOfType<ManagerLevel>();

        moveCam.IsFollow = true;
        moveCam.IsAnimationReturn = false;

        // если улучшения и транспорт для этого уровня уже созданы - не создавать новые
        if (levelPlay == managerLvl.LoadLevel) return;

        // создание улучшений
        try
        {
            upgrades = UpgradeData.GetUpgradeData(managerLvl.LoadLevel);
        }
        catch (LevelIsNotLoaded)
        {
            return;
        }
        catch (ArgumentOutOfRangeException)
        {
            UpgradeData.AddLvlUpgrades(FunBusterVirtual, carSettings.Propeller);
            UpgradeData.AddLvlUpgrades(RocketVirtual, carSettings.Rocket);
            UpgradeData.AddLvlUpgrades(SpikedWheelVirtual, carSettings.SpikedWheels);
            UpgradeData.AddLvlUpgrades(WheelVirtual, !carSettings.SpikedWheels);
            UpgradeData.AddLvlUpgrades(WingsVirtual, carSettings.Wings);

            upgrades = UpgradeData.GetUpgradeData(managerLvl.LoadLevel);
        }

        int i = 0;
        foreach (var (k, v) in upgrades)
        {
            if (v == true)
            {
                GameObject upgrade = Instantiate(k, parentUpgrade.transform);
                float x = upgrade.transform.position.x;
                float y = upgrade.transform.position.y;
                if (i == 0) { }
                else if (i % 2 == 0)
                {
                    upgrade.transform.position = new Vector3(i / 2 * 210 + x, y, 0);
                }
                else if (i % 2 == 1)
                {
                    upgrade.transform.position = new Vector3((i / 2 + 1) * -210 + x, y, 0);
                }
                i++;
            }
        }

        // создание транспорта
        switch (carSettings.ActiveCar)
        {
            case Car.Banana:
                car = Instantiate(Banana, parentCar.transform);
                break;
            case Car.Can:
                car = Instantiate(Can, parentCar.transform);
                break;
            case Car.MilkCarton:
                car = Instantiate(MilkCarton, parentCar.transform);
                break;
            default:
                car = Instantiate(Banana);
                break;
        }





        // сохранение уровня
        levelPlay = managerLvl.LoadLevel;
        // установка transform текущей машины
        moveCam.TargetCar = car.transform;
    }

    /// <summary>
    /// возвращение камеры на главный стол
    /// </summary>
    void OnDisable()
    {
        moveCam.CurrencyTime = 0;

        moveCam.IsFollow = false;
        moveCam.IsAnimationReturn = true;
    }

    /// <summary>
    /// создаёт контуры для улучшений
    /// </summary>
    public void CreateUpgradesOutline()
    {
        // список дочерних элементов машины
        Transform[] spaceForUpgrades = car.GetComponentsInChildren<Transform>();

        foreach (Transform el in spaceForUpgrades)
        {
            // проверка элементов на принадлежность к контурам улучшений
            UpgradeType typeOfSpaceOfUpgrade = el.gameObject.GetComponent<UpgradeType>();
            if (typeOfSpaceOfUpgrade == null) continue;

            // создание префаба
            Vector3 screenPos = Camera.main.WorldToScreenPoint(el.position);
            GameObject prefab = Instantiate(UpgradeOutline, screenPos, Quaternion.identity);
            prefab.transform.SetParent(parentOutline);

            // запись префаба в список контуров улучшений
            UpgradeType typeOfUpgrade = prefab.GetComponent<UpgradeType>();
            typeOfUpgrade.Upgrade = typeOfSpaceOfUpgrade.Upgrade;

            upgradesOutline.Add(prefab.transform);
        }
    }

    /// <summary>
    /// деактивирует все контуры для улучшений не считаю того,
    /// который в данный момент двигается
    /// </summary>
    /// <param name="upgrade"> движущиеся улучшение </param>
    public void SetActiveUpgradeOutline(Upgrades upgrade)
    {
        foreach (Transform el in upgradesOutline)
        {
            if (el == null || el.gameObject.activeSelf == false) continue;
            Debug.Log("sdf");
            UpgradeType typeOfUpgrade = el.GetComponent<UpgradeType>();

            // деактивация всех улучшений, кроме соответствующего 
            bool shouldBeActive = typeOfUpgrade.Upgrade == upgrade;
            el.gameObject.SetActive(shouldBeActive);
        }
    }

    public void ActivateUpgradeOutline()
    {
        foreach (Transform el in upgradesOutline)
        {
            el.gameObject.SetActive(true);
        }
    }
}