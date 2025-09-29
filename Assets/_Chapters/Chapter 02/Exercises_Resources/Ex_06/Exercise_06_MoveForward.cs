using MyBox;
using UnityEngine;

public class Exercises_06_MoveForward : MonoBehaviour
{

    [SerializeField] private float movementSpd = 5f;
    // Update is called once per frame
    void Update()
    {
        //whatever method you use, always multiply the speed of the movement you're applying by Time.deltatime to make it independant from the framerate
        //for example, going right would look like this: Vector.right * Time.deltatime (and then you can multiply again by another float to control the speed)
        
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(0, 0, 1 * movementSpd * Time.deltaTime);

        // Debug.Log(transform.position);
        // Debug.Log(horizontalInput + " " + verticalInput);
    }
}
