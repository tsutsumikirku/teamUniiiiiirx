using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerManager : MonoBehaviour
{
    //TODO:HERE媚値の減少する倍率
    [SerializeField] float _value = 0.8f;
    List<IViewer> _viewerList = new();
    public void CommentReply(float tention, IViewer instance)
    {
        foreach (IViewer viewer in _viewerList)
        {
            if (viewer == instance)
            {
                Debug.Log("テンション増加");
            }
            else
            {
                Debug.Log("テンション減少");
            }
        }
    }

    public void AddViewer()
    {
    
        //コンストラクターをAdd
    }
}

public interface IViewer
{

}