using UnityEngine;

public class ArenaBehaviour : MonoBehaviour
{
    public Vector3 defaultScale;
    public Vector3[] customScales;
    public float waitingTime = 2f;
    public float lerpSpeed = 1f;

    float timer;
    float lerpT;
    int customIndex = 0;
    bool isDefault = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = defaultScale;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitingTime)
        {
            lerpT += lerpSpeed * Time.deltaTime;

            if (isDefault == true)
            {
                RescaleArena(defaultScale, customScales[customIndex]);
            }
            else
            {
                RescaleArena(customScales[customIndex], defaultScale);
            }
        }
    }

    void RescaleArena(Vector3 start, Vector3 end)
    {
        transform.localScale = Vector3.Lerp(start, end, lerpT);

        if (lerpT >= 1f)
        {
            timer = 0f;
            lerpT = 0f;

            if (isDefault == false)
            {
                customIndex++;
            }

            if (customIndex == customScales.Length)
            {
                customIndex = 0;
            }

            isDefault = !isDefault;
        }
    }
}
