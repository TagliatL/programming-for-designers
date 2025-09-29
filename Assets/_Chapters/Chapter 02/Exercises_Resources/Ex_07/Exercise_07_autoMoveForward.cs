using UnityEngine;

public class Exercises_07_autoMoveForward : MonoBehaviour
{
    public Vector3 axis;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        Debug.Log(axis);
    }
}
