using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
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

    [SerializeField] TextMeshProUGUI _turnText;

    [SerializeField] MapManager _mapManager;
    public bool hasRolled = false;

    public void TurnChange()
    {
        hasRolled = false;
        //今と逆のプレイヤーにターンを変更
        switch (NowTurn)
        {
            case Turn.PlayerOne:
                NowTurn = Turn.PlayerTwo;
                _turnText.text = "プレイヤー２のターン";
                _turnText.color = Color.blue;
                break;
            case Turn.PlayerTwo:
                NowTurn = Turn.PlayerOne;
                _turnText.text = "プレイヤー１のターン";
                _turnText.color = Color.black;
                break;
        }

        _mapManager.Processing = false;//テスト用
    }
}
