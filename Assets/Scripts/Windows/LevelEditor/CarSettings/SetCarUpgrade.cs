using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCarUpgrade : CarSettings
{
    [SerializeField] GameObject checkMark;
    [SerializeField] Upgrades upgrade;
    [SerializeField] CarSettings settings;
    public override GameObject CheckMark => checkMark;

    static GameObject activeUpgradeMark;
    static GameObject rocketCheckMark;
    static GameObject wingsCheckMark;

    /// <summary>
    /// установка апгрейда
    /// </summary>
    public void SetUpgrade()
    {
        /* 
        если выбрана ракета и активны крылья
        то деактивировать крылья и активировать ракету
        и наоборот
        */
        switch (upgrade)
        {
            case Upgrades.SpikedWheels:
                settings.SpikedWheels = true;
                break;
            case Upgrades.Propeller:
                settings.Propeller = true;
                break;
            case Upgrades.Rocket:
                if (settings.Wings && wingsCheckMark != null)
                {
                    settings.Wings = false;
                    wingsCheckMark.SetActive(false);
                }

                rocketCheckMark = checkMark;
                settings.Rocket = true;
                break;
            case Upgrades.Wings:
                if (settings.Rocket && rocketCheckMark != null)
                {
                    settings.Rocket = false;
                    rocketCheckMark.SetActive(false);
                }

                settings.Wings = true;
                wingsCheckMark = checkMark;
                break;
        }

        activeUpgradeMark = checkMark;
        checkMark.SetActive(true);
    }


    public override void UpdateSettings()
    {
        switch (upgrade)
        {
            case Upgrades.SpikedWheels:
                CheckMark.SetActive(settings.SpikedWheels);
                break;
            case Upgrades.Propeller:
                CheckMark.SetActive(settings.Propeller);
                break;
            case Upgrades.Rocket:
                CheckMark.SetActive(settings.Rocket);
                break;
            case Upgrades.Wings:
                CheckMark.SetActive(settings.Wings);
                break;
        }
    }
}
