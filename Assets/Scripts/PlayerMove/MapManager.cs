using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _diceNumberText;

    [SerializeField] Transform[] _spaces;

    public IEnumerator MovePlayer(int playerPosition, int diceNumber, PlayerPlaceManager.PlayerNumber playerNumber)
    {
        _diceNumberText.enabled = true;

        Debug.Log(diceNumber);
        _diceNumberText.text = diceNumber.ToString();

        yield return new WaitForSeconds(1f);
        //残りの動くマス数が0じゃ無い限り
        while (diceNumber != 0)
        {
            if(diceNumber > 0)
            {
                diceNumber--;
                _diceNumberText.text = diceNumber.ToString();
            }
            else if(diceNumber < 0)
            {
                diceNumber++;
                _diceNumberText.text = diceNumber.ToString();
            }
        }

         yield return new WaitForSeconds(1f);
    }

    //現在のマスがイベントマスかチェック
    void SpaceCheck()
    {
        
    }
}
