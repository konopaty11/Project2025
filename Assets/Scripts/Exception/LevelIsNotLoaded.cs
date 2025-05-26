using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIsNotLoaded : ArgumentOutOfRangeException
{
    public LevelIsNotLoaded(string msg) : base(msg) 
    {
        Debug.Log("sdf");
        Debug.LogWarning(msg);
    }
}
