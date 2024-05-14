using System;

[Serializable]
public class MapData
{
    public string[] MapModel;
    public int[] MapSeed;
    public string BiomeIndex;
    public int CurrentGameLvlIndex;

    public MapData(string[] mapModel, int[] mapSeed, int currentGameLvlIndex)
    {
        MapModel = mapModel;
        MapSeed = mapSeed;
        CurrentGameLvlIndex = currentGameLvlIndex;
    }
}
