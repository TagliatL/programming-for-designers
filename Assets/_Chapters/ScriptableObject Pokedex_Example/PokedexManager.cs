using UnityEngine;

public class PokedexManager : MonoBehaviour
{
    public static PokedexManager Instance;

    public PokedexSO pokedex;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public PokedexSO.PokemonEntry GetPokemon(int index)
    {
        return pokedex.listOfPkmn[index];
    }
}
