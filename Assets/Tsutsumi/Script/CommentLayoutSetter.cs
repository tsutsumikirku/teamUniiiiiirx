using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public static class CommentLayoutSetter
{
    
    public static async UniTask UniTaskSetLayoutAsync(GameObject commentObject, Vector3 position, Quaternion rotation)
    {
        // Simulate some asynchronous operation
        await UniTask.Delay(100); // Simulating a delay for the sake of example

        // Set the position and rotation of the comment object
        commentObject.transform.position = position;
        commentObject.transform.rotation = rotation;
    }
}
