using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformerController3_SonicDash : MonoBehaviour
{
    [SerializeField] private float jumpStrength;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float extraGravity;
    [SerializeField] private GameObject targetMarkerPrefab;

    Rigidbody rb;
    BoxCollider playerCollider;
    Animator animator;

    Vector3 inputVector;
    Vector3 lastPos;
    Vector3 currentVelocity;
    Vector3 currentDirection;
    Vector3 lastPressedDirection;
    Transform currentTarget;
    GameObject showedTargetMarker;

    bool isGrounded = true;
    bool isJumping = false;
    bool pressJump = false;
    bool isDash = false;

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

        if (isJumping && !isDash)
        {
            DetectEnemies();
        }
        else if (!isDash)
        {
            currentTarget = null;
            Destroy(showedTargetMarker);
        }

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
            lastPressedDirection = currentDirection;
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

    void DetectEnemies()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, 8f, currentDirection, 10f);
        for (int i = 0; i < hits.Count(); i++)
        {
            if (hits[i].collider.CompareTag("Targetable"))
                currentTarget = hits[i].transform;
        }

        if (showedTargetMarker == null)
        {
            showedTargetMarker = Instantiate(targetMarkerPrefab);
        }
        else if (currentTarget != null)
        {
            showedTargetMarker.transform.position = currentTarget.position;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = Vector3.zero;
                rb.isKinematic = true;
                isDash = true;
            }
        }

        if (currentTarget == null)
        {
            Destroy(showedTargetMarker);
        }
    }

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, (playerCollider.size.y * .5f) + .1f) && rb.linearVelocity.y <= 0f)
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

    private void OnTriggerEnter(Collider other)
    {
        //when entering a trigger, is it a collectible?
        Collectible_Abstract collectible = other.GetComponent<Collectible_Abstract>();
        if (collectible != null)
        {
            //if it is indeed a collectible, we collect it!
            //by using an abstract class for all collectibles, we can keep this part of the code VERY generic and then have special cases in the collectibles themselves
            collectible.Collect();
        }

    }

    private void FixedUpdate()
    {
        if (isDash)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, currentTarget.position, 1f));
            if (rb.position == currentTarget.position)
            {
                Destroy(currentTarget.gameObject);
                rb.isKinematic = false;
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
                isJumping = true;
                pressJump = false;
                isDash = false;
            }
            return;
        }


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
