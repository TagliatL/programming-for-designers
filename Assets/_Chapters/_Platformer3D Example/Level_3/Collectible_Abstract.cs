using UnityEngine;

public abstract class Collectible_Abstract : MonoBehaviour
{
    public virtual void Collect()
    {
        //this method is going to be called by the character when it enters the collectible's trigger
        Destroy(gameObject);
    }
}
