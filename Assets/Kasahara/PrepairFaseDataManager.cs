using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public struct SerifData
{
    public int Tension; // ���_��
    public (int Money, string Serif)[] Serifs; // ���z�A�Z���t�̃^�v��
    public string SpecialSerif; // ����Z���t
}
public class PrepairFaseDataManager : MonoBehaviour
{
    [SerializeField]private string faseDataCsvPath = "FaseData.csv"; // �f�t�H���g��CSV�t�@�C����
    public string PrepairFaseDataPath
    {
        get => faseDataCsvPath;
        set
        {
            faseDataCsvPath = value;
            LoadFaseData();
        }
    }
    [Header("Debug")]
    [SerializeField] int tension = 0; // �f�o�b�O�p�̐��_��
    [SerializeField] int money = 0; // �f�o�b�O�p�̋��z

    SerifData[] serifLevels;
    private void Start()
    {
    }
    public void LoadFaseData()
    {
        string faseDataCsvPath = Path.Combine(Application.streamingAssetsPath, PrepairFaseDataPath);
        if (File.Exists(faseDataCsvPath))
        {
            string[] lines = File.ReadAllLines(faseDataCsvPath);
            serifLevels = new SerifData[lines.Length - 1]; // �w�b�_�[�s������
            for (int j = 1; j < lines.Length; j++)
            {
                string line = lines[j];
                string[] values = line.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                // values[0] : ���_��
                // values[2n - 1] : ���z
                // values[2n] : �Z���t
                // values[max] : ����Z���t
                serifLevels[j - 1] = new SerifData();
                serifLevels[j - 1].Tension = int.Parse(values[0]);
                serifLevels[j - 1].Serifs = new (int Money, string Serif)[(values.Length - 2) / 2]; // ���z�ƃZ���t�̃y�A�̐�
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
                Debug.Log($"���_�͋��E�l : {data.Tension}, �����̋��E�l�ƃZ���t : {data.Serifs},�����z�ȏ�̃Z���t : {data.SpecialSerif}");
            }

        }
        else
        {
            Debug.LogError($"CSV�t�@�C����������܂���: {faseDataCsvPath}");
        }
    }
    [ContextMenu("�Z���t�擾�f�o�b�O")]
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
                // ���_�͂̋��E�l�𒴂��Ă����ꍇ�A�Z���t��Ԃ�
                for (int j = 0; j < serifLevels[i].Serifs.Length; j++)
                {
                    if (money <= serifLevels[i].Serifs[j].Money)
                    {
                        return serifLevels[i].Serifs[j].Serif;
                    }
                }
                // �����z�ȏ�̃Z���t������ꍇ�͂����Ԃ�
                return serifLevels[i].SpecialSerif;
            }
        }
        Debug.LogWarning("�Y������Z���t��������܂���ł����B���_�͂₨���̒l���m�F���Ă��������B");
        return string.Empty; // �������Ă΂�邱�Ƃ͂Ȃ�
    }
}
