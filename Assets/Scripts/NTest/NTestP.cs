using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestD;

public class NTestP : MonoBehaviour
{
    //他のクラスの処理を呼び出す方法
    //1.継承　自信を継承元の派生クラスにすることで継承元の機能を扱う
    //2.グローバル変数化　グローバル変数化としてクラスを呼び出す
    //3.using追加　namespace(名前空間)に登録する

    void Start()
    {
        NTestD.DiceRoll();
    }
}
