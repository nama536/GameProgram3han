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

    public Sprite newSprite; // インスペクターで設定するスプライト
    private SpriteRenderer spriteRenderer;

    //サイコロの出目画像
    public Sprite one; // 1
    public Sprite two; // 2
    public Sprite three; // 3
    public Sprite four; // 4
    public Sprite five; // 5
    public Sprite six; // 6

    
    public Sprite Riskone; // -1
    public Sprite Risktwo; // -2
    public Sprite Riskthree; // 3
    public Sprite Riskfour; // -4
    public Sprite Risksix; // 6
    //-------------------------------------
     void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRendererコンポーネントを取得
        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite; // スプライトを割り当てて表示
        }
    }

    void Update()
    {
        
    }
    //普通のサイコロ
    private void OnDice(InputValue input)
    {
        //均等なダイス（1から最大値−１の間で生成）
        dicevalue = UnityEngine.Random.Range(1,7);

        if(dicevalue == 1)
        {
            spriteRenderer.sprite == one;
        }

        else if(dicevalue == 2)
        {
            spriteRenderer.sprite == two;
        }

        else if(dicevalue == 3)
        {
            spriteRenderer.sprite == three;
        }

        else if(dicevalue == 4)
        {
            spriteRenderer.sprite == four;
        }

        else if(dicevalue == 5)
        {
            spriteRenderer.sprite == five;
        }

        else if(dicevalue == 6)
        {
            spriteRenderer.sprite == six;
        }

        Debug.Log(dicevalue);
    }
//-----------------------------------------------------------

    //ハイリスクのサイコロ
    private void OnSixDice(InputValue input)
    {
        sixdicevalue = UnityEngine.Random.Range(1,7);
        Select();
    }

    //100分率に変換してサイコロの出目を調整
    void Select()
    {
        //1/100
        if(sixdicevalue <= 1) //乱数で出てきた値が10以下なら 1 と判断
        {
            View(-1);
        }
        //1/100
        else if(sixdicevalue <= 2) //乱数で出てきた値が20以下 2 と判断
        {
            View(3);
        }
        //1/100
        else if(sixdicevalue <= 3) //乱数で出てきた値が30以下なら 3 と判断
        {
            View(-2);
        }
        //1/100
        else if(sixdicevalue <= 4) //乱数で出てきた値が40以下なら 4 と判断
        {
            View(6);
        }
        //1/100
        else if(sixdicevalue <= 5) //乱数で出てきた値が50以下なら 5 と判断
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
