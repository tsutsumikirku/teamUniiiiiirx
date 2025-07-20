using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ViewerManager : MonoBehaviour
{
    //TODO:HERE媚値の減少する倍率
    [SerializeField] private float _value = 0.8f;
    private List<Viewer> _viewerList = new();
    public static CSVReader CSVReader;
    [SerializeField] private CommentGenerator _generator;
    [SerializeField] private float _viewerSpawnMaxTime = 5;
    [SerializeField] private float _viewerSpawnMinTime = 1;
    [SerializeField] private int _maxViewerCount = 10;

    private void Start()
    {
        CSVReader = new CSVReader();

        CancellationTokenSource source = new CancellationTokenSource();

        AsyncUpdate(source.Token).Forget();
    }

    private async UniTask AsyncUpdate(CancellationToken token)
    {
        while (true)
        {
            float randTime = Random.Range(_viewerSpawnMinTime, _viewerSpawnMaxTime);
            await UniTask.Delay((int)(randTime * 1000), cancellationToken: token);
            AddViewer();
        }
    }

    public void CommentReply(int tension, Viewer instance)
    {
        instance.TensionChange(tension);

        foreach (Viewer viewer in _viewerList)
        {
            if (viewer != instance)
            {
                //  viewer.TentionChange(-tension * _value);
                Debug.Log("テンション減少");
            }
        }
    }

    public void AddViewer()
    {
        if (_viewerList.Count >= _maxViewerCount) { return; }

        var pair = CSVReader.GetRandomViewer();

        if (!pair.HasValue) { return; }

        _viewerList.Add(new Viewer(pair.Value.Key, pair.Value.Value, _generator));
        Debug.Log("最新の視聴者" + _viewerList[^1].Name);
    }
}