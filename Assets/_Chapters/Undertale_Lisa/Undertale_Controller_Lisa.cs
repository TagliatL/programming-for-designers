using UnityEngine;

public class Undertale_Controller_Lisa : MonoBehaviour
{
    public float speed = 4f;

    public Transform arenaFloor;

    private float upperLimit;
    private float leftLimit;
    private float downLimit;
    private float rightLimit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upperLimit = arenaFloor.transform.position.z + arenaFloor.transform.lossyScale.z / 2f;
        downLimit = arenaFloor.transform.position.z - arenaFloor.transform.lossyScale.z / 2f;
        rightLimit = arenaFloor.transform.position.x + arenaFloor.transform.lossyScale.x / 2f;
        leftLimit = arenaFloor.transform.position.x - arenaFloor.transform.lossyScale.x / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
        transform.position += inputDirection * speed * Time.deltaTime;
        
        // map constraint for x-axis
        if (transform.position.x > rightLimit)
        {
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
        } 
        else if (transform.position.x < leftLimit)
        {
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
        }

        // map constraint for z-axis
        if (transform.position.z > upperLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperLimit);
        }
        else if (transform.position.z < downLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, downLimit);
        }
    }
}
