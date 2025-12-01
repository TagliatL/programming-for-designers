using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Undertale_Controller_Lisa playerScript = other.GetComponent<Undertale_Controller_Lisa>();
            if(playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
        }
    }
}
