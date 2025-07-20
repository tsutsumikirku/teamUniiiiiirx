using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChatMove : MonoBehaviour
{
    [SerializeField] ChatMove _chatMove;
    [SerializeField] float _xMaxPosition = 10f;
    ChatMove chatMove;

    [ContextMenu("Test")]
    public void Test()
    {
        chatMove = Instantiate(_chatMove, FindAnyObjectByType<Canvas>().transform);
        
    }
}
