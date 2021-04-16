using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static string filePathName = "/gameData.9LivesStudioIsAwesome";
    public static void SaveData (HasClearedLevelController controller)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + filePathName;
        FileStream stream = new FileStream(path, FileMode.Create);

        Debug.Log("Saving data to " + path + "...");

        GameData data = new GameData(controller);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + filePathName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            Debug.Log("Successfully loaded data from " + path);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    /*
    public static void DeleteData()
    {
        string path = Application.persistentDataPath + filePathName;
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
                Debug.Log("Successfully deleted the file path " + path);
            }
            catch
            {
                Debug.LogError("Could not delete the file path " + path);
            }
        }
    }
    */
}
