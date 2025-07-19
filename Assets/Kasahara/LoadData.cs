using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    CSVReader reader;
    
    // Start is called before the first frame update
    void Start()
    {
        reader = new CSVReader();
    }

    // Update is called once per frame
    void Update()
    {
        reader.GetRandomViewer();
        Debug.Log(reader.GetRandomCommentAndResponse("Common").Key);
    }
}
