using UnityEngine;

public class Exercise_07_Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // other.transform.Rotate(Vector3.up * 90f);
        other.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }
}
