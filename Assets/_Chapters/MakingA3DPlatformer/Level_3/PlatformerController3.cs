using UnityEngine;

public class PlatformerController3 : MonoBehaviour
{
    [SerializeField] private float jumpStrength;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float extraGravity = 5f;

    Rigidbody rb;
    BoxCollider playerCollider;
    Animator animator;

    Vector3 inputVector;
    Vector3 lastPos;
    Vector3 currentVelocity;
    Vector3 currentDirection;
    bool isGrounded = true;
    bool isJumping = false;
    bool pressJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponentInChildren<BoxCollider>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //we compare our last saved position with the current one to calculate our velocity
        currentVelocity = transform.position - lastPos;
        //by normalizing this value we get our direction in a way that's very easy to process, -1 in the X axis means we're going left, 1 right, etc...
        currentDirection = currentVelocity.normalized;

        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputVector = Vector3.ClampMagnitude(inputVector, 1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            pressJump = true;
            animator.SetTrigger("Jump");
        }
        //we add a check to only update where the character looks when we're actually pressing a direction
        if (inputVector.magnitude > 0.2f)
        {
            transform.GetChild(0).LookAt(transform.position + inputVector);
        }

        //Accessing the animator I set a few parameters so the character animates properly
        //Here I modify the float called "RunSpeed" so it reflects the velocity of the player, in the animator I use this value to multiply the speed of the walk animation
        animator.SetFloat("RunSpeed", inputVector.magnitude);
        //Here I set the bool "Run" to true if the current input exceeds a certain threshold aka I make sure the character only runs if the player is pressing a direction
        //(yes, you can assign a condition to a bool like that, it can be handy!)
        animator.SetBool("Run", inputVector.magnitude > .2f);
        //Same here, I set the animator bool "Fall" to true if we are midair and our velocity on the Y axis is negative (if we are going down, basically)
        animator.SetBool("Fall", currentVelocity.y < -0f && !isGrounded);


        //we record our current position
        lastPos = transform.position;

        //to not clutter the update we put the ground check and the visual debugging into their own methods called in the Update
        GroundCheck();

        VisualDebug();
    }

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, (playerCollider.size.y * .5f)+.1f) && rb.linearVelocity.y <= 0f)
        {
            if (!isGrounded)
                animator.SetTrigger("Land");

            isGrounded = true;
            isJumping = false;
        }
        //if the raycast doesnt touch anything then it means we are midair
        else
        {
            isGrounded = false;
        }
    }

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

    }

    void VisualDebug()
    {
        Debug.DrawRay(transform.position, inputVector, Color.cyan);
        if (isGrounded)
            Debug.DrawRay(transform.position, (Vector3.down * (playerCollider.size.y * .5f + 0.1f)), Color.green);
        else
            Debug.DrawRay(transform.position, (Vector3.down * (playerCollider.size.y * .5f + 0.1f)), Color.red);
    }
}
