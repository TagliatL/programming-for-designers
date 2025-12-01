using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokedex", menuName = "ScriptableObjects/Pokedex", order = 1)]
public class PokedexSO : ScriptableObject
{
    public enum PkmnType
    {
        FIRE,
        GRASS,
        DRAGON,
        WATER,
        FAIRY,
        FLYING,
        NORMAL
    }

    [Serializable]
    public class PokemonEntry
    {
        public string name;
        public Sprite sprite;
        public PkmnType type;
    }

    public List<PokemonEntry> listOfPkmn;
}
