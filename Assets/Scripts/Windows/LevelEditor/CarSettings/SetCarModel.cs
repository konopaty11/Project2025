using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCarModel : CarSettings
{
    [SerializeField] GameObject checkMark;
    [SerializeField] Car car;
    [SerializeField] CarSettings settings;
    public override GameObject CheckMark => checkMark;

    static GameObject activeCarMark;

    /// <summary>
    /// установить вид транспорта
    /// </summary>
    public void SetModel()
    {
        settings.ActiveCar = car;

        if (activeCarMark != null) activeCarMark.SetActive(false);
        activeCarMark = checkMark;
        checkMark.SetActive(true);
    }

    
    public override void UpdateSettings()
    {
        if (car == settings.ActiveCar) CheckMark.SetActive(true);
        else CheckMark.SetActive(false);
    }
}
