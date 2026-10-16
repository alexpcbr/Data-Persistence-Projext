using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Persistence : MonoBehaviour
{
    public static Persistence Instance { get; private set; }

    public int mainPoints;
    public int bestPoints;
    public  string bestNamePlayer;
    public string namePlayer;

    //public ResourceDatabase ResourceDatabase;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        mainPoints = 0;
        bestPoints = 0;
        namePlayer = "";

        //ResourceDatabase.Init();

        //we try to load the savefile if there is one to retrieve the right color
        LoadScore();
    }

    //Code related to saving and loading the color

    //we wrap the color in a class as Json utility need it. And mark this class as Serializable.
    //If we ever want to save more thing (like name, or score etc..) we can just add it to that class!
    [System.Serializable]
    public class SaveData
    {
        public int mainPoints;
        public int bestPoints;
        public  string bestNamePlayer;
        public string namePlayer;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.mainPoints = mainPoints;
        data.bestPoints = bestPoints;
        data.bestNamePlayer = bestNamePlayer;
        data.namePlayer = namePlayer;

        string json = JsonUtility.ToJson(data, true);

        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        File.WriteAllText(path, json);
        Debug.Log("Arquivo salvo com SUCESSO em: " + path); // Diagnóstico 2 
    }

    public void LoadScore()
    {
        //string path = Application.persistentDataPath + "/savefile.json";
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            namePlayer = data.namePlayer;
            bestNamePlayer = data.bestNamePlayer;
            bestPoints = data.bestPoints;
            mainPoints = data.mainPoints;

        }
    }
}
/*


public class MainManager : MonoBehaviour
{
    public static Persistence Instance { get; private set; }

    public int mainPoints;
    public int bestPoints;
    public string name;
    
    //public ResourceDatabase ResourceDatabase;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        mainPoints = 0;
        bestPoints = 0;
        name = "";
        
        //ResourceDatabase.Init();

        //we try to load the savefile if there is one to retrieve the right color
        LoadScore();
    }

    //Code related to saving and loading the color

    //we wrap the color in a class as Json utility need it. And mark this class as Serializable.
    //If we ever want to save more thing (like name, or score etc..) we can just add it to that class!
    [System.Serializable]
    class SaveData
    {
         public int mainPoints;
    public int bestPoints;
    public string name;
    }
    
    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.mainPoints = mainPoints;
        data.bestPoints = bestPoints;
        data.name = name;

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            name = data.name;
            bestPoints = data.bestPoints;
            mainPoints = data.mainPoints;
            
        }
    }
}

*/