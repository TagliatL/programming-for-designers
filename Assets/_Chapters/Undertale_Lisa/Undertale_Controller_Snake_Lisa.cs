using NUnit.Framework.Constraints;
using UnityEngine;

public class Undertale_Controller_Snake_Lisa : MonoBehaviour
{
    public float speed = 1f;

    public Vector3 mapConstraint = new Vector3(5.5f, 0.0f, 3.5f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
        //transform.position += inputDirection * speed * Time.deltaTime;
        transform.position += transform.forward * speed * Time.deltaTime;
        if(inputDirection.magnitude>.25f)
        {
            transform.rotation = Quaternion.LookRotation(inputDirection);
        }

        // map constraint for x-axis
        if (transform.position.x > mapConstraint.x)
        {
            transform.position = new Vector3(mapConstraint.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -mapConstraint.x)
        {
            transform.position = new Vector3(-mapConstraint.x, transform.position.y, transform.position.z);
        }

        // map constraint for z-axis
        if (transform.position.z > mapConstraint.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, mapConstraint.z);
        }
        else if (transform.position.z < -mapConstraint.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -mapConstraint.z);
        }
    }
}
