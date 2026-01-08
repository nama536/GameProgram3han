using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PlayerDataScriptableObject")]
public class PlayerData : ScriptableObject
{
    //playerのId情報
    public int PlayerId = 0;
    //プレイヤーのデバイス情報
    public InputDevice MachDevice = null;
    //プレイヤーの選択キャラクター
    public GameObject PlayerPrefab = null; 
}
