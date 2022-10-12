using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    #region Singleton

    public static SaveLoadSystem instance;

    private void Awake()
    {
        Debug.Log(savePath);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        Load();
        GameManager.instance.onSceneChanged += Save;
    }

    //Tengo que guardar cuando salgo de la aplicacion, y cuando cambio de escena
    private void OnApplicationQuit()
    {
        Save();
    }

    public string savePath => $"{Application.persistentDataPath}/save.txt";

    [ContextMenu("Save")]
    void Save()
    {
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
    }

    [ContextMenu("Load")]
    void Load()
    {
        Debug.Log("loaded");
        var state = LoadFile();
        LoadState(state);
    }

    public void SaveFile(object state)
    {
        using (var stream = File.Open(savePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();

            formatter.Serialize(stream, state);
        }
    }

    Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save file found");
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(savePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    void SaveState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.SaveState();
        }
    }

    void LoadState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }
}
