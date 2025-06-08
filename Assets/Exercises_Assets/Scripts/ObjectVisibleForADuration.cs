using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
public class ObjectVisibleForADuration : MonoBehaviour
{
    [SerializeField] private float m_visibleTimer;

    [SerializeField]private float timeElapsed;
    private Renderer m_Renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Renderer.isVisible)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= m_visibleTimer)
            {
                //Complete()
                Debug.Log("FOUND");
            }
        }
    }
}
