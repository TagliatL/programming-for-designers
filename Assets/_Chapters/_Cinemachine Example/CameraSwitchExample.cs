using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitchExample : MonoBehaviour
{
    public CinemachineCamera zoomInCam;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            zoomInCam.gameObject.SetActive(true);


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            zoomInCam.gameObject.SetActive(false);
    }
}
