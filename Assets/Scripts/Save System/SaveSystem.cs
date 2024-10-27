using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static SaveData saveData = new SaveData();
    
    [System.Serializable]
    public struct SaveData
    {
        public PlayerSaveData PlayerData;
        public DropsSaveData DropsData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();
        
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(saveData, true));
    }

    private static void HandleSaveData()
    {
        //GameManager.instance.Player.Save(ref saveData.PlayerData);
        GameManager.instance.Drops.Save(ref saveData.DropsData);
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());
        saveData = JsonUtility.FromJson<SaveData>(saveContent);
        
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        //GameManager.instance.Player.Load(saveData.PlayerData);
        GameManager.instance.Drops.Load(saveData.DropsData);
    }
}