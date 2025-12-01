using UnityEngine;

public class Undertale_Controller_Snake_Lisa : MonoBehaviour
{
    public float speed = 1f;

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
            transform.rotation = Quaternion.LookRotation(inputDirection);
       
    }
}
