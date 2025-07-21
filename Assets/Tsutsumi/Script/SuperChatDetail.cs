using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuperChatDetail : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI _text;
    public void SetText(string text, CommentAndResponseData data)
    {
        _text.text = text;
        if (data.MentalDamage > 0)
        {
            _text.color = Color.red; // Set text color to red for anti comments
        }
    }   
}
