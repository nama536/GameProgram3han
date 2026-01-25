using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    //今がどちらのターンなのか　０がプレイヤー１　１がプレイヤー２
    public int NowTurn = 0;
    public GameObject DiceMenu;
    //０がノーマル１がハイリスク
    [SerializeField] Button[] _diceSelect;

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

        EventSystem.current.SetSelectedGameObject(_diceSelect[0].gameObject);
    }
    public void TurnChange()
    {
        DiceMenu.SetActive(false);
        hasRolled = false;
        //今と逆のプレイヤーにターンを変更
        switch (NowTurn)
        {  
            case 0:
                NowTurn = 1;
                _turnText.text = "プレイヤー２のターン";
                _turnText.color = Color.blue;
                _uiAction.actions.devices = new InputDevice[] { _gamepads[1] };
                break;
            case 1:
                NowTurn = 0;
                _turnText.text = "プレイヤー１のターン";
                _turnText.color = Color.black;
                _uiAction.actions.devices = new InputDevice[] { _gamepads[0] };
                break;
        }
        DiceMenu.SetActive(true); 
        EventCheck();
        _mapManager.Processing = false;//テスト用
    }

    void EventCheck()
    {
        switch (_mapManager.PlayerManager[NowTurn].thisEvent)
        {
            case PlayerManager.Event.normalDice:
                EventSystem.current.SetSelectedGameObject(_diceSelect[0].gameObject);
                _diceSelect[0].interactable = true;
                _diceSelect[1].interactable = true;
                break;
            case PlayerManager.Event.stopDice:
                _diceSelect[0].interactable = true;
                EventSystem.current.SetSelectedGameObject(_diceSelect[0].gameObject);
                _diceSelect[1].interactable = false;
                break;
            case PlayerManager.Event.sixDice:
                _diceSelect[1].interactable = true;
                EventSystem.current.SetSelectedGameObject(_diceSelect[1].gameObject);
                _diceSelect[0].interactable = false;
                break;
            case PlayerManager.Event.beforeDice:
                switch (_mapManager.PlayerManager[NowTurn].thisBeforeDice)
                {
                    case PlayerManager.BeforeDice.NormalDice:
                        _diceSelect[0].interactable = true;
                        EventSystem.current.SetSelectedGameObject(_diceSelect[0].gameObject);
                        _diceSelect[1].interactable = false;
                        break;
                    case PlayerManager.BeforeDice.HighRisk:
                        _diceSelect[1].interactable = true;
                        EventSystem.current.SetSelectedGameObject(_diceSelect[1].gameObject);
                        _diceSelect[0].interactable = false;
                        break;
                }
                break;
        }
    }
}
