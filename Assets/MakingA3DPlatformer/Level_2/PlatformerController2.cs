using UnityEngine;

public class PlatformerController2 : MonoBehaviour
{
    [SerializeField] private float jumpStrength;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float extraGravity = 5f;

    Rigidbody rb;
    BoxCollider collider;
    Vector3 inputVector;
    bool isGrounded = true;
    bool isJumping = false;
    bool pressJump = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //here we get the collider contained in the child of the Player using GetComponentInChildren
        collider = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            pressJump = true;
        }

        //let's make the player look in the direction it's moving by accessing the child Cube and then rotating it
        //here I use the very useful method LookAt that can take a position and I simply add my current inputs to the current position of the object
        transform.GetChild(0).LookAt(transform.position + inputVector);

        //here I will code some visual debuggin to help visualize in the scene what values and directions we're working with
        //first, current player input direction
        Debug.DrawRay(transform.position,  inputVector, Color.cyan);

        //next, a visual representaton of the raycast that checks for the ground
        //since I'm feeling fancy, I'll change the color depending on the grounding status
        if(isGrounded)
            Debug.DrawRay(transform.position, (Vector3.down * (collider.size.y * .5f + 0.1f)), Color.green);
        else
            Debug.DrawRay(transform.position, (Vector3.down * (collider.size.y * .5f + 0.1f)), Color.red);
    }

    //something I didnt do in Level 1 that needs addressing: if we are applying forces to a rigidbody we need to do so in the FixedUpdate 
    //BUT the input detection should still be happening in the regular Update, here I simply have a bool that I set to true once I press Space
    //and then I check fr this value (pressjump) in my FixedUpdate to properly jump!
    //the movement using inputVector also happens in FixedUpdate but is calculated in the Update
    //This is only needed when we manipulate rigidbodies and I I had made a bespoke controller without any physics I wouldn't need to do that
    private void FixedUpdate()
    {
        //jump
        if (pressJump)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
            isJumping = true;
            pressJump = false;
        }

        //horizontal movement
        if (inputVector.magnitude > 0.2f)
        {
            rb.MovePosition(rb.position + inputVector * movementSpeed * Time.fixedDeltaTime);
        }

        //this constant extra force going down is a cheap trick I use to make the jump more snappy
        //because it's a constant gravity, we need to make our jump stronger to "fight" it going up
        rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);

        //we use a raycast and use it to check for the floor under us
        //it starts in the center of the object (transform.position), goes down (Vector3.down) and go as far as the size of the player collider / 2
        //I add a 0.1f value to the max distance of the raycast just so the check goes a bit beyond the size of the collider

        //in this case, it's to check to see if the player is on the ground, if it is, we can jump
        if(Physics.Raycast(transform.position, Vector3.down, (collider.size.y * .5f) + .1f))
        {
            isGrounded = true;
            isJumping = false;
        }
        //if the raycast doesnt touch anything then it means we are midair
        else
        {
            isGrounded = false;
        }
    }
}
