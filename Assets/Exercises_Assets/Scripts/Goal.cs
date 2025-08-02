using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public ExerciseComplete exerciseComplete;
    public bool OnPlay = false;

    private void Start()
    {
        if(OnPlay)
            exerciseComplete.Complete();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball") && !other.CompareTag("Edward")) return;

        exerciseComplete.Complete();
    }


}
