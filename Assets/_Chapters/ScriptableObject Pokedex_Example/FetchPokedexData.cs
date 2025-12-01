using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FetchPokedexData : MonoBehaviour
{
    public Image pkmnImage;
    public TMP_Text pkmnType;
    public TMP_Text pkmnName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PokedexSO.PokemonEntry entry = PokedexManager.Instance.GetPokemon(1);
        pkmnImage.sprite = entry.sprite;
        pkmnType.text = entry.type.ToString();
        pkmnName.text = entry.name;
    }

}
