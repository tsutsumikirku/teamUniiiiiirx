using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CharacterTalkDataManager : MonoBehaviour
{
    [SerializeField] private TalkData[] talkDataArray;
    [Header("�f�o�b�O�p")]
    [SerializeField] private string talkTheme = "Common";
    [SerializeField] private int mentalValue = 0;
    [SerializeField] private int money = 0;
    Dictionary<string, TalkData> talkDataDictionary = new Dictionary<string, TalkData>();
    private void Start()
    {
        // TalkData�����[�h
        LoadTalkData();
    }
    void LoadTalkData()
    {
        for (int i = 0; i < talkDataArray.Length; i++)
        {
            talkDataDictionary.Add(talkDataArray[i].TalkTheme, talkDataArray[i]);
        }
    }
    [ContextMenu("GetTalkContent")]
    void GetTalkContent()
    {
        string content = GetTalkContent(talkTheme, mentalValue, money);
        if (content != null)
        {
            Debug.Log($"Talk Content: {content}");
        }
        else
        {
            Debug.Log("No talk content found for the given parameters.");
        }
    }
    public string GetTalkContent(string talkTheme, int mentalValue, int money)
    {
        if(talkDataDictionary.TryGetValue(talkTheme,out TalkData talkData))
        {
            // �����^���l�ɑΉ�����m�[�h���擾
            var mentalNode = talkData.mentalNodes.FirstOrDefault(node => node.minMental <= mentalValue);
            if (mentalNode != null)
            {
                // �������ɑΉ�����g�[�N���e���擾
                var moneyNode = mentalNode.moneyNodes.FirstOrDefault(node => node.maxMoney > money);
                if (moneyNode != null)
                {
                    return moneyNode.talkContent;
                }
            }
        }
        return null; // �Y������g�[�N���e��������Ȃ��ꍇ��null��Ԃ�
    }
}
