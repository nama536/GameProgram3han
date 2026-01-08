using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceManager : MonoBehaviour
{
    //プレイヤー1がプレイヤー2か
    public enum PlayerNumber
    {
        One,
        Two
    }
    //このスクリプトがついてるオブジェクトがプレイヤー1か2か
    PlayerNumber _thisPlayerNumber;

    //マップ上のプレイヤーの位置
    int _playerPosition = 0;

    MapManager _mapManager;
    TurnManager _turnManager;

    void Start()
    {
        _mapManager = FindFirstObjectByType<MapManager>();
        _turnManager = FindFirstObjectByType<TurnManager>();

        DoDice(5);//仮
    }

    //ダイスが振られたら(ダイスの数)
    public void DoDice(int diceNumber)
    {
        //今のプレイヤーのマスを保存
        int nowPlayerPosition = _playerPosition;

        //プレイヤー1のスクリプトで今がプレイヤー1のターンなら
        if(_thisPlayerNumber == PlayerNumber.One && _turnManager.NowTurn == TurnManager.Turn.PlayerOne)
        {
            StartCoroutine(_mapManager.MovePlayer(nowPlayerPosition,diceNumber,_thisPlayerNumber));
            //プレイヤーのマスを更新
            _playerPosition += diceNumber;
        }
        //プレイヤー1のスクリプトで今がプレイヤー1のターンなら
        else if(_thisPlayerNumber == PlayerNumber.Two && _turnManager.NowTurn == TurnManager.Turn.PlayerTwo)
        {
            StartCoroutine(_mapManager.MovePlayer(nowPlayerPosition,diceNumber,_thisPlayerNumber));
            //プレイヤーのマスを更新
            _playerPosition += diceNumber;
        }

        Debug.Log("プレイヤーは" + _playerPosition);
    }
}
