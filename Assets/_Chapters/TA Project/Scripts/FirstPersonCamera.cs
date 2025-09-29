using Unity.Cinemachine;
using UnityEngine;


public class FirstPersonCamera : CinemachineExtension
{
    [SerializeField] private GameObject PlayerModel;
    private float lookXLimit = 45f;
    private float rotationX;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                rotationX += -Input.GetAxis("Mouse Y") * 10;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                state.RawOrientation = Quaternion.Euler(rotationX, Input.GetAxis("Mouse X") * 10, 0);
                PlayerModel.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 10, 0);
            }
        }
    }
}
