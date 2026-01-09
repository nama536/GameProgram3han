using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    [SerializeField] MapManager _mapManager;

    public void TurnChange()
    {
        //今と逆のプレイヤーにターンを変更
        switch (NowTurn)
        {
            case Turn.PlayerOne:
                NowTurn = Turn.PlayerTwo;
                break;
            case Turn.PlayerTwo:
                NowTurn = Turn.PlayerOne;
                break;
        }

        _mapManager.Processing = false;//テスト用
    }
}
