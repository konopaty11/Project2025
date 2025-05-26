using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeType : MonoBehaviour
{
    [SerializeField] Upgrades upgrade;
    public Upgrades Upgrade { get => upgrade; set => upgrade = value; }
}
