using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] GameObject prefabLevel;
    [SerializeField] RectTransform parent;

    // максимальное кол-во левелов
    // без необходимости увеличения высоты parent
    const int maxCountLvl = 4;
    float stepY = -70f;     // разница между левалами по y
    float startY = -32.5f;  // позиция первого левала по y
    float x = 8.5f;         // позиция левелов по x

    Vector2 anchor = new Vector2(0.5f, 1);  // якори леволов

    float heightParent; // высота parent

    ManagerLevel managerLvl;


    void Start()
    {
        managerLvl = FindObjectOfType<ManagerLevel>();
        CreateLevelInListLevels();
    }

    /// <summary>
    /// Создание префабов уровней 
    /// настройка их якорей и текста 
    /// </summary>
    public void CreateLevelInListLevels()
    {
        DeleteLevelInListLvls();

        heightParent = parent.sizeDelta.y;
        for (int i = 0; i < managerLvl.CountLevels; i++)
        {
            GameObject level = Instantiate(prefabLevel, parent);
            RectTransform rt = level.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(x, startY + i * stepY, 0);
            rt.anchorMax = anchor;
            rt.anchorMin = anchor;

            Text text = level.GetComponentInChildren<Text>();
            text.text = $"Level {i + 1}";

            if (i > maxCountLvl) parent.sizeDelta = new Vector2(parent.sizeDelta.x, heightParent + 70);
        }
    }

    void DeleteLevelInListLvls()
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
