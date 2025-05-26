using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using static BuingBG;

public enum Backgrounds
{
    None,
    Blue,
    Red,
    Yellow
}

public class ManagerBackgrounds : MonoBehaviour
{
    [SerializeField] Image activeImage;

    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material blueMat;
    [SerializeField] Material redMat;
    [SerializeField] Material yellowMat;

    // выбранный фон
    public Backgrounds SelectedBackground { get; set; }
    // активная галочка
    public GameObject ActiveCheckMark { get; set; }
    
    public Sprite ActiveSprite { set { activeImage.sprite = value; } }

    public void SetBackground(Backgrounds bg)
    {
        SelectedBackground = bg;
        switch (bg)
        {
            case Backgrounds.Blue:
                mesh.material = blueMat;
                break;
            case Backgrounds.Red:
                mesh.material = redMat;
                break;
            case Backgrounds.Yellow:
                mesh.material = yellowMat;
                break;
        }
    }
}
