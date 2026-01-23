using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextColorCube : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private TextMeshProUGUI _text;
    
    [Header("色設定")]
    public Color normalColor = Color.white;   // 通常時の色
    public Color selectedColor = Color.blue;   // 選択（フォーカス）時の色

    /*void Update()
    {
        Debug.Log(this.gameObject.name + " が選択されました。"); // これを追加
        if (_text != null) _text.color = selectedColor;
    }*/

    void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        if (_text != null) _text.color = normalColor;
    }

    // ボタンが選択された時（フォーカスが当たった時）
    public void OnSelect(BaseEventData eventData)
    {
        if (_text != null) _text.color = selectedColor;
    }

    // 選択が外れた時
    public void OnDeselect(BaseEventData eventData)
    {
        if (_text != null) _text.color = normalColor;
    }

    // メニューが閉じる時に色をリセットするための処理
    void OnDisable()
    {
        if (_text != null) _text.color = normalColor;
    }
}
