using System.Collections;
using UnityEngine;

public class ArenaBehaviour : MonoBehaviour
{

    private float horizontalMax = 12f;
    private float horizontalMin = 4f;

    private float verticalMax = 8f;
    private float verticalMin = 2f;

    private float horizontalFrame = 0;

    private int scaleSpeed = 2;

    private bool reverseScaleH = false;
    private bool reverseScaleV = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        horizontalFrame = Time.time;
        // Debug.Log(horizontalFrame);
        if (transform.lossyScale.x != horizontalMin && horizontalFrame >= 5 && !reverseScaleH)
        {
            transform.localScale = new Vector3(transform.lossyScale.x - (1 * scaleSpeed * Time.deltaTime), 1f, transform.lossyScale.z);

            if (transform.lossyScale.x < horizontalMin)
            {
                transform.localScale = new Vector3(horizontalMin, 1f, transform.lossyScale.z);
                horizontalFrame = 0;
                reverseScaleH = true;
            }
        }

        if (transform.lossyScale.x != horizontalMax && horizontalFrame >= 7 && reverseScaleH)
        {
            transform.localScale = new Vector3(transform.lossyScale.x + (1 * scaleSpeed * Time.deltaTime), 1f, transform.lossyScale.z);

            if (transform.lossyScale.x > horizontalMax)
            {
                transform.localScale = new Vector3(horizontalMax, 1f, transform.lossyScale.z);
                horizontalFrame = 0;
                reverseScaleH = false;
            }

        }
    }

    /*
     if (transform.lossyScale.z != verticalMin && !reverseScaleV)
        {
            transform.localScale = new Vector3(transform.lossyScale.x, 1f, transform.lossyScale.z - (1 * scaleSpeed * Time.deltaTime));

            if (transform.lossyScale.z < verticalMin) { reverseScaleV = true; }
        }

        if (transform.lossyScale.z != verticalMax && reverseScaleV)
        {
            transform.localScale = new Vector3(transform.lossyScale.x, 1f, transform.lossyScale.z + (1 * scaleSpeed * Time.deltaTime));

            if (transform.lossyScale.z > verticalMax) { reverseScaleV = false; }

        }
     
     */
}
