using System.Collections.Generic;
using UnityEngine;

public class BiomesData : MonoBehaviour
{
    [Header("Biomes")]
    [SerializeField] List<BiomeSO> _biomes;

    public List<BiomeSO> GetBiomes()
    {
        return _biomes;
    }
}
