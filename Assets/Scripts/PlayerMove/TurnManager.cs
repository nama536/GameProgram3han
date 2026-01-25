using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private InputDevice[] _gamepads = new InputDevice[2];

    [SerializeField] MapManager _mapManager;
    public bool hasRolled = false;

    void Start()
    {
        //初期設定
        _uiAction.neverAutoSwitchControlSchemes = true;
        _uiAction.SwitchCurrentActionMap("Select");
        //コントローラーの情報保存
        _gamepads[0] = _mapManager.PlayerDataManagers[0].PlayerDevice;
        _gamepads[1] = _mapManager.PlayerDataManagers[1].PlayerDevice;
        //プレイヤー１のコントローラーにする
        _uiAction.SwitchCurrentControlScheme(_gamepads[0]);
        _uiAction.actions.devices = new InputDevice[] { _gamepads[0] };
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
                _uiAction.actions.devices = new InputDevice[] { _gamepads[1] };
                break;
            case Turn.PlayerTwo:
                NowTurn = Turn.PlayerOne;
                _turnText.text = "プレイヤー１のターン";
                _turnText.color = Color.black;
                _uiAction.actions.devices = new InputDevice[] { _gamepads[0] };
                break;
        }
        DiceMenu.SetActive(true); 

        _mapManager.Processing = false;//テスト用
        GameObject firstButton = DiceMenu.GetComponentInChildren<Button>().gameObject;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
