using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //プレイヤーのオブジェクト
    [SerializeField] GameObject[] _players;
    //マップ上のプレイヤーの位置
    int[] _playerPositions = new int[2];
    //プレイヤーが動いてる時に上に出るあと何マス進むかのテキスト
    [SerializeField] TextMeshProUGUI _playerMoveSpaceCountText;
    float movePresent = 0f;
    //ターンが今どちらか 0がプレイヤー1　1がプレイヤー2
    int _nowTurn;

    //マスの位置0~30
    [SerializeField] Transform[] _spaces;

    [SerializeField] TurnManager _turnManager;

    void Start()
    {
        StartCoroutine(MovePlayer(6));
    }

    void Update()
    {

    }

    //ダイスが振られたら(ダイスの目)
    public IEnumerator MovePlayer(int moveSpaceCount)
    {

        switch (_turnManager.NowTurn)
        {
            case TurnManager.Turn.PlayerOne:
                _nowTurn = 0;
                break;

            case TurnManager.Turn.PlayerTwo:
                _nowTurn = 1;
                break;
        }

        //プレイヤーが動いてる時に上に出るあと何マス進むかのテキストを表示
        _playerMoveSpaceCountText.enabled = true;
        _playerMoveSpaceCountText.text = moveSpaceCount.ToString();

        yield return new WaitForSeconds(1f);

        //ダイスがプラスの場合
        if(moveSpaceCount > 0)
        {
            //残りの動くマス数が0じゃ無い限り
            while (moveSpaceCount != 0)
            {   
                //残りの進むマス数を減らしてプレイヤーの位置をプラス
                moveSpaceCount--;
                _playerPositions[_nowTurn]++;
                _playerMoveSpaceCountText.text = moveSpaceCount.ToString();

                Transform nowPlayerTransform = _players[_nowTurn].transform;//今のプレイヤーの位置保存
                bool nowMoobing = true;//今動いてるか

                //0.8秒でプレイヤーを次のマスへ移動させる
                while(movePresent < 0.8f)
                {
                    movePresent += Time.deltaTime;
                    _players[_nowTurn].transform.position = Vector3.Slerp(nowPlayerTransform.position,_spaces[_playerPositions[_nowTurn]].position,movePresent);
                    yield return null;//whileは1フレームの中で処理を行うためこれで1フレーム進めさせる
                }
                //パーセントをリセット
                if (nowMoobing)
                {
                    movePresent = 0f;
                    nowMoobing = false;
                }

                yield return new WaitForSeconds(0.2f);
            }
        }

        //ダイスがマイナスの場合
        else if(moveSpaceCount < 0)
        {
            //残りの動くマス数が0じゃ無くてプレイヤーの位置が0じゃない限り
            while (moveSpaceCount != 0 && _playerPositions[_nowTurn] != 0)
            {   
                //残りの進むマス数を減らしてプレイヤーの位置をプラス
                moveSpaceCount++;
                _playerPositions[_nowTurn]--;
                _playerMoveSpaceCountText.text = moveSpaceCount.ToString();

                Transform nowPlayerTransform = _players[_nowTurn].transform;//今のプレイヤーの位置保存
                bool nowMoobing = true;//今動いてるか

                //0.8秒でプレイヤーを次のマスへ移動させる
                while(movePresent < 0.8f )
                {
                    movePresent += Time.deltaTime;
                    _players[_nowTurn].transform.position = Vector3.Slerp(nowPlayerTransform.position,_spaces[_playerPositions[_nowTurn]].position,movePresent);
                    yield return null;//whileは1フレームの中で処理を行うためこれで1フレーム進めさせる
                }
                //パーセントをリセット
                if (nowMoobing)
                {
                    movePresent = 0f;
                    nowMoobing = false;
                }

                yield return new WaitForSeconds(0.2f);
            }
        }

        //残りの進むマス数の表示を消す
        _playerMoveSpaceCountText.enabled = false;

        SpaceCheck(_nowTurn);
    }

    //現在のマスがイベントマスかチェック
    void SpaceCheck(int nowTurn)
    {
        switch (_playerPositions[_nowTurn])
        {
            case 6:
            case 11:
            case 16:
            case 24:
            case 26:
                StartCoroutine(MovePlayer(3));
                break;
        }
    }
}
