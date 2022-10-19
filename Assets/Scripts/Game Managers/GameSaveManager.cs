using System.Collections;
using System.Collections.Generic;
using System.IO; // Required for FileStream
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    public List<ScriptableObject> objects = new List<ScriptableObject>();


    public void SaveScriptableObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            FileStream newFile = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));

            BinaryFormatter binary = new BinaryFormatter();

            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(newFile, json);
            newFile.Close();
        }
    }


    public void LoadScriptableObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream loadFile = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);

                BinaryFormatter binary = new BinaryFormatter();

                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(loadFile), objects[i]);

                loadFile.Close();
            }
            else
            {
                Debug.Log("Trying to load data which doesn't exist - " + (string)objects[i].name);
            }
        }
    }


    public void ResetScriptableObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            }
        }
    }


    private void Awake()
    {
        if (gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            //Destroy(this);
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    private void OnEnable()
    {
        LoadScriptableObjects();
    }


    private void OnDisable()
    {
        SaveScriptableObjects();
    }
}
