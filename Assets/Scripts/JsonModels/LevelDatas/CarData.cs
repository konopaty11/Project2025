using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class CarData
{
    public int CarModelIndex;

    public bool HasSpikedWheels;

    public bool HasRocket;

    public bool HasPropeller;

    public bool HasWings;
}
