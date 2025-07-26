using UnityEngine;

public class PlatformerController1 : MonoBehaviour
{
    [SerializeField] private float jumpStrength;
    [SerializeField] private float movementSpeed;

    //we will need the rigidbosy present on this game object
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //we fetch the rigidbody so we can access it for various physics related shenanigans
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //the most basic implementation of a jump: we simply apply a force upward and pray to the gods that the global gravity
            //that we have set in the unity project will bring the character down
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
            //it's a very common way to apply force in a direction (Vector3.up) and with a certain strength (here by multiplying it by our variable)
        }

        //horizontal movement
        //here we store in a Vector3 two input axises (read about it if you haven't, it's important but also a bit antiquated now that unity has a new input system)
        //still usefull for simple usecases like this one though
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //this next if statement is just a deadzone, mostly useful when playing with a joystick, as keyboard key presses are pretty binary
        if (inputVector.magnitude > 0.2f)
        {
            //the player is a rigidbody so we want to properly move it, this is a way to do it without using forces, dont use transform.Translate on rigidbodies
            //and dont set the position of an object with a rigidbody directly, instead use:
            rb.MovePosition(rb.position + inputVector * movementSpeed * Time.deltaTime);

            //dont worry too much about time.deltatime, the rule usually is to apply it to constant motion happening overtime (like in this update)
            //to make sure the movement is independant from the framerate, otherwise if the game runs faster, the character will as well
        }
    }
}
