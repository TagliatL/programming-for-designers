using UnityEngine;

public class TravelingProjectile : MonoBehaviour
{
    public GameObject explosionFXPrefab;
    public float speed = 10f;
    [HideInInspector]public Vector3 initialDirection;

    private Vector3 m_lastPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_lastPos = transform.position;

        //let's make the projectile face the right direction on Start, this value is set by the other script instantiating a projectile
        transform.forward = initialDirection;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        //here comes the cool part: we are RAYCASTING AGAIN...TWICE
        //this time, it's to account for bullets that are too fast they could pass through wall inbetween two frames
        //we use a raycast from our current position to the last frame's position and see if there is any wall that intersects
        float rayDist = Vector3.Distance(transform.position, m_lastPos);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward,out hit, rayDist))
        {
            //do we hit something? then we EXPLODE
            transform.position = hit.point;
            Explode();
        }

        // (I swear 0.375 isn't a magic number, it's half the size of the bullet model I'm using)
        if (Physics.Raycast(transform.position, transform.forward, .375f))
        {
            Explode();
        }

        //we save our position in the frame once we've done everything else so we can compare it to our position next frame. Technologia!
        m_lastPos = transform.position;
    }
    void Explode()
    {
        //Quaternion.identity is just the default value for a rotation, the object will have a rotation of (0,0,0)
        Instantiate(explosionFXPrefab, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
}
