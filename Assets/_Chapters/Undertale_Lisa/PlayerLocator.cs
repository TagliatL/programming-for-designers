using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    [SerializeField] private Undertale_Controller_Lisa playerScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Undertale_Controller_Lisa>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerScript.speed);
    }
}
