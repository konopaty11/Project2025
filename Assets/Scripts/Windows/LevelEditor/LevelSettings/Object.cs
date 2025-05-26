using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Objects
{
    Window = 0,
    Picture = 7,
    CurvedGround = 2,
    BedDrawer = 4,
    Sticks = 8,
    Cups = 5,
    Door = 3,
    Shelf = 6,
    Pano = 1,
    WallClock = 11,
    UGround = 13,
    WallLamp = 9,
    FlatGround = 12,
    ShelfStack = 14,
    LowShelf = 15,
    WorkTable = 10,
}

public class Object : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject FlatGround;
    [SerializeField] GameObject CurvedGround;
    [SerializeField] GameObject UGround;
    [SerializeField] GameObject BedDrawer;
    [SerializeField] GameObject Sticks;
    [SerializeField] GameObject Cups;
    [SerializeField] GameObject Door;
    [SerializeField] GameObject LowShelf;
    [SerializeField] GameObject Pano;
    [SerializeField] GameObject WallClock;
    [SerializeField] GameObject Picture;
    [SerializeField] GameObject WallLamp;
    [SerializeField] GameObject Window;
    [SerializeField] GameObject ShelfStack;
    [SerializeField] GameObject Shelf;
    [SerializeField] GameObject WorkTable;

    

    /// <summary>
    /// Возвращает префаб объекта по элементу перечисления
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public GameObject GetObject(Objects obj)
    {
        switch (obj)
        {
            case Objects.FlatGround:
                return FlatGround;
            case Objects.CurvedGround:
                return CurvedGround;
            case Objects.UGround:
                return UGround;
            case Objects.BedDrawer:
                return BedDrawer;
            case Objects.Sticks:
                return Sticks;
            case Objects.Cups:
                return Cups;
            case Objects.Door:
                return Door;
            case Objects.LowShelf:
                return LowShelf;
            case Objects.Pano:
                return Pano;
            case Objects.WallClock:
                return WallClock;
            case Objects.Picture:
                return Picture;
            case Objects.WallLamp:
                return WallLamp;
            case Objects.Window:
                return Window;
            case Objects.ShelfStack:
                return ShelfStack;
            case Objects.Shelf:
                return Shelf;
            case Objects.WorkTable:
                return WorkTable;
            default:
                return FlatGround;
        }
    }

    /// <summary>
    /// Возвращает префаб объекта по элементу перечисления
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns> 
    public Objects GetObject(GameObject obj)
    {
        if (obj.CompareTag(FlatGround.tag)) return Objects.FlatGround;
        if (obj.CompareTag(CurvedGround.tag)) return Objects.CurvedGround;
        if (obj.CompareTag(UGround.tag)) return Objects.UGround;
        if (obj.CompareTag(BedDrawer.tag)) return Objects.BedDrawer;
        if (obj.CompareTag(Sticks.tag)) return Objects.Sticks;
        if (obj.CompareTag(Cups.tag)) return Objects.Cups;
        if (obj.CompareTag(Door.tag)) return Objects.Door;
        if (obj.CompareTag(LowShelf.tag)) return Objects.LowShelf;
        if (obj.CompareTag(Pano.tag)) return Objects.Pano;
        if (obj.CompareTag(WallClock.tag)) return Objects.WallClock;
        if (obj.CompareTag(Picture.tag)) return Objects.Picture;
        if (obj.CompareTag(WallLamp.tag)) return Objects.WallLamp;
        if (obj.CompareTag(Window.tag)) return Objects.Window;
        if (obj.CompareTag(ShelfStack.tag)) return Objects.ShelfStack;
        if (obj.CompareTag(Shelf.tag)) return Objects.Shelf;
        if (obj.CompareTag(WorkTable.tag)) return Objects.WorkTable;

        return Objects.FlatGround; 
    }

}
