using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable]
public class ObjectData
{
    public Position Position;

    public Rotation Rotation;

    public Scale Scale;

    public int ObjectType;
}
