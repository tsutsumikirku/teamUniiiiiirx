using UnityEngine;
using UnityEngine.UI;

public class Text_gene : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] Text _textPrefab;
    float _height;
    float _width;


    private void Start()
    {
        RectTransform _rectTransform = _canvas.GetComponent<RectTransform>();

        _height = _rectTransform.rect.height;
        _width = _rectTransform.rect.width;

        float top = _height / 2;
        float bottom = -_height / 2;

        float randomY = Random.Range(top, bottom);
        Text newtext = Instantiate(_textPrefab, _canvas.transform);
        RectTransform _textPosi = newtext.GetComponent<RectTransform>();
        _textPosi.anchoredPosition = new Vector2(_width/2 + 100, randomY);

    }
     public float GiveCanvsWidth()
    {
       return _width; 
    }
}
