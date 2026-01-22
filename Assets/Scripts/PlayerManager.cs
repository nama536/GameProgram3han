using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerCount
    {
        PlayerOne,
        PlayerTwo
    }
    public PlayerCount thisPlayerCount;

    [SerializeField] PlayerInput _playerinput;
    private TurnManager turnManager; 
    private Dice diceManager;

    // Start is called before the first frame update
    void Start()
    {
        diceManager = FindFirstObjectByType<Dice>();
        turnManager = FindFirstObjectByType<TurnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDice(InputValue input)
    {
        if(thisPlayerCount == PlayerCount.PlayerOne && turnManager.NowTurn == TurnManager.Turn.PlayerOne)
        {
        diceManager.OnDice();
        }
        else if(thisPlayerCount == PlayerCount.PlayerTwo && turnManager.NowTurn == TurnManager.Turn.PlayerTwo)
        {
        diceManager.OnDice();
        }
    }
    public void OnSixDice(InputValue input)
    {
        if(thisPlayerCount == PlayerCount.PlayerOne && turnManager.NowTurn == TurnManager.Turn.PlayerOne)
        {
        diceManager.OnSixDice();
        }
        else if(thisPlayerCount == PlayerCount.PlayerTwo && turnManager.NowTurn == TurnManager.Turn.PlayerTwo)
        {
        diceManager.OnSixDice();
        }
    }
}
