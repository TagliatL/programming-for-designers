using UnityEditor;
using UnityEngine;

public class Fail : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;
        EditorApplication.ExitPlaymode();
    }

}
