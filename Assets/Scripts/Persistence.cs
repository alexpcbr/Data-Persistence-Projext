using System.IO;
using UnityEngine;

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

        
        LoadScore();
    }

    //Code related to saving and loading the data score

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
