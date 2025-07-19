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
    public int SubscribTensions { get; private set; }
    public List<SuperChat> SuperChatList { get; private set; } = new List<SuperChat>();

    private float _maxTime = 5, _minTime = 1;
    private float _currentTension = 0;

    public Viewer(string name, (int SubscribTention, (int money, int tention)[] SuperChat) data)
    {
        Name = name;
        SubscribTensions = data.SubscribTention;
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
            float randTime = UnityEngine.Random.Range(_minTime, _maxTime);
            await UniTask.Delay((int)(randTime * 1000), cancellationToken: token);
            //Debug.Log("コメント");
        }
    }
    public void SetMaxTime(float max)
    {
        _maxTime = max;
    }
    public void SetMinTime(float min)
    {
        _minTime = min;
    }
    public void TensionChange(float tension)
    {
        Debug.Log(_currentTension);
        _currentTension += tension;
        if (_currentTension >= SubscribTensions && !IsSubscription)
        {
            IsSubscription = true;
            SubscriptionManagement.Subscribers.Add(Name, (SubscribTensions, SuperChatList.ToArray()));
            Debug.Log("Add");
        }
        foreach (var item in SuperChatList.OrderByDescending(a => a.Tension))
        {
            if (item.Tension <= _currentTension)
            {
                //抽選コメントかスパチャか
                int rand = Random.Range(0, 2);
                Debug.Log(rand == 0 ? "ノーマルコメント" : "スパ茶");

                break;
            }
        }
        if (_currentTension <= 0)
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
    public int Tension { get; private set; } = 10;

    public SuperChat(int money, int tension)
    {
        Money = money;
        Tension = tension;
    }
}
