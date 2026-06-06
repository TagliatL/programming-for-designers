using UnityEngine;

public class LookAndHitscanAtMouseClick : MonoBehaviour
{
    public GameObject hitFXPrefab;

    private Vector3 m_nextProjectileDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //let's detect when we left click on our mouse
        // 0 is left, 1 is right click
        if (Input.GetMouseButtonDown(0))
        {
            EdwardLookAtClick();
            ShootInDirection();
        }
    }

    void EdwardLookAtClick()
    {
        //next we cast a ray where our mouse is using a raycast and the ScreenPointToRay method taking the mouse position as parameter
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 clickPosition = hit.point;
            //since we are using LookAt() to look at the position of the raycast intersecting with the ground, we dont want to look up or down
            //so here, I make sure Edward wont look at a direction that's higher or lower than his own:
            clickPosition.y = transform.position.y;

            //we store it as a proper direction:
            m_nextProjectileDirection = (clickPosition - transform.position).normalized;

            //remember, this script is attached to our boy Edward, meaning that just typing "transform" refers to HIS transform
            transform.LookAt(clickPosition);
        }
    }

    void ShootInDirection()
    {
        //hitscan hits are instant, here we fire a raycast and if something interesects we get the point of intersection and instantiate our explosion there
        RaycastHit hit;
        if (Physics.Raycast(transform.position, m_nextProjectileDirection, out hit))
        {
            Vector3 hitPosition = hit.point;
            GameObject newHitFX = Instantiate(hitFXPrefab, hit.point, Quaternion.identity);
        }
    }
}
