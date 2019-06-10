using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;


public class CSVReadWrite : MonoBehaviour {

    Scene _mScene;
    string _mSceneName; 
    public int ID = 0;
    public int intervalID = 1;
    GameManager gameManager;
    public int nombreDeSauvegardes = 0;

    public List<List<string>> rowData = new List<List<string>>();
    public List<float> rowDataTemp2 = new List<float>();
    public List<string> rowDataTemp = new List<string>();


    [Header("Check Pour RESET l'ID")]
    public bool checkToReset = false;

    private bool checkToReset2;

    void CreateFirstRow()
    {
        List<string> rowDataTemp = new List<string>();
        rowDataTemp.Add(_mSceneName + "; ID =; "+ID);       
        rowData.Add(rowDataTemp);
    }
    void CreateSecondRow()
    {
        List<string> rowDataTemp = new List<string>();       
        for (int i = 0; i < 20; i++)
        {
            intervalID++;
            rowDataTemp.Add("interval " + intervalID.ToString());

        }
        rowData.Add(rowDataTemp);
    }
    
    public void GetData()
    { 
        rowDataTemp2 = gameManager.timeBetweenEvents;       
        for (int i = 0; i < rowDataTemp2.Count; i++)
        {
            if (!rowDataTemp.Contains(gameManager.timeBetweenEvents[i].ToString()))
            {
                rowDataTemp.Add( i.ToString() + " e place dans le tableau = ; " +(gameManager.timeBetweenEvents[i]).ToString());
            }
        }
        rowData.Add(rowDataTemp);
    }
    
    private void Start()
    {
       

        ID = PlayerPrefs.GetInt("ID", ID);        
        ID++;
        PlayerPrefs.SetInt("ID", ID);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _mScene = SceneManager.GetActiveScene();
        _mSceneName = _mScene.name;
        CreateFirstRow();
        CreateSecondRow();
        Save();
        rowData.Clear();
        
    }

    public void Save()
    {
             
       
        string[][] output = new string[rowData.Count][];
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i].ToArray();
        }
        int length = output.GetLength(0);
        string delimiter = ";";
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        string filePath = getPath();
        //Check if the file already exists
        
            if (System.IO.File.Exists(filePath))
            {
                //Debug.Log("The file " + _mSceneName + ID + ".csv " + "exists already!");
                // StreamWriter outStream = System.IO.File.CreateText(filePath);
                // outStream.WriteLine(sb);
                // outStream.Close();
                GetData();
                StreamWriter outStream = System.IO.File.AppendText(filePath);
                outStream.WriteLine(sb);
                outStream.Close();

            }
            //If the file doesn't exist, create the file
            else
            {
                GetData();
                StreamWriter outStream = System.IO.File.CreateText(filePath);
                outStream.WriteLine(sb);
                outStream.Close();
            }
        
        
                     
        
        
    }

    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + _mSceneName + ID +".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }

    private void Update()
    {
        if(checkToReset == true && checkToReset2 == false)
        {
            checkToReset2 = true;
            PlayerPrefs.SetInt("ID", 0);
        }
    }

}
