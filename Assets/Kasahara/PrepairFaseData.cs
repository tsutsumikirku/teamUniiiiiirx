using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "data/kasu")]
public class PrepairFaseData : ScriptableObject
{
    public DayData[] DayDatas;
}
// 1. �V���A���C�Y�Ώۂ� struct
[System.Serializable]
public struct DayData
{
    public string csvPath; // CSV�t�@�C���̃p�X
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DayData))]
public class DayDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Element X ���� X �𔲂��o��
        int idx = 0;
        if (label.text.StartsWith("Element "))
        {
            var numStr = label.text.Substring("Element ".Length);
            int.TryParse(numStr, out idx);
        }
        // ���{�ꃉ�x������
        var customLabel = new GUIContent($"{idx + 1}���ڂ̃f�[�^");

        // �l�X�g���ꂽ�t�B�[���h���ꊇ�\��
        EditorGUI.PropertyField(position, property, customLabel, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // �f�t�H���g�̍����������Ōv�Z �itrue = �q�v�f���܂߂āj
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
#endif
