using UnityEngine;

public class LookAndShootAtMouseClick : MonoBehaviour
{
    public TravelingProjectile projectilePrefab;

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
            ShootProjectileInDirection();
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

    void ShootProjectileInDirection()
    {
        //let's spawn the projectile we have referenced using the method Instantiate:
        TravelingProjectile newProjectile = Instantiate(projectilePrefab) as TravelingProjectile;
        newProjectile.initialDirection = m_nextProjectileDirection;
        newProjectile.transform.position = transform.position;

        //side note: You could technically only write "Instantiate(projectilePrefab, tranform.position, transform.rotation)"
        //because since Edward is facing the direction we're shooting in, just using his rotation as the rotation the projectile will have when it spawns is a valid solution
        //that being said, I wanted to highlight that when we instantiate prefabs, it's better to do so by putting them in bariable (here: newProjectile)
        //because then it's very easy to set them up exactly the way we want and access them once they are instantiated
    }
}
