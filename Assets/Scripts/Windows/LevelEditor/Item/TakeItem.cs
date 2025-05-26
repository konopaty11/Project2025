using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TakeItem : MonoBehaviour
{
    [SerializeField] GameObject prefabItem;

    bool isReady = false;

    LayerMask layer;
    ManagerLevel managerLvl;

    private void Start()
    {
        layer = LayerMask.GetMask("Spawn");
        managerLvl = FindObjectOfType<ManagerLevel>();
    }

    public void OnBtnClick()
    {
        isReady = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isReady)
        {
            // �������� ���� �� ������ � ����� �����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // �������� ����������� � 3D �����������
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                // ����� �������� ���� ���� � ����� ����� ��������� 
                //spawnPosition = hit.point + Vector3.back * 0.2f;
                GameObject item = Instantiate(prefabItem, managerLvl.Parent.transform);
                Vector3 spawnPosition = hit.point + Vector3.up * 0.2f + Vector3.back * 0.2f;
                item.transform.position = spawnPosition;

                ManagerItems.Items.Add(item);
            }

            isReady = false;
        }
    }
}