using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerLevel : MonoBehaviour
{
    [SerializeField] GameObject parent;   // �������� ��� �������� ������
    public GameObject Parent => parent;

    public int ActiveLevel { get; set; } = -1;  // ��������� ������� � ������ �������
    public int LoadLevel { get; private set; } = -1;  // ����������� �������
    public int CurrentLevel { get; private set; } = 0;  // ������� �������
    public int CountLevels { get; private set; }    // ����� ���-�� �������

    string fileName = "LevelDatas.json";
    string path;

    LevelDatas levels;
    Object objects;
    CarSettings carSettings;
    
    /// <summary>
    /// �������������� json
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
    /// �������� ������
    /// </summary>
    public void Load()
    {
        if (LoadLevel == CurrentLevel) return;

        DestroyObjects();   
        // ���������� �������� ������ �� �����
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

        // ��������� �������� 
        //Debug.Log(levels.ArrayOfLevelData.LevelData[CurrentLevel].CarData.CarModelIndex);
        carSettings.SetSettings(levels.ArrayOfLevelData.LevelData[CurrentLevel].CarData);
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    public void Remove()
    {
        if (ActiveLevel == -1) return; 
        // ���� ��������� ������� �������� �� ����� - ������� ���
        if (ActiveLevel == LoadLevel) DestroyObjects();

        // �������� ������ �� ������
        levels.ArrayOfLevelData.LevelData.RemoveAt(ActiveLevel);

        string data = JsonConvert.SerializeObject(levels,Formatting.Indented);
        File.WriteAllText(path, data);

        // ���������� ������� � ������ �������
        CountLevels--;
        LevelSettings settingsLvl = FindObjectOfType<LevelSettings>();
        settingsLvl.CreateLevelInListLevels();
    }

    /// <summary>
    /// ���������� �������
    /// </summary>
    public void Add()
    {
        CountLevels++;

        // �������� ������� ������ � ���������� � ������ 
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
    /// ��������� �����
    /// </summary>
    public void Save()
    {
        if (LoadLevel != CurrentLevel) return;

        // ���������� �������� ������
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

        // ���������� �������� ������
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.CarModelIndex = (int)carSettings.ActiveCar - 1;
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.HasPropeller = carSettings.Propeller;
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.HasRocket = carSettings.Rocket;
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.HasSpikedWheels = carSettings.SpikedWheels;
        levels.ArrayOfLevelData.LevelData[LoadLevel].CarData.HasWings = carSettings.Wings;

        // ������ 
        string data = JsonConvert.SerializeObject(levels, Formatting.Indented);
        File.WriteAllText(path, data);
    }

    /// <summary>
    /// ������� ������� ������ �� ����� 
    /// </summary>
    public void DestroyObjects()
    {
        for (int i = parent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }
}
