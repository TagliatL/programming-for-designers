using UnityEngine;

public class CreateCubeRow : MonoBehaviour
{
    [SerializeField] private int numberOfCubes = 5;

    [SerializeField] private Vector3 rowDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = transform.position + rowDirection  * i*2;
        }
    }
}
