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

    //�i���[�V�����p�̃e�L�X�g�𐶐�����֐�
    public void DrawText(string text)
    {
        StartCoroutine("CoDrawText", text);
    }

    //�e�L�X�g���k���k���o�Ă���ׂ̃R���[�`��
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
