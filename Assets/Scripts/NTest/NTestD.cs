using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NTestDを名前空間TestDに設定
namespace TestD
{
    public class NTestD
    {
        //1.乱数を生成する処理
        //2.出た乱数を6個に分ける処理(抽選)
        //3.1~6の中で何の目が出たか情報を戻り値で渡す
        public static void DiceRoll()
        {
            //乱数を生成する
            //Random.Range(min,max)
            //実際に出る値は min ~ max-1
            int rand = UnityEngine.Random.Range(0,100);

            //乱数を1~6のグループに分ける
            //1.出てきた値が n以上 n'以下 だったらグループAに設定する
            //2.乱数を分けたい値で割った余りを返す処理

            //各ダイス側は乱数を受け取って値を返すだけ
            int Value = Dice(rand);
            Debug.Log(Value);
        }

        //[public 型宣言 関数名]という書き方
        //型宣言 = void :関数の終了時に値を返さない
        //型宣言 = int :関数の終了時にint型で値を返してくれる

        //関数の終了時に特定の値を戻すことを戻り値という

        //確率が偏っているイカサマダイス
        public static int TrickDice(int rand)
        {
            //乱数の値が90より大きかったら1を値として戻す
            if(rand > 90)
            {
                return 1;
            }
            //乱数の値が90以下で80よりも大きかったら2を値として戻す
            else if(rand > 80)
            {
                return 2;
            }
            //乱数の値が80以下で70よりも大きかったら3を値として戻す
            else if(rand > 70)
            {
                return 3;
            }
            //乱数の値が70以下で60よりも大きかったら3を値として戻す
            else if(rand > 60)
            {
                return 4;
            }
            //乱数の値が60以下で50よりも大きかったら3を値として戻す
            else if(rand > 50)
            {
                return 5;
            }
            //乱数の値が50以下
            else
            {
                return 6;
            }
        }

        //均等な確率のダイス
        public static int Dice(int rand)
        {
            //確率が均等な6面ダイス
            //6で割っているので0,1,2,3,4,5のいずれかが返る
            int value = rand % 6;

            //出てきた値によって戻り値として返す値を変更
            switch (value)
            {
                case 0:
                return 1;

                case 1:
                return 2;

                case 2:
                return 3;

                case 3:
                return 4;

                case 4:
                return 5;

                case 5:
                return 6;

                //0~5以外の数値
                default:
                return 0;
            }
        }
    }
}
