using System.Collections.Generic;
using UnityEngine;

public class BiomesData : MonoBehaviour
{
    [Header("Biomes")]
    [SerializeField] List<Biome> _biomes;

    public List<Biome> GetBiomes()
    {
        return _biomes;
    }
}
