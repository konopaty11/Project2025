using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTran;

    public Transform TargetCar { get; set; }
    public bool IsFollow { get; set; } = false;
    public bool IsAnimation { get; set; } = false; 
    public bool IsAnimationReturn { get; set; } = false;
    public bool IsLevelEditorActive { get; set; } = false;

    float currencyTime = 0;
    public float CurrencyTime { set => currencyTime = 0; }

    Vector3 targetPos = new Vector3(10f, 2f, -8f);
    Vector3 startPos;
    Quaternion startRot;

    bool isCreateUpgradesOutline = false;

    void Start()
    {
        startPos = cameraTran.position;
        startRot = cameraTran.rotation;
    }

    void Update()
    {
        if (IsAnimation)    // перемещение камеры на постройку уровня
        {
            float deltaTime = Time.deltaTime;
            cameraTran.position = Vector3.Lerp(cameraTran.position, targetPos, deltaTime);
            cameraTran.rotation = Quaternion.Lerp(cameraTran.rotation, Quaternion.LookRotation(Vector3.forward), deltaTime);
            currencyTime += Time.deltaTime;

            if (currencyTime >= 3f) IsAnimation = false;
        }
        else if (IsAnimationReturn) // перемещение камеры на стартовый стол
        {
            float deltaTime = Time.deltaTime;
            cameraTran.position = Vector3.Lerp(cameraTran.position, startPos, deltaTime);
            cameraTran.rotation = Quaternion.Lerp(cameraTran.rotation, startRot, deltaTime);
            currencyTime += Time.deltaTime;

            if (currencyTime >= 3f) IsAnimationReturn = false;
        }
        else if (IsLevelEditorActive)   // перемещение камеры по кнопкам 
        {
            float deltaTime = Time.deltaTime;

            Vector3 targetVect = cameraTran.position;
            if (Input.GetKey(KeyCode.W)) targetVect = cameraTran.position + Vector3.up;
            else if (Input.GetKey(KeyCode.S)) targetVect = cameraTran.position + Vector3.down;
            else if (Input.GetKey(KeyCode.A)) targetVect = cameraTran.position + Vector3.left;
            else if (Input.GetKey(KeyCode.D)) targetVect = cameraTran.position + Vector3.right;

            cameraTran.position = Vector3.Lerp(cameraTran.position, targetVect, 3 * deltaTime);
        }
        else if (IsFollow && TargetCar != null) // следование камеры за транспортом
        {
            float deltaTime = Time.deltaTime;

            Vector3 pos = TargetCar.position + new Vector3(-1f, 2f, -4f);       // камеры со смещением от транспорта
            Vector3 resPos = Vector3.Lerp(cameraTran.position, pos, deltaTime); // результирующий вектор
            Vector3 differentPos = resPos - cameraTran.position;                // вектор разницы старого положения камеры и нового

            // если смещение камеры незначительно - создавать контуры улучшений
            if (Math.Abs(differentPos.x) + Math.Abs(differentPos.y) + Math.Abs(differentPos.z) < 0.001f && !isCreateUpgradesOutline)
            {
                Game game = FindObjectOfType<Game>();
                game.CreateUpgradesOutline();
                isCreateUpgradesOutline = true;
            }
            
            cameraTran.position = resPos;
            cameraTran.LookAt(TargetCar.position + Vector3.up);
        }
    }
}
