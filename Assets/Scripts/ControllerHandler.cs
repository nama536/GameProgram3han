using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement;

public class ControllerHandler : MonoBehaviour
{
    //接続されているデバイス情報を配列（複数個扱える）形で取得する。
    public List<InputDevice> joinDevices = new List<InputDevice>();
    //プレイヤーのキャラクターを設定するための変数
    public GameObject PlayerObject = null;

    // Start is called before the first frame update
    //1.ゲーム開始時に接続されているコントローラーを変数joinDeviceにすべて格納
    //2.変数joiDeviceに格納されている数だけプレイヤーオブジェクトを作る
    //3.作ったオブジェクトをコントローラーと接続する。
    //プレイヤーデータの取得
    public List<PlayerData> playerData = new();
    //部屋の中にいる人数
    private int playerCount = 0;
    void Start()
    {
        //このオブジェクトはシーンを変更しても消さないでください
        DontDestroyOnLoad(this.gameObject);
        //foreachは配列のような複数の変数がある場合に使える
        //配列の戦闘から変数の中身（値）を一つずつ取得する
        //Input.System.Device（PCに接続されているデバイスの情報群を）
        foreach(var device in InputSystem.devices)
        {
            //接続されているデバイスの中でコントローラーのみ取得
            if(device.name.Contains("Gamepad"))
            {
                Debug.Log(device.name);
                joinDevices.Add(device);
            }
            /*//もしデバイスの名前がKeyboardもしくはMouseじゃなかったら
            if(!(device.name == "Keyboard" || device.name == "Mouse" || device.name == "Pen"))
            {
                //取り出した値（デバイス情報）の中から名前（Name）の情報をコンソールに出す
                Debug.Log(device.name);

                //変数joinDeviceにdevice（コントローラー情報）を格納する
                joinDevices.Add(device);
                //return;
                //もしデバイスの名前がKeyboardもしくはMouseだったらこれ以下の処理をしない
            }*/
        }
        //プレイヤーオブジェクトを生成する
        JoinPlayer();
        //Sceneを切り替える処理をする
        ChangeScene();
    }
    
    //2.変数joiDeviceに格納されている数だけプレイヤーオブジェクトを作る
    void JoinPlayer()
    {
        //変数joiDeviceの数だけプレイヤーを作る。
        foreach(var device in joinDevices)
        {
            //プレイヤーのオブジェクトを生成する
            PlayerInput.Instantiate(playerData[playerCount].PlayerPrefab,pairWithDevice: device);
            //プレイヤーのデータ群にデバイス情報を登録
            playerData[playerCount].MachDevice = device;
            //プレイヤーの人数カウントを＋１しました。
            playerCount++;
            //Debug.Log(playerCount);
        }
    }

    //シーンを切り替える処理を書く
    void ChangeScene()
    {
        SceneManager.LoadScene("InGame");
    }

}