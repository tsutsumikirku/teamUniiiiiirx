using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TalkData", menuName = "TalkData")]
public class TalkData : ScriptableObject
{
    public string TalkTheme;
    public MentalNode[] mentalNodes;

    [System.Serializable]
    public class MentalNode
    {
        [Tooltip("このメンタル値以上")]
        public int minMental;
        public MoneyNode[] moneyNodes;
    }

    [System.Serializable]
    public class MoneyNode
    {
        [Tooltip("この所持金未満")]
        public int maxMoney;
        public string talkContent;
    }
}
