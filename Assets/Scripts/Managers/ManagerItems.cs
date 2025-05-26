using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerItems : MonoBehaviour
{
    public static GameObject Item => Items.Count != 0 ? Items[^1] : null;
    public static List<GameObject> Items { get; set; } = new();
    public static bool IsRotate { get; set; }


    void Update()
    {
        // если активного предмета нет - выход
        if (Items.Count == 0) return;

        // вращение
        if (Input.GetKey(KeyCode.R) || IsRotate) Item.transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
        // удаление элемента
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z)) DestroyItem();
    }

    /// <summary>
    /// Удаление активного элемента
    /// </summary>
    public void DestroyItem()
    {
        Destroy(Item);
        Items.Remove(Item);
    }
}

