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
    /// �������� ���������� � UI ��������
    /// </summary>
    void OnEnable()
    {
        moveCam = FindObjectOfType<MoveCamera>();
        managerLvl = FindObjectOfType<ManagerLevel>();

        moveCam.IsFollow = true;
        moveCam.IsAnimationReturn = false;

        // ���� ��������� � ��������� ��� ����� ������ ��� ������� - �� ��������� �����
        if (levelPlay == managerLvl.LoadLevel) return;

        // �������� ���������
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

        // �������� ����������
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





        // ���������� ������
        levelPlay = managerLvl.LoadLevel;
        // ��������� transform ������� ������
        moveCam.TargetCar = car.transform;
    }

    /// <summary>
    /// ����������� ������ �� ������� ����
    /// </summary>
    void OnDisable()
    {
        moveCam.CurrencyTime = 0;

        moveCam.IsFollow = false;
        moveCam.IsAnimationReturn = true;
    }

    /// <summary>
    /// ������ ������� ��� ���������
    /// </summary>
    public void CreateUpgradesOutline()
    {
        // ������ �������� ��������� ������
        Transform[] spaceForUpgrades = car.GetComponentsInChildren<Transform>();

        foreach (Transform el in spaceForUpgrades)
        {
            // �������� ��������� �� �������������� � �������� ���������
            UpgradeType typeOfSpaceOfUpgrade = el.gameObject.GetComponent<UpgradeType>();
            if (typeOfSpaceOfUpgrade == null) continue;

            // �������� �������
            Vector3 screenPos = Camera.main.WorldToScreenPoint(el.position);
            GameObject prefab = Instantiate(UpgradeOutline, screenPos, Quaternion.identity);
            prefab.transform.SetParent(parentOutline);

            // ������ ������� � ������ �������� ���������
            UpgradeType typeOfUpgrade = prefab.GetComponent<UpgradeType>();
            typeOfUpgrade.Upgrade = typeOfSpaceOfUpgrade.Upgrade;

            upgradesOutline.Add(prefab.transform);
        }
    }

    /// <summary>
    /// ������������ ��� ������� ��� ��������� �� ������ ����,
    /// ������� � ������ ������ ���������
    /// </summary>
    /// <param name="upgrade"> ���������� ��������� </param>
    public void SetActiveUpgradeOutline(Upgrades upgrade)
    {
        foreach (Transform el in upgradesOutline)
        {
            if (el == null || el.gameObject.activeSelf == false) continue;
            Debug.Log("sdf");
            UpgradeType typeOfUpgrade = el.GetComponent<UpgradeType>();

            // ����������� ���� ���������, ����� ���������������� 
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