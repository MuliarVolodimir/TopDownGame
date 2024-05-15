using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    [SerializeField] int _mapSize;
    [SerializeField] GameObject _mapPrefab;

    [Header("Biomes")]
    [SerializeField] BiomeSO _biome;

    private List<Room> _createdRooms;

    // data to save
    private ApplicationData _appData;
    private MapData _mapData;

    [Inject]
    public void Construct(ApplicationData applicationData, BiomesData biomesData)
    {
        _appData = applicationData;
        _mapData = applicationData.MapData;

        _biome = FindBiome(biomesData);
    }

    private BiomeSO FindBiome(BiomesData biomesData)
    {
        var biomes = biomesData.GetBiomes();
        var biome = new BiomeSO();

        if (_appData.MapData.BiomeIndex != null)
        {
            biome = biomes.Find(b => b.Index == _appData.MapData.BiomeIndex);
        }
        else
        {
            biome = biomes[Random.Range(0, biomes.Count)];
        }

        return biome;
    }

    void Start()
    {
        if (_appData.NewGame)
        {
            InitializeMap();
            GenerateMap();
        }
        else
        {
            LoadMap();
        }   
    }

    private void InitializeMap()
    {
        _mapData = null;

        _mapData = new MapData(new string[_mapSize], new int[_mapSize], 0);
        _mapData.BiomeIndex = _biome.Index;

        int shopIndex = Random.Range(1, _mapData.MapModel.Length - 2);

        for (int i = 0; i < _mapData.MapModel.Length; i++)
        {
            if (i <= 0)
            {
                _mapData.MapModel[i] = "*";
            }
            else if (i >= _mapData.MapModel.Length - 1)
            {
                _mapData.MapModel[i] = "@";
            }

            if (i > 0 && i < _mapData.MapModel.Length - 1)
            {
                if (i != shopIndex)
                {
                    _mapData.MapModel[i] = "#";
                }
                else
                {
                    _mapData.MapModel[i] = "$";
                }
            }
        }
    }

    private void GenerateMap()
    {
        _createdRooms = new List<Room>();
        GameObject[] roomObjects = null;

        int roomIndex = 0;
        string previousSeedSymbol = "";

        for (int i = 0; i < _mapData.MapModel.Length; i++)
        {
            if (_mapData.MapModel[i] != previousSeedSymbol || roomIndex < 0)
            {
                roomObjects = GetRoomFromMapModel(_mapData.MapModel[i]).ToArray();
                roomObjects = ShuffleObjects(roomObjects);
                roomIndex = roomObjects.Length - 1;
            }

            var room = roomObjects[roomIndex].GetComponent<Room>();
            var newRoom = Instantiate(room, _mapPrefab.transform);

            if (_createdRooms.Count > 0)
            {
                newRoom.transform.position = _createdRooms[_createdRooms.Count - 1].EndPoint.position - newRoom.StartPoint.localPosition;
            }
            _mapData.MapSeed[i] = roomIndex;
            _createdRooms.Add(newRoom);

            previousSeedSymbol = _mapData.MapModel[i];
            roomIndex--;
        }
        _appData.MapData = _mapData;
        _appData.NewGame = false;

        // to debug
        string mapToDebug = "Seed : ";
        for (int i = 0; i < _mapData.MapSeed.Length; i++)
        {
            mapToDebug += _mapData.MapSeed[i];
        }

        Debug.Log($"seed: {mapToDebug}, biome: {_mapData.BiomeIndex}");
    }

    private GameObject[] ShuffleObjects(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            int j = Random.Range(0, i + 1);

            var a = objects[i];
            objects[i] = objects[j];
            objects[j] = a;
        }
        return objects;
    }

    private void LoadMap()
    {
        _createdRooms = new List<Room>();
        for (int i = 0; i < _mapData.MapModel.Length; i++)
        {
            var roomObjects = GetRoomFromMapModel(_mapData.MapModel[i]);

            if (roomObjects != null)
            {
                var room = roomObjects[_mapData.MapSeed[i]].GetComponent<Room>();
                var newRoom = Instantiate(room, _mapPrefab.transform);
                

                if (_createdRooms.Count > 0)
                {
                    newRoom.transform.position = _createdRooms[_createdRooms.Count - 1].EndPoint.position - newRoom.StartPoint.localPosition;
                }
                _createdRooms.Add(newRoom);
            }    
        }

        // to debug
        string mapToDebug = "";
        for (int i = 0; i < _mapData.MapSeed.Length; i++)
        {
            mapToDebug += _mapData.MapSeed[i];
        }

        Debug.Log($"Map loaded, seed : {mapToDebug}, biome: {_mapData.BiomeIndex}");
    }

    private List<GameObject> GetRoomFromMapModel(string curRoom)
    {
        switch (curRoom)
        {
            case "*": // main room
                return _biome._mainRoomPrefabs;
            case "#": // normal room
                return _biome._normalRoomPrefabs;
            case "$": // shop
                return _biome._shopRoomPrefabs;
            case "@": // boss room
                return _biome._bossRoomPrefabs;
            default:
                break;
        }

        return null;
    }
}
