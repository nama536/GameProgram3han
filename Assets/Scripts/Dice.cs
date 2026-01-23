using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Dice : MonoBehaviour
{
    [Header("表示用のスプライトレンダラー")]
    public SpriteRenderer targetSpriteRenderer; 

    [Header("--- 普通のサイコロ設定 ---")]
    public Sprite[] normalShuffleSprites; // シャッフル中に表示する画像(1~6など)
    public Sprite[] normalResultSprites;  // 1~6の確定用画像

    [Header("--- ハイリスクサイコロ設定 ---")]
    public Sprite[] riskShuffleSprites;  // シャッフル中に表示する画像(-1~-4, 6など)
    // 確定時の個別画像
    public Sprite riskMinus1;
    public Sprite riskMinus2;
    public Sprite risk3;
    public Sprite riskMinus4;
    public Sprite risk6;

    [Header("共通演出設定")]
    public float shuffleDuration = 1.0f; 
    public float shuffleInterval = 0.1f; 

    private bool isRolling = false; 

    [SerializeField] TurnManager turnManager; // インスペクターでTurnManagerをセット

    // ==========================================
    // 普通のサイコロ (1〜6)
    // ==========================================
    public void OnDice(PlayerManager.Event thisEvent)
    {
        MapManager mapManager = FindObjectOfType<MapManager>();
        if (turnManager.hasRolled || isRolling || mapManager.Processing) return;

        turnManager.hasRolled = true; // 振ったことにする
        
        int resultIndex = UnityEngine.Random.Range(0, 6); // 0~5のインデックス
        Sprite finalSprite = normalResultSprites[resultIndex];
        
        StartCoroutine(NormalShuffleRoutine(finalSprite, resultIndex + 1, thisEvent));
    }

    IEnumerator NormalShuffleRoutine(Sprite finalSprite, int value, PlayerManager.Event thisEvent)
    {
        isRolling = true;
        float elapsed = 0f;

        while (elapsed < shuffleDuration)
        {
            // 普通のサイコロ用画像群からランダム表示
            targetSpriteRenderer.sprite = normalShuffleSprites[UnityEngine.Random.Range(0, normalShuffleSprites.Length)];
            elapsed += shuffleInterval;
            yield return new WaitForSeconds(shuffleInterval);
        }

        targetSpriteRenderer.sprite = finalSprite;
        MapManager mapManager = FindObjectOfType<MapManager>();

        //もし"1以外だと動けないイベント"中で1以外を出したら
        if(thisEvent == PlayerManager.Event.stopDice && value != 1)
        {
            Invoke("TurnChange",1f);//1秒後にターンチェンジ
        }
        else//それ以外なら普通に動かす
        {
            StartCoroutine(mapManager.MovePlayer(value)); 
        }

        Debug.Log($"普通ダイス確定: {value}");
        isRolling = false;
    }

    void TurnChange()
    {
        turnManager.TurnChange();
    }
    //-------------------------------------------------------------------------------

    // ==========================================
    // ハイリスクサイコロ (-1, -2, -4, 3, 6)
    // ==========================================
    public void OnSixDice(PlayerManager.Event thisEvent)
    {
        if (turnManager.hasRolled || isRolling) return;
        turnManager.hasRolled = true; // 振ったことにする

        // 確率計算
        int roll = UnityEngine.Random.Range(1, 7);
        int resultValue;
        Sprite finalSprite;

        if (roll == 1) { resultValue = -1; finalSprite = riskMinus1; }
        else if (roll == 2) { resultValue = 3;  finalSprite = risk3; }
        else if (roll == 3) { resultValue = -2; finalSprite = riskMinus2; }
        else if (roll == 4) { resultValue = 6;  finalSprite = risk6; }
        else if (roll == 5) { resultValue = -4; finalSprite = riskMinus4; }
        else { resultValue = 6; finalSprite = risk6; }

        StartCoroutine(RiskShuffleRoutine(finalSprite, resultValue));
    }

    IEnumerator RiskShuffleRoutine(Sprite finalSprite, int value)
    {
        isRolling = true;
        float elapsed = 0f;

        while (elapsed < shuffleDuration)
        {
            // ハイリスク専用画像群（マイナス値など）からランダム表示
            targetSpriteRenderer.sprite = riskShuffleSprites[UnityEngine.Random.Range(0, riskShuffleSprites.Length)];
            elapsed += shuffleInterval;
            yield return new WaitForSeconds(shuffleInterval);
        }

        targetSpriteRenderer.sprite = finalSprite;
        MapManager mapManager = FindObjectOfType<MapManager>();
        StartCoroutine(mapManager.MovePlayer(value));
        Debug.Log($"ハイリスク確定: {value}");
        isRolling = false;
    }
}
