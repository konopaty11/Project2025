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
    /// возвращает словарь настроек уровня из списка по индексу
    /// </summary>
    /// <param name="ind"> индекс в списке </param>
    /// <returns> словарь настроек </returns>
    /// <exception cref="LevelIsNotLoaded"> бросается при передачи в качестве индекса -1 </exception>
    public static Dictionary<GameObject, bool> GetUpgradeData(int ind)
    {
        index = ind;
        try
        {
            if (index == -1) throw new LevelIsNotLoaded("Уровень не был загружен. Однако ты пытаешься начать игру.");
            return levelsUpgrades[index];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// добавляет ключ и значение в словарь 
    /// </summary>
    /// <param name="key"> ключ </param>
    /// <param name="value"> значение </param>
    public static void AddLvlUpgrades(GameObject key, bool value)
    {
        // если словаря нет создать его по текущему индексу
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
