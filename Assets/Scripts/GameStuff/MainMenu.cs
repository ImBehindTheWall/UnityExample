using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        for (int i = 0; i <= scriptableObjects.Count; i++)
        {
            switch (scriptableObjects[i].GetType().FullName)
            {
                case "FloatValue":
                    FloatValue ftmp = (FloatValue)scriptableObjects[i];
                    ftmp.RunTimeValue = ftmp.initialValue;
                    Debug.Log("resetting FloatValue");
                    break;

                case "BoolValue":
                    BoolValue btmp = (BoolValue)scriptableObjects[i];
                    btmp.RuntimeValue = btmp.initialValue;
                    Debug.Log("resetting BoolValue");
                    break;

                case "Inventory":
                Inventory itmp = (Inventory)scriptableObjects[i];
                 itmp.coin = 0;
                //    itmp.maxMagic = 10;
                //    itmp.items.Clear();
                  itmp.nuberOfKeys = 0;
                //    Debug.Log("Inventory Reset");
                break;



                default:
                    break;
            }




            if (File.Exists(Application.persistentDataPath +
                string.Format($"/{i}.dat")))
            {
                File.Delete(Application.persistentDataPath +
                string.Format($"/{i}.dat"));
            }

            SceneManager.LoadScene("MainScene");

        }

    }

    public void QuitToDesctop()
    {
        Application.Quit();
    }
    public void Oncontinue()
    {
        for (int i = 0; i < scriptableObjects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), scriptableObjects[i]);
                file.Close();
            }

        }
        SceneManager.LoadScene("MainScene");

    }
}
