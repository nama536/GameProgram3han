using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class Dice : MonoBehaviour
{   
    private int dicevalue;
    private int sixdicevalue;

    //普通のサイコロ
    private void OnDice(InputValue input)
    {
        //均等なダイス（1から最大値−１の間で生成）
        dicevalue = UnityEngine.Random.Range(1,7);
        Debug.Log(dicevalue);
    }
//-----------------------------------------------------------

    //ハイリスクのサイコロ
    private void OnSixDice(InputValue input)
    {
        sixdicevalue = UnityEngine.Random.Range(1,101);
        Select();
    }

    //100分率に変換してサイコロの出目を調整
    void Select()
    {
        //1/100
        if(sixdicevalue <= 17) //乱数で出てきた値が10以下なら 1 と判断
        {
            View(-1);
        }
        //1/100
        else if(sixdicevalue <= 34) //乱数で出てきた値が20以下 2 と判断
        {
            View(3);
        }
        //1/100
        else if(sixdicevalue <= 51) //乱数で出てきた値が30以下なら 3 と判断
        {
            View(-2);
        }
        //1/100
        else if(sixdicevalue <= 68) //乱数で出てきた値が40以下なら 4 と判断
        {
            View(6);
        }
        //1/100
        else if(sixdicevalue <= 85) //乱数で出てきた値が50以下なら 5 と判断
        {
            View(-4);
        }
        //95/100
        else//乱数で出てきた値がその他(51以上)なら 6 と判断
        {
            View(6);
        }
    }

        //結果の表示
    void View(int sixdicevalue)
    {
        Debug.Log($"ダイスで{sixdicevalue}が出た!");
    }
}
