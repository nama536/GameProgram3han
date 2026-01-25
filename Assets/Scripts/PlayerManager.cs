using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public enum Event
    {
        normalDice,//通常の進行状態
        sixDice,//ハイリスクのサイコロしか触れない状態
        stopDice,//一の目が出るまで動けない状態
        beforeDice//ひとつ前のサイコロに強制させる状態
    }
    public Event thisEvent;

    public enum PlayerCount
    {
        PlayerOne,
        PlayerTwo
    }
    public PlayerCount thisPlayerCount;

    public enum BeforeDice
    {
        NormalDice,
        HighRisk
    }
    public BeforeDice thisBeforeDice;

    private TurnManager turnManager; 
    private Dice diceManager;

    /*void Start()
    {
        diceManager = FindFirstObjectByType<Dice>();
        turnManager = FindFirstObjectByType<TurnManager>();
    }

    public void OnDice(InputValue input)
    {
        if(thisPlayerCount == PlayerCount.PlayerOne && turnManager.NowTurn == TurnManager.Turn.PlayerOne)
        {
            diceManager.OnDice(thisEvent);
        }
        else if(thisPlayerCount == PlayerCount.PlayerTwo && turnManager.NowTurn == TurnManager.Turn.PlayerTwo)
        {
            diceManager.OnDice(thisEvent);
        }
    }
    public void OnSixDice(InputValue input)
    {
        if(thisPlayerCount == PlayerCount.PlayerOne && turnManager.NowTurn == TurnManager.Turn.PlayerOne)
        {
            diceManager.OnSixDice(thisEvent);
        }
        else if(thisPlayerCount == PlayerCount.PlayerTwo && turnManager.NowTurn == TurnManager.Turn.PlayerTwo)
        {
            diceManager.OnSixDice(thisEvent);
        }
    }*/
}
