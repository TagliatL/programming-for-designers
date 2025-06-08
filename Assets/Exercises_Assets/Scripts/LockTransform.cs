using UnityEditor;
using UnityEngine;

public class LockTransform : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Selection.activeGameObject == gameObject)
        {
            //GizmoUtility.SetGizmoEnabled()
        }
    }
}
