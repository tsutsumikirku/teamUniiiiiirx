using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommentDetail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _commentText;
    public void SetString(string comment)
    {
        _commentText.text = comment;
    }
}
