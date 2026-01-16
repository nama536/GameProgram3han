using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] GameObject _resultPanel;
    [SerializeField] TextMeshProUGUI _resultWinText;
    [SerializeField] Image _resultWinImage;
    [SerializeField] Sprite[] _resultWinSprites;

    public IEnumerator DoResult(int whoWin)
    {
        yield return new WaitForSeconds(2f);

        switch (whoWin)
        {
            case 0://プレイヤー1が勝ったら
                _resultWinText.text = "プレイヤー１の勝利";
                _resultWinText.color = Color.black;
                _resultWinImage.sprite = _resultWinSprites[0];
                break;
            case 1://プレイヤー2が勝ったら
                _resultWinText.text = "プレイヤー２の勝利";
                _resultWinText.color = Color.blue;
                _resultWinImage.sprite = _resultWinSprites[1];
                break;
        }
        _resultPanel.SetActive(true);

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Title");
    }
}
