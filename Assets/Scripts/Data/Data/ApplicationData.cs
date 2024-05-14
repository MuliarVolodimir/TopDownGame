using System;

[Serializable]
public class ApplicationData
{
    public string PlayerName;
    public string SelectedCharacterIndex;
    public int PlayerLVL;
    public bool NewGame;
    public string[] InventoryItems;


    public MapData MapData;
    public SelectedCharacterInfo SelectedCharacterInfo = new SelectedCharacterInfo();

    public ApplicationData(string playerName, int playerLVL, bool newGame, MapData currentMap, string selectedCharacterIndex)
    {
        PlayerName = playerName;
        PlayerLVL = playerLVL;
        MapData = currentMap;
        NewGame = newGame;
        SelectedCharacterIndex = selectedCharacterIndex;
    }  
}