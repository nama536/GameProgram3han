using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //プレイヤーのオブジェクト
    [SerializeField] GameObject _player;
    //プレイヤーの見た目
    [SerializeField] Sprite[] _playerSprites;
    //生成されたプレイヤー　0がプレイヤー1　1がプレイヤー2
    [SerializeField] GameObject[] _players;
    
    //マップ上のプレイヤーの位置
    int[] _playerPositions = new int[2];
    //プレイヤーが動いてる時に上に出るあと何マス進むかのテキスト
    [SerializeField] TextMeshProUGUI _playerMoveSpaceCountText;
    //プレイヤーの動きの処理用
    float _offset = 0.2f;
    float _movePresent = 0f;
    bool _doDefaultPosition = true;
    //マスの位置0~30
    [SerializeField] Transform[] _spaces;

    //ターンが今どちらか 0がプレイヤー1　1がプレイヤー2
    int _nowTurn;
    //処理中かどうか
    public bool Processing = false;

    [SerializeField] TurnManager _turnManager;
    [SerializeField] ResultManager _resultManager;

    void Start()
    {
        //プレイヤーを2人生成
        _players[0] = Instantiate(_player,_spaces[0].position,Quaternion.identity);
        _players[1] = Instantiate(_player,_spaces[0].position,Quaternion.identity);

        //プレイヤーの見た目変更
        SpriteRenderer spriteRendererP1 = _players[0].GetComponent<SpriteRenderer>();
        spriteRendererP1.sprite = _playerSprites[0];
        SpriteRenderer spriteRendererP2 = _players[1].GetComponent<SpriteRenderer>();
        spriteRendererP2.sprite = _playerSprites[1];

        SameSpace();
    }

    /*void Update()
    {
        //テスト用
       if (!Processing)
        {
            StartCoroutine(MovePlayer(5));
        }
    }*/

    //ダイスが振られたら(ダイスの目)
    public IEnumerator MovePlayer(int moveSpaceCount)
    {
        //処理中
        Processing = true;

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
        switch (_nowTurn)
        {
            case 0:
                _playerMoveSpaceCountText.color = Color.black;
                break;
            case 1:
                _playerMoveSpaceCountText.color = Color.blue;
                break;
        }

        yield return new WaitForSeconds(1f);

        //ダイスがプラスの場合
        if(moveSpaceCount > 0)
        {
            //残りの動くマス数が0じゃ無い限り
            while (moveSpaceCount != 0 && _playerPositions[_nowTurn] != 30)
            {   
                //残りの進むマス数を減らしてプレイヤーの位置をプラス
                moveSpaceCount--;
                _playerPositions[_nowTurn]++;
                _playerMoveSpaceCountText.text = moveSpaceCount.ToString();

                Transform nowPlayerTransform = _players[_nowTurn].transform;//今のプレイヤーの位置保存
                bool nowMoobing = true;//今動いてるか

                //0.8秒でプレイヤーを次のマスへ移動させる
                while(_movePresent < 0.8f)
                {
                    _movePresent += Time.deltaTime;
                    _players[_nowTurn].transform.position = Vector3.Slerp(nowPlayerTransform.position,_spaces[_playerPositions[_nowTurn]].position,_movePresent);
                    yield return null;//whileは1フレームの中で処理を行うためこれで1フレーム進めさせる
                }
                //パーセントをリセットしてプレイヤーが同じマスにいる時の処理
                if (nowMoobing)
                {
                    _movePresent = 0f;
                    SameSpace();
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
                while(_movePresent < 0.8f )
                {
                    _movePresent += Time.deltaTime;
                    _players[_nowTurn].transform.position = Vector3.Slerp(nowPlayerTransform.position,_spaces[_playerPositions[_nowTurn]].position,_movePresent);
                    yield return null;//whileは1フレームの中で処理を行うためこれで1フレーム進めさせる
                }
                //パーセントをリセットしてプレイヤーが同じマスにいる時の処理
                if (nowMoobing)
                {
                    _movePresent = 0f;
                    SameSpace();
                    nowMoobing = false;
                }

                yield return new WaitForSeconds(0.2f);
            }
        }

        //残りの進むマス数の表示を消す
        _playerMoveSpaceCountText.enabled = false;

        SpaceCheck();
    }

    //プレイヤーが同じ場所にいた時
    void SameSpace()
    {
        //プレイヤー1と2が同じ場所にいたら
        if(_playerPositions[0] == _playerPositions[1])
        {
            //位置をずらす
            _players[0].transform.position -= new Vector3(_offset,0f,0f);
            _players[1].transform.position += new Vector3(_offset,0f,0f);

            _doDefaultPosition = false;
        }
        //違う場所に行ったら
        else if(!_doDefaultPosition)
        {
            //今動いていない方の位置を戻す
            switch (_nowTurn)
            {
                case 0:
                    _players[1].transform.position -= new Vector3(_offset,0f,0f);
                    break;
                case 1:
                    _players[0].transform.position += new Vector3(_offset,0f,0f);
                    break;                                      
            }
            
            _doDefaultPosition = true;
        }
    }

    //現在のマスがイベントマスかチェック
    void SpaceCheck()
    {
        switch (_playerPositions[_nowTurn])
        {
            //3進むイベントマスに止まったら
            case 6:
            case 11:
            case 16:
            case 24:
            case 26:
                StartCoroutine(MovePlayer(3));
                Debug.Log("3進むマス");
                break;
            //次ターン確定で2の目になるイベントマスに止まったら
            case 12:
            case 20:
                _turnManager.TurnChange();
                Debug.Log("次ターン確定で2の目になるマス");
                break;
            //次ターンから1の目を出すまでターンが回ってこないイベントマスに止まったら
            case 15:
                _turnManager.TurnChange();
                Debug.Log("次ターンから1の目を出すまでターンが回ってこないマス");
                break;
            //ゴールマスに止まったら
            case 30:
                Debug.Log("プレイヤー" + (_nowTurn + 1) + "の勝利");
                StartCoroutine(_resultManager.DoResult(_nowTurn));
                break;
            //その他通常マスに止まったら
            default:
                _turnManager.TurnChange();
                break;
        }
    }
}
