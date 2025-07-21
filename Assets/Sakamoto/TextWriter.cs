using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TutorialText _uitext;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private List<SceneData> _sceneList=new ();
    [SerializeField] private string _nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Cotest");
    }

    // �N���b�N�҂��̃R���[�`��
    IEnumerator Skip()
    {
        while (_uitext.Playing) yield return 0;
        while (!_uitext.IsClicked()) yield return 0;
    }

    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        for (int i = 0; i < _sceneList.Count; i++)
        {
            var scene = _sceneList[i];

            if (scene.Background != null)
                _backgroundImage.sprite = scene.Background;

            _uitext.DrawText(scene.Text);
            yield return StartCoroutine(Skip());
        }

        // �Ō�̃Z���t���I�������V�[���J��
        if (!string.IsNullOrEmpty(_nextSceneName))
            SceneManager.LoadScene(_nextSceneName);
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(_nextSceneName);
    }
}

[System.Serializable]
public class SceneData
{
    public string Text;
    public Sprite Background;
}