using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteAlways]
public class Goal_RunInEditor : MonoBehaviour
{
    public ExerciseComplete exerciseComplete; 


    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, .5f);
        foreach (var hitCollider in hitColliders)
        {
            if (!hitCollider.CompareTag("Ball")) return;

            exerciseComplete.Complete();
        }
    }
}
