using Unity.Cinemachine;
using UnityEngine;


public class FirstPersonCamera : CinemachineExtension
{
    [SerializeField] private GameObject playerModel;
    private float lookXLimit = 45f;
    private float lookSpeed = 10f;
    private float rotationX;
    private float rotationY;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
                
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                
                state.OrientationCorrection = Quaternion.Euler(rotationX, 0, 0);
                
                playerModel.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }
}
