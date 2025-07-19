using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Viewer
{
    public string Name { get; private set; }
    public bool IsSubscription { get; private set; }
    public int SubscribTention { get; private set; }
    public List<SuperChat> SuperChatList { get; private set; } = new List<SuperChat>();

    private int _maxTime = 5, _minTime = 1;
    private int _currentTention = 0;

    public Viewer(string name, (int SubscribTention, (int money, int tention)[] SuperChat) data)
    {
        Name = name;
        SubscribTention = data.SubscribTention;
        foreach (var item in data.SuperChat)
        {
            SuperChatList.Add(new SuperChat(item.money, item.tention));
        }
        CancellationTokenSource cts = new CancellationTokenSource();
        LoopAsync(cts.Token).Forget();
    }
    private async UniTask LoopAsync(CancellationToken token)
    {
        while (true)
        {
            int randTime = UnityEngine.Random.Range(_minTime, _maxTime);
            await UniTask.Delay(randTime * 1000, cancellationToken: token);
            //Debug.Log("コメント");
        }
    }
    public void SetMaxTime(int max)
    {
        _maxTime = max;
    }
    public void SetMinTime(int min)
    {
        _minTime = min;
    }
    public void TentionChange(int tention)
    {
        Debug.Log(_currentTention);
        _currentTention += tention;
        if (_currentTention >= SubscribTention && !IsSubscription)
        {
            IsSubscription = true;
            SubscriptionManagement.Subscribers.Add(Name, (SubscribTention, SuperChatList.ToArray()));
            Debug.Log("Add");
        }
        foreach (var item in SuperChatList.OrderByDescending(a => a.Tention))
        {
            if (item.Tention <= _currentTention)
            {
                //抽選コメントかスパチャか
                int rand = Random.Range(0, 2);
                Debug.Log(rand == 0 ? "ノーマルコメント" : "スパ茶");

                break;
            }
        }
        if (_currentTention <= 0)
        {
            IsSubscription = false;
            SubscriptionManagement.Subscribers.Remove(Name);
            Debug.Log("Remove");
        }
    }
}
[System.Serializable]
public class SuperChat
{
    public int Money { get; private set; } = 1000;
    public int Tention { get; private set; } = 10;

    public SuperChat(int money, int tention)
    {
        Money = money;
        Tention = tention;
    }
}
