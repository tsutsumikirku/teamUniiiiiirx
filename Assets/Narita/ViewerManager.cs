using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerManager : MonoBehaviour
{
    List<IViewer> _viewerList = new();
}

public interface IViewer
{

}