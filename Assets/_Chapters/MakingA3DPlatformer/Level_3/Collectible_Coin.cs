using UnityEngine;

public class Collectible_Coin : Collectible_Abstract
{
    public override void Collect()
    {
        //we use the original behavior of Collectible_Abstract.Collect() aka the destruction of the gameobject
        base.Collect();
    }
}
