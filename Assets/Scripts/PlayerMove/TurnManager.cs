using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public enum Turn
    {
        PlayerOne,
        PlayerTwo
    }
    //今がどちらのターンなのか
    public Turn NowTurn;
    public GameObject DiceMenu;

    [SerializeField] TextMeshProUGUI _turnText;
    [SerializeField] PlayerInput _uiAction;

    [SerializeField] MapManager _mapManager;
    public bool hasRolled = false;

    void Start()
    {
        _uiAction.neverAutoSwitchControlSchemes = true;
        _uiAction.SwitchCurrentControlScheme(_mapManager.PlayerDataManagers[0].PlayerDevice);
    }
    public void TurnChange()
    {
        DiceMenu.SetActive(false);
        hasRolled = false;
        //今と逆のプレイヤーにターンを変更
        switch (NowTurn)
        {  
            case Turn.PlayerOne:
                NowTurn = Turn.PlayerTwo;
                _turnText.text = "プレイヤー２のターン";
                _turnText.color = Color.blue;
                _uiAction.SwitchCurrentControlScheme(_mapManager.PlayerDataManagers[1].PlayerDevice);
                break;
            case Turn.PlayerTwo:
                NowTurn = Turn.PlayerOne;
                _turnText.text = "プレイヤー１のターン";
                _turnText.color = Color.black;
                _uiAction.SwitchCurrentControlScheme(_mapManager.PlayerDataManagers[0].PlayerDevice);
                break;
        }
        DiceMenu.SetActive(true); 

        _mapManager.Processing = false;//テスト用
        GameObject firstButton = DiceMenu.GetComponentInChildren<Button>().gameObject;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
