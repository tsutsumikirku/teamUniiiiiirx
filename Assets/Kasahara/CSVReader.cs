using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CSVReader
{
    readonly Dictionary<string, (int SubscribeTension, (int Money, int Tension)[] SuperChat)> viewerData = new Dictionary<string, (int, (int money, int tension)[])>();
    readonly HashSet<string> standbyViewerName = new HashSet<string>();
    readonly Dictionary<string, Dictionary<string, (string Response, int FlatteryPoints)[]>> CommentAndResponseData = new Dictionary<string, Dictionary<string, (string Response, int FlatteryPoints)[]>>();
    public CSVReader()
    {
        LoadViewerData();
        LoadCommentAndResponseData();
    }
    void LoadViewerData()
    {
        // StreamingAssets�t�H���_��CSV�t�@�C���p�X���擾
        string viewerCsvPath = Path.Combine(Application.streamingAssetsPath, "Viewer.csv");

        // �t�@�C����ǂݍ���
        if (File.Exists(viewerCsvPath))
        {
            string[] lines = File.ReadAllLines(viewerCsvPath);

            foreach (string line in lines.Skip(1))
            {
                string[] values = line.Split(',');
                standbyViewerName.Add(values[0]); // �����Җ����n�b�V���Z�b�g�ɒǉ�
                viewerData.Add(values[0], (int.Parse(values[1]), new (int Money, int Tension)[(values.Length - 2) / 2]));
                for (int i = 2; i < values.Length; i += 2)
                {
                    int index = (i - 2) / 2;
                    viewerData[values[0]].Item2[index] = (int.Parse(values[i]), int.Parse(values[i + 1]));
                }
            }
            foreach (var user in viewerData)
            {
                Debug.Log($"User: {user.Key}, SubscribeTension: {user.Value.SubscribeTension}, Money and Tension Pairs: {string.Join(", ", user.Value.Item2)}");
            }
        }
        else
        {
            Debug.LogError("CSV�t�@�C����������܂���ł���: " + viewerCsvPath);
        }
    }
    void LoadCommentAndResponseData()
    {
        string commentCsvPath = Path.Combine(Application.streamingAssetsPath, "CommentAndResponse.csv");
        if (File.Exists(commentCsvPath))
        {
            string[] lines = File.ReadAllLines(commentCsvPath);
            foreach (string line in lines.Skip(1))
            {
                string[] values = line.Split(',',System.StringSplitOptions.RemoveEmptyEntries);
                CommentAndResponseData.Add(values[0], new Dictionary<string, (string response, int flatteryPoints)[]>());
                //�R�����g�̃^�C�v���L�[�Ƃ��āA�R�����g���L�[�Ƃ��鎫���Ƀ��X�|���X�ƃt���b�^���[�|�C���g�̃y�A�̔z���ǉ�
                CommentAndResponseData[values[0]].Add(values[1], new (string Response, int FlatteryPoints)[(values.Length - 2) / 2]);
                //CommentAndResponse.Add(values[0], (values[1], new (string response, int flatteryPoints)[(values.Length - 2) / 2]));
                for (int i = 2; i < values.Length; i += 2)
                {
                    int index = (i - 2) / 2;
                    //Debug.Log($"{values[0]} {values[1]} {index} {values[i]} {values[i + 1]}");
                    CommentAndResponseData[values[0]][values[1]][index] = (values[i], int.Parse(values[i + 1]));
                }
            }
            foreach (var comment in CommentAndResponseData)
            {
                foreach (var line in comment.Value)
                {
                    Debug.Log($"Comment Type: {comment.Key}, Comment: {line.Key}, Responses: {string.Join(", ", line.Value.Select(r => $"({r.Response}, {r.FlatteryPoints})"))}");
                }
            }
        }
        else
        {
            Debug.LogError("CSV�t�@�C����������܂���ł���: " + commentCsvPath);
        }
    }
    /// <summary>
    /// �����_���Ɏ����҂�I�����A���̎����҂̖��O�A�`�����l���o�^�ɕK�v�ȃe���V�����A
    /// �X�p�`���z�Ƃ���𓊂��邽�߂̃e���V�����̔z���Ԃ��B
    /// </summary>
    /// <returns></returns>
    public KeyValuePair<string, (int SubscribeTension, (int Money, int Tension)[] SuperChat)>? GetRandomViewer()
    {
        // �����_���Ɏ����҂�I��
        if (viewerData.Count > 0)
        {
            if (standbyViewerName.Count == 0)
            {
                Debug.LogWarning("�X�^���o�C���̎����҂����܂���B");
                return null;
            }
            string viewerName = standbyViewerName.ElementAt(Random.Range(0, standbyViewerName.Count));
            standbyViewerName.Remove(viewerName);
            var randomViewer = viewerData[viewerName];
            KeyValuePair<string, (int SubscribeTension, (int Money, int Tension)[] SuperChat)> randomViewerPair = new(viewerName, randomViewer);
            Debug.Log($"�����_���ɑI�΂ꂽ������: {randomViewerPair.Key}, SubscribeTension: {randomViewerPair.Value.SubscribeTension}, SuperChat: {string.Join(", ", randomViewerPair.Value.Item2.Select(sc => $"({sc.Money}, {sc.Tension})"))}");
            return randomViewerPair;
        }
        else
        {
            Debug.LogWarning("�����҃f�[�^����ł��B");
            return null;
        }
    }
    /// <summary>
    /// �����҂��ޏo�����ۂɌĂԁB�w�肳�ꂽ�����Җ����X�^���o�C���̎����҃��X�g�ɖ߂��B
    /// </summary>
    /// <param name="name"></param>
    public void ExitViewer(string name)
    {
        standbyViewerName.Add(name);
    }
    public KeyValuePair<string, (string Response,int flattypoints)[]> GetRandomCommentAndResponse(string type)
    {
        if (!CommentAndResponseData.TryGetValue(type,out var dic))
        {
            Debug.LogWarning($"�w�肳�ꂽ�R�����g�^�C�v�����݂��܂���: {type}");
            return default;
        }
        return dic.ElementAt(Random.Range(0, CommentAndResponseData[type].Count));
    }
}