using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Upgrades
{
    None,
    SpikedWheels,
    Rocket,
    Propeller,
    Wings
}

public enum Car
{
    None,
    Banana,
    Can,
    MilkCarton
}

public class CarSettings : MonoBehaviour
{
    public Car ActiveCar { get; set; }

    public bool SpikedWheels { get; set; }
    public bool Rocket { get; set; }
    public bool Propeller { get; set; }
    public bool Wings { get; set; }

    public virtual GameObject CheckMark { get; set; }

    /// <summary>
    /// применяет настройки загруженного уровня
    /// </summary>
    /// <param name="carData"> настройки транспорта </param>
    public void SetSettings(CarData carData)
    {
        ActiveCar = (Car)carData.CarModelIndex  + 1;
        SpikedWheels = carData.HasSpikedWheels;
        Rocket = carData.HasRocket;
        Propeller = carData.HasPropeller;
        Wings = carData.HasWings;

        CarSettings[] models = FindObjectsOfType<SetCarModel>(true);
        CarSettings[] upgrades = FindObjectsOfType<SetCarUpgrade>(true);
        List<CarSettings> settings = new();
        settings.AddRange(models.ToList());
        settings.AddRange(upgrades.ToList());
        foreach (CarSettings setting in settings)
            setting.UpdateSettings();
    }

    /// <summary>
    /// активация галочки при загрузке уровня
    /// </summary>
    public virtual void UpdateSettings() { }

}
