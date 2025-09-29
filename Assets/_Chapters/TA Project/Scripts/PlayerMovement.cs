using System;
using System.ComponentModel;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float walkSpeed;
    public float runSpeed;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    
    [Header("Camera Angles")] 
    [SerializeField] private GameObject ThirdPerson;
    [SerializeField] private GameObject FirstPerson;
    [SerializeField] private GameObject BirdsEyeView;

    
    [Header("SerializeFields")]
    [Description("Player Perspective")]
    [SerializeField] private PlayerPerspective PerspectiveEnum;
    
    [Description("PlayerMovement Config")]
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private bool canMove = true;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection.y = 0f;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        
        transform.position += moveDirection * Time.deltaTime;
        
        if (canMove)
        {
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            CameraSwitch();
        }
    }

    private enum PlayerPerspective
    {
        ThirdPerspective01,
        FirstPerspective,
        BirdsEyeView,
        ThirdPerspective02,
    }

    private void CameraSwitch()
    {
        if (PerspectiveEnum.Equals(PlayerPerspective.ThirdPerspective01))
        {
            PerspectiveEnum = PlayerPerspective.FirstPerspective;
            FirstPerson.SetActive(true);
            ThirdPerson.SetActive(false);
        }
        else if (PerspectiveEnum.Equals(PlayerPerspective.FirstPerspective))
        {
            PerspectiveEnum = PlayerPerspective.BirdsEyeView;
            BirdsEyeView.SetActive(true);
            FirstPerson.SetActive(false);
        }
        else if (PerspectiveEnum.Equals(PlayerPerspective.BirdsEyeView))
        {
            PerspectiveEnum = PlayerPerspective.ThirdPerspective02;
            ThirdPerson.SetActive(true);
            var cameraDist = ThirdPerson.GetComponent<CinemachineOrbitalFollow>();
            cameraDist.Radius = 8;
            BirdsEyeView.SetActive(false);
        }
        else if (PerspectiveEnum.Equals(PlayerPerspective.ThirdPerspective02))
        {
            PerspectiveEnum = PlayerPerspective.ThirdPerspective01;
            var cameraDist = ThirdPerson.GetComponent<CinemachineOrbitalFollow>();
            while (cameraDist.Radius > 4)
            {
                cameraDist.Radius = cameraDist.Radius - 0.1f * Time.deltaTime;
            }
        }
    }
}
