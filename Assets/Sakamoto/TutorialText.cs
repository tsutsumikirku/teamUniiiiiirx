using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private Text _talkText;
    [SerializeField] private float _textSpeed = 0.1f;
    public bool Playing { get; private set; } = false;
    
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    //ナレーション用のテキストを生成する関数
    public void DrawText(string text)
    {
        StartCoroutine("CoDrawText", text);
    }

    //テキストがヌルヌル出てくる為のコルーチン
    IEnumerator CoDrawText(string text)
    {
        Playing = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / _textSpeed);
            if (len > text.Length) break;
            _talkText.text=text.Substring(0, len);
        }
        _talkText.text = text;
        yield return 0;
        Playing = false;
    }
}
