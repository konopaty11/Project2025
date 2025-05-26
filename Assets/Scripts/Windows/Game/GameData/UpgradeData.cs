using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeData
{
    static List<Dictionary<GameObject, bool>> levelsUpgrades = new();
    static int index;

    /// <summary>
    /// ���������� ������� �������� ������ �� ������ �� �������
    /// </summary>
    /// <param name="ind"> ������ � ������ </param>
    /// <returns> ������� �������� </returns>
    /// <exception cref="LevelIsNotLoaded"> ��������� ��� �������� � �������� ������� -1 </exception>
    public static Dictionary<GameObject, bool> GetUpgradeData(int ind)
    {
        index = ind;
        try
        {
            if (index == -1) throw new LevelIsNotLoaded("������� �� ��� ��������. ������ �� ��������� ������ ����.");
            return levelsUpgrades[index];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// ��������� ���� � �������� � ������� 
    /// </summary>
    /// <param name="key"> ���� </param>
    /// <param name="value"> �������� </param>
    public static void AddLvlUpgrades(GameObject key, bool value)
    {
        // ���� ������� ��� ������� ��� �� �������� �������
        if (levelsUpgrades.Count > index)   
        {
            levelsUpgrades[index].Add(key, value);
        }
        else
        {
            levelsUpgrades.Add(new Dictionary<GameObject, bool>() 
            { 
                { key, value } 
            });
        }
    }

    //public static void Print()
    //{
    //    foreach(Dictionary<GameObject, bool> elem in levelsUpgrades)
    //    {
    //        foreach(var (k, v) in elem)
    //        {
    //            Debug.Log($"{k} {v}");
    //        }
    //    }
    //}
}
