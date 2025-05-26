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
        // ���� ��������� �������� ��� - �����
        if (Items.Count == 0) return;

        // ��������
        if (Input.GetKey(KeyCode.R) || IsRotate) Item.transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
        // �������� ��������
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z)) DestroyItem();
    }

    /// <summary>
    /// �������� ��������� ��������
    /// </summary>
    public void DestroyItem()
    {
        Destroy(Item);
        Items.Remove(Item);
    }
}

