using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine;

public class ManagerUserData : MonoBehaviour, IWritable
{
    string fileName = "UserData.json";
    string path;
    ManagerCoins managerCoins;

    public UserData user { get; private set; }

    /// <summary>
    /// установка монет и фонов из файла UserData
    /// </summary>
    private void Start()
    {
        path = Path.Combine(Application.streamingAssetsPath, fileName);
        string data = File.ReadAllText(path);

        UserData userData = JsonConvert.DeserializeObject<UserData>(data);
        user = userData; // присваивание экземпляру для сериализации экземпляр десериализации

        List<BuingBG> bgs = FindObjectsByType<BuingBG>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

        ManagerBackgrounds managerBG = FindObjectOfType<ManagerBackgrounds>();
        managerBG.SelectedBackground = userData.SelectBackground;

        foreach (BuingBG bg in bgs)
        {
            if (bg.Background == userData.SelectBackground) // установка выбранного фона
            {
                bg.Background = userData.SelectBackground;
                bg.Select();
            }
            else
                switch (bg.Background)  // установка купленного фона
                {
                    case Backgrounds.Blue:
                        if (userData.Blue == true) bg.SetActiveTrue();
                        break;
                    case Backgrounds.Red:
                        if (userData.Red == true) bg.SetActiveTrue();
                        break;
                    case Backgrounds.Yellow:
                        if (userData.Yellow == true) bg.SetActiveTrue();
                        break;
                }
        }

        // установка кол-ва монет
        managerCoins = FindObjectOfType<ManagerCoins>();
        managerCoins.SetCoins(userData.CountCoins);
    }

    /// <summary>
    /// сериализует user в UserData
    /// </summary>
    public void WriteData()
    {
        user.CountCoins = managerCoins.CountCoins;

        string data = JsonConvert.SerializeObject(user, Formatting.Indented);
        File.WriteAllText(path, data);
    }
}
