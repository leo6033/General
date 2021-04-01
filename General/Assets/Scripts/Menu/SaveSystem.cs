using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public static class SaveSystem
{
    //保存数据
    public static void SavePlayer(PlayerData player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, player);
        stream.Close();
        Debug.Log("save");
    }

    //读取数据
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("load");
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in  " + path);
            return null;
        }
    }
}