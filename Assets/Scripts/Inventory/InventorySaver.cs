using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Required for FileStream
using System.Runtime.Serialization.Formatters.Binary;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory myInventory;

    public void SaveScriptableObjects()
    {
        ResetScriptableObjects();

        for (int i = 0; i < myInventory.myInventory.Count; i++)
        {
            FileStream newFile = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i));

            BinaryFormatter binary = new BinaryFormatter();

            var json = JsonUtility.ToJson(myInventory.myInventory[i]);
            binary.Serialize(newFile, json);
            newFile.Close();
        }
    }


    public void LoadScriptableObjects()
    {
        int i = 0;

        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            var temp = ScriptableObject.CreateInstance<InventoryItem>();

            FileStream loadFile = File.Open(Application.persistentDataPath + string.Format("/{0}.inv", i), FileMode.Open);

            BinaryFormatter binary = new BinaryFormatter();

            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(loadFile), temp);

            loadFile.Close();
            myInventory.myInventory.Add(temp);
            i++;
        }
    }


    public void ResetScriptableObjects()
    {
        int i = 0;

        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));

            i++;
        }

    }


    private void OnEnable()
    {
        myInventory.myInventory.Clear();
        LoadScriptableObjects();
    }


    private void OnDisable()
    {
        SaveScriptableObjects();
    }
}
