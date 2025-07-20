using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "data/kasu")]
public class PrepairFaseData : ScriptableObject
{
    public DayData[] DayDatas;
}
// 1. シリアライズ対象の struct
[System.Serializable]
public struct DayData
{
    public string csvPath; // CSVファイルのパス
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DayData))]
public class DayDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Element X から X を抜き出し
        int idx = 0;
        if (label.text.StartsWith("Element "))
        {
            var numStr = label.text.Substring("Element ".Length);
            int.TryParse(numStr, out idx);
        }
        // 日本語ラベル生成
        var customLabel = new GUIContent($"{idx + 1}日目のデータ");

        // ネストされたフィールドを一括表示
        EditorGUI.PropertyField(position, property, customLabel, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // デフォルトの高さを自動で計算 （true = 子要素も含めて）
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
#endif
