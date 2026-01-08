using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum Turn
    {
        PlayerOne,
        PlayerTwo
    }
    //今がどちらのターンなのか
    public Turn NowTurn;
}
