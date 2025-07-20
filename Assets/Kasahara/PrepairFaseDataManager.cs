using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public struct SerifData
{
    public int Tension; // 精神力
    public (int Money, string Serif)[] Serifs; // 金額、セリフのタプル
    public string SpecialSerif; // 特殊セリフ
}
public class PrepairFaseDataManager : MonoBehaviour
{
    [SerializeField]
    PrepairFaseData faseData;
    [Header("Debug")]
    [SerializeField] int day = 0; // デバッグ用の日付
    [SerializeField] int tension = 0; // デバッグ用の精神力
    [SerializeField] int money = 0; // デバッグ用の金額

    SerifData[] serifLevels;
    private void Start()
    {
        LoadFaseData();
    }
    [ContextMenu("DebugLoadData")]
    void LoadFaseData()
    {
        LoadFaseData(day);
    }
    public void LoadFaseData(int day)
    {
        day--;
        if(faseData == null)
        {
            Debug.LogError("PrepairFaseDataが設定されていません。");
            return;
        }
        if (day < 0 || day >= faseData.DayDatas.Length)
        {
            Debug.LogError($"無効な日付: {day}。範囲は1から{faseData.DayDatas.Length}までです。");
            return;
        }
        string faseDataCsvPath = Path.Combine(Application.streamingAssetsPath, faseData.DayDatas[day].csvPath);
        if (File.Exists(faseDataCsvPath))
        {
            string[] lines = File.ReadAllLines(faseDataCsvPath);
            serifLevels = new SerifData[lines.Length - 1]; // ヘッダー行を除く
            for (int j = 1; j < lines.Length; j++)
            {
                string line = lines[j];
                string[] values = line.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                // values[0] : 精神力
                // values[2n - 1] : 金額
                // values[2n] : セリフ
                // values[max] : 特殊セリフ
                serifLevels[j - 1] = new SerifData();
                serifLevels[j - 1].Tension = int.Parse(values[0]);
                serifLevels[j - 1].Serifs = new (int Money, string Serif)[(values.Length - 2) / 2]; // 金額とセリフのペアの数
                for (int k = 1; k < values.Length - 1; k += 2)
                {
                    int money = int.Parse(values[k]);
                    string serif = values[k + 1];
                    serifLevels[j - 1].Serifs[k / 2] = (money, serif);
                }
                serifLevels[j - 1].SpecialSerif = values[values.Length - 1];
            }
            foreach (var data in serifLevels)
            {
                Debug.Log($"精神力境界値 : {data.Tension}, お金の境界値とセリフ : {data.Serifs},一定金額以上のセリフ : {data.SpecialSerif}");
            }

        }
        else
        {
            Debug.LogError($"CSVファイルが見つかりません: {faseDataCsvPath}");
        }
    }
    [ContextMenu("セリフ取得デバッグ")]
    private void GetSerif()
    {
        Debug.Log(GetSerif(tension, money));
    }
    public string GetSerif(int tension, int money)
    {
        for (int i = 0; i < serifLevels.Length; i++)
        {
            if (tension >= serifLevels[i].Tension)
            {
                // 精神力の境界値を超えていた場合、セリフを返す
                for (int j = 0; j < serifLevels[i].Serifs.Length; j++)
                {
                    if (money <= serifLevels[i].Serifs[j].Money)
                    {
                        return serifLevels[i].Serifs[j].Serif;
                    }
                }
                // 一定金額以上のセリフがある場合はそれを返す
                return serifLevels[i].SpecialSerif;
            }
        }
        Debug.LogWarning("該当するセリフが見つかりませんでした。精神力やお金の値を確認してください。");
        return string.Empty; // ここが呼ばれることはない
    }
}
