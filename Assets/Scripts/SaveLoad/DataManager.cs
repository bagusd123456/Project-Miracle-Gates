using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IData> dataObjectList;
    private FileDataHandler dataHandler;
    public static DataManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more tha one DataManager");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataObjectList = FindAllDataObjects();
        LoadData();
    }

    public void NewData()
    {
        this.gameData = new GameData();
    }

    public void LoadData()
    {
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("No Data Found. Creating New Data...");
            NewData();
        }

        foreach (IData dataObject in dataObjectList)
        {
            dataObject.LoadData(gameData);
        }
    }

    public void SaveData()
    {
        foreach (IData dataObject in dataObjectList)
        {
            dataObject.SaveData(gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private List<IData> FindAllDataObjects()
    {
        IEnumerable<IData> dataObjectList = FindObjectsOfType<MonoBehaviour>().OfType<IData>();

        return new List<IData>(dataObjectList);
    }
}
