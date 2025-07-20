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
        [Tooltip("‚±‚Ìƒƒ“ƒ^ƒ‹’lˆÈã")]
        public int minMental;
        public MoneyNode[] moneyNodes;
    }

    [System.Serializable]
    public class MoneyNode
    {
        [Tooltip("‚±‚ÌŠ‹à–¢–")]
        public int maxMoney;
        public string talkContent;
    }
}
