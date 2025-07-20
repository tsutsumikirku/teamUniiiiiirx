using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public struct CommentAndResponseData
{
    public int Id;//使うかわからん
    public string Comment;//コメント内容
    public string Response;//レスポンス内容
    public int MentalDamage;//メンタルダメージ
    public int LikePoint;//いいねポイント
    public string CommentType;//話題
    public string MotionType;//モーションタイプ
    public int Money;//スパチャ額
}
public class CommentDataManager
{
    readonly Dictionary<string, HashSet<CommentAndResponseData>> commentAndResponseData = new Dictionary<string, HashSet<CommentAndResponseData>>();
    public CommentDataManager()
    {
        LoadCommentData();
    }
    private void LoadCommentData()
    {
        string commentCsvPath = Path.Combine(Application.streamingAssetsPath, "CommentAndResponse.csv");
        if (File.Exists(commentCsvPath))
        {
            string[] lines = File.ReadAllLines(commentCsvPath);
            foreach (string line in lines.Skip(1))
            {
                string[] values = line.Split(',');
                if (commentAndResponseData.TryGetValue(values[0], out var datas))
                {
                    datas.Add(new CommentAndResponseData
                    {
                        Id = int.Parse(values[1]),
                        Comment = values[2],
                        Response = values[3],
                        MentalDamage = int.Parse(values[4]),
                        LikePoint = int.Parse(values[5]),
                        CommentType = values[6],
                        MotionType = values[7],
                        Money = int.Parse(values[8])
                    });
                }
                else
                {
                    commentAndResponseData.Add(values[0], new HashSet<CommentAndResponseData>
                    {
                        new CommentAndResponseData
                        {
                            Id = int.Parse(values[1]),
                            Comment = values[2],
                            Response = values[3],
                            MentalDamage = int.Parse(values[4]),
                            LikePoint = int.Parse(values[5]),
                            CommentType = values[6],
                            MotionType = values[7],
                            Money = int.Parse(values[8])
                        }
                    });
                }
            }
        }
        else
        {
            Debug.LogError("CSVファイルが見つかりませんでした: " + commentCsvPath);
        }
    }
    public CommentAndResponseData GetCommentData()
    {
        CommentAndResponseData data = new CommentAndResponseData();
        GetCommentData("Common", ref data);
        return data;
    }
    public bool GetCommentData(string type, ref CommentAndResponseData data)
    {
        //ランダムにコメントデータを取得する例を示します。
        if (commentAndResponseData.TryGetValue(type, out var datas))
        {
            if (datas.Count > 0)
            {
                var randomData = datas.ElementAt(Random.Range(0, datas.Count));
                data = randomData;
                return true;
            }
            return false;
        }
        else
        {
            Debug.LogWarning($"指定されたコメントタイプ '{type}' のデータが見つかりません。");
            return false;
        }
    }
}
