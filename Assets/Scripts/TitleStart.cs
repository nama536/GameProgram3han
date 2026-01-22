using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleStart : MonoBehaviour
{
    //タイトルマネージャーから変数プレイヤーデータマネージャーを持ってくる
    public TitleManager[] PlayerDataManagers;

    // Start is called before the first frame update
    void Start()
    {
     PlayerDataManagers[0].PlayerDevice = null; //初期化
     PlayerDataManagers[1].PlayerDevice = null;
     JoinDevices();
     ChangeScene();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void JoinDevices()
    {
        
        foreach(var device in InputSystem.devices)
         if(device.name.Contains("Gamepad"))
            {
                if(PlayerDataManagers[0].PlayerDevice == null)
                {
                    PlayerDataManagers[0].PlayerDevice =device;
                    Debug.Log(PlayerDataManagers[0].PlayerDevice);
                    Debug.Log("プレイヤー１");
                }
                else if(PlayerDataManagers[1].PlayerDevice == null)
                {
                    PlayerDataManagers[1].PlayerDevice =device;
                     Debug.Log(PlayerDataManagers[1].PlayerDevice);
                    Debug.Log("プレイヤー２");
                }
            }
       /* {
            //プレイヤーのオブジェクトを生成する
            PlayerInput.Instantiate(playerData[playerCount].PlayerPrefab,pairWithDevice: device);
            //プレイヤーのデータ群にデバイス情報を登録
            playerData[playerCount].MachDevice = device;
            //プレイヤーの人数カウントを＋１しました。
            playerCount++;
            //Debug.Log(playerCount);
        }*/
    }
     void ChangeScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}
