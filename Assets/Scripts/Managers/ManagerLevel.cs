using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerLevel : MonoBehaviour
{
    [SerializeField] GameObject parent;   // родитель для объектов уровня
    public GameObject Parent => parent;

    public int ActiveLevel { get; set; } = -1;  // выбранный уровень в списке уровней
    public int LoadLevel { get; private set; } = -1;  // загруженный уровень
    public int CurrentLevel { get; private set; } = 0;  // текущий уровень
    public int CountLevels { get; private set; }    // общее кол-во уровней

    string fileName = "LevelDatas.json";
    string path;

    LevelDatas levels;
    Object objects;
    CarSettings carSettings;
    
    /// <summary>
    /// десериализация json
    /// </summary>
    private void Start()
    {
        path = Path.Combine(Application.streamingAssetsPath, fileName);
        string data = File.ReadAllText(path);
        levels = JsonConvert.DeserializeObject<LevelDatas>(data);
        CountLevels = levels.ArrayOfLevelData.LevelData.Count;

        objects = FindObjectOfType<Object>();
        carSettings = FindObjectOfType<CarSettings>();
    }

    /// <summary>
    /// загрузка уровня
    /// </summary>
    public void Load()
    {
        if (LoadLevel == CurrentLevel) return;

        DestroyObjects();   
        // размещение объектов уровня на сцене
        foreach (ObjectData el in levels.ArrayOfLevelData.LevelData[CurrentLevel].Objects.ObjectData)
        {
            Vector3 position = new Vector3(el.Position.X, el.Position.Y, el.Position.Z);
            Vector3 rotation = new Vector3(el.Rotation.X, el.Rotation.Y, el.Rotation.Z);
            Vector3 scale = new Vector3(el.Scale.X, el.Scale.Y, el.Scale.Z);

            GameObject obj = Instantiate(objects.GetObject((Objects)el.ObjectType), position, Quaternion.Euler(rotation));
            obj.transform.SetParent(parent.transform);
            obj.transform.localScale = scale;
        }

        LoadLevel = CurrentLevel;

        // активация настроек 
        //Debug.Log(levels.ArrayOfLevelData.LevelData[CurrentLevel].CarData.CarModelIndex);
        carSettings.SetSettings(levels.ArrayOfLevelData.LevelData[CurrentLevel].CarData);
    }

    /// <summary>
    /// удаление уровня
    /// </summary>
    public void Remove()
    {
        if (ActiveLevel == -1) return; 
        // если удаляемый уровень загружен на сцене - удалить его
        if (ActiveLevel == LoadLevel) DestroyObjects();

        // удаление уровня из списка
        levels.ArrayOfLevelData.LevelData.RemoveAt(ActiveLevel);

        string data = JsonConvert.SerializeObject(levels,Formatting.Indented);
        File.WriteAllText(path, data);

        // перерасчёт уровней в списке уровней
        CountLevels--;
        LevelSettings settingsLvl = FindObjectOfType<LevelSettings>();
        settingsLvl.CreateLevelInListLevels();
    }

    /// <summary>
    /// добавление уровней
    /// </summary>
    public void Add()
    {
        CountLevels++;

        // создание шаблона уровня и добавление в список 
        LevelData lvlData = new LevelData()
        {
            Objects = new LevelObjects() { ObjectData = new List<ObjectData>() },
            CarData = new CarData()
        };
        levels.ArrayOfLevelData.LevelData.Add(lvlData);

        LevelSettings settingsLvl = FindObjectOfType<LevelSettings>();
        settingsLvl.CreateLevelInListLevels();
    }

    /// <summary>
    /// сохраняет левал
    /// </summary>
    public void Save()
    {
        if (LoadLevel != CurrentLevel) return;

        // сохранение объектов левала
        for (int i = parent.transform.childCount - 1; i >= 0; i--)
        {
            GameObject obj = parent.transform.GetChild(i).gameObject;
            Objects lvlObj = objects.GetObject(parent.transform.GetChild(i).gameObject);

            ObjectData objData = new ObjectData();
            objData.ObjectType = (int) lvlObj;
            objData.Position = new Position() 
            { 
                X = obj.transform.position.x,
                Y = obj.transform.position.y,
                Z = obj.transform.position.z
            };
            objData.Rotation = new Rotation()
            {
                X = obj.transform.rotation.x,
                Y = obj.transform.rotation.y,
                Z = obj.transform.rotation.z
            };
            objData.Scale = new Scale()
            {
                X = obj.transform.localScale.x,
                Y = obj.transform.localScale.y,
                Z = obj.transform.localScale.z
            };

            levels.ArrayOfLevelData.LevelData[LoadLevel].Objects.ObjectData.Add(objData);
        }

        // сохранение настроек левала
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.CarModelIndex = (int)carSettings.ActiveCar - 1;
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.HasPropeller = carSettings.Propeller;
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.HasRocket = carSettings.Rocket;
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.HasSpikedWheels = carSettings.SpikedWheels;
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.HasWings = carSettings.Wings;

        // запись 
        string data = JsonConvert.SerializeObject(levels, Formatting.Indented);
        File.WriteAllText(path, data);
    }

    /// <summary>
    /// удаляет объекты уровня на сцене 
    /// </summary>
    public void DestroyObjects()
    {
        for (int i = parent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }
}
