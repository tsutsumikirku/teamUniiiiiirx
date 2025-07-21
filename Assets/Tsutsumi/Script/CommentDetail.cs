using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommentDetail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _commentText;
    public void SetString(string comment, CommentAndResponseData data)
    {
        _commentText.text = comment;
        if (data.Money == 0 && data.MentalDamage > 0)
        {
            _commentText.color = Color.red; // Set text color to red for anti comments
        }
    }
}
