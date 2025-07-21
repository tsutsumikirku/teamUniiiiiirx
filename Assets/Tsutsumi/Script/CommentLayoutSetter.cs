using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CommentLayoutSetter : MonoBehaviour
{
    public static CommentLayoutSetter Instance { get; private set; }
    [SerializeField]float[] _yPositions;
    Queue<float> _yQueue = new Queue<float>();
    void Awake()
    {
        Instance = this;
        _yQueue = new Queue<float>(_yPositions);
    }
    public float Gety()
    {
        var y = _yQueue.Dequeue();
        _yQueue.Enqueue(y);
        return y;
    }
}
