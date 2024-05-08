using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VContainer;

public class ApplicationDataSaver
{
    private string _saveFilePath = Application.dataPath + "/ApplicationData.json";
    private ApplicationData _applicationData;

    [Inject]
    public void Constract(ApplicationData applicationData)
    {
        _applicationData = applicationData;
    }

    public void SaveData()
    {
        ApplicationData applicationData = _applicationData;

        string savePlayerData = JsonUtility.ToJson(applicationData);
        File.WriteAllText(_saveFilePath, savePlayerData);
        Debug.Log("Data Saved");
    }

    public ApplicationData LoadData()
    {
        if (File.Exists(_saveFilePath))
        {
            string applicationDataPath = File.ReadAllText(_saveFilePath);
            var applicationData = JsonUtility.FromJson<ApplicationData>(applicationDataPath);

            Debug.Log("Data Loaded");
            return applicationData;
        }
        else
        {
            Debug.Log("No save file found");
        }

        return new ApplicationData("Guest", 1, true, new MapData(null, null, 0), "");
    }
}
