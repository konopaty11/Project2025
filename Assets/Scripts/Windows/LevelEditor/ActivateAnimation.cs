using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    MoveCamera moveCam;

    void Awake()
    {
        moveCam = FindObjectOfType<MoveCamera>();
    }

    /// <summary>
    /// активация анимации перемещения камеры для постройки уровня
    /// и активация функционала по её перемещению
    /// </summary>
    void OnEnable()
    {
        moveCam.CurrencyTime = 0;

        moveCam.IsAnimation = true;
        moveCam.IsAnimationReturn = false;
        moveCam.IsLevelEditorActive = true;
    }

    /// <summary>
    /// активация анимации перемещения камеры на главный стол
    /// </summary>
    void OnDisable()
    {
        moveCam.CurrencyTime = 0;

        moveCam.IsAnimation = false;
        moveCam.IsAnimationReturn = true;
        moveCam.IsLevelEditorActive = false;
    }
}
