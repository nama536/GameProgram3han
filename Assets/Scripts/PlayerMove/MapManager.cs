using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerMoveSpaceCountText;

    [SerializeField] Transform[] _spaces;

    public IEnumerator MovePlayer(int playerPosition, int moveSpaceCount, PlayerPlaceManager.PlayerNumber playerNumber)
    {
        _playerMoveSpaceCountText.enabled = true;

        Debug.Log(moveSpaceCount);
        _playerMoveSpaceCountText.text = moveSpaceCount.ToString();

        yield return new WaitForSeconds(1f);
        //残りの動くマス数が0じゃ無い限り
        while (moveSpaceCount != 0)
        {
            if(moveSpaceCount > 0)
            {
                moveSpaceCount--;
                _playerMoveSpaceCountText.text = moveSpaceCount.ToString();
            }
            else if(moveSpaceCount < 0)
            {
                moveSpaceCount++;
                _playerMoveSpaceCountText.text = moveSpaceCount.ToString();
            }

            yield return new WaitForSeconds(1f);
        }

        _playerMoveSpaceCountText.enabled = false;
    }

    //現在のマスがイベントマスかチェック
    void SpaceCheck()
    {
        
    }
}
