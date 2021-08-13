using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    public List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();
    public InventoryManager isOpen;
    public bool IsPlaying;

    public GameObject PauseCanvas;
    public GameObject AudioListener;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen.inventOpen)
        {
            if (Input.GetButtonDown("pause"))
            {
                changePause();
            }
        }
      
    }
    public void changePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void OnOffMusic()
    {
        IsPlaying = !IsPlaying;
        if (IsPlaying)
        {
            AudioListener.SetActive(false);
        }
        else
        {
            AudioListener.SetActive(true);

        }
    }
    public void quitRoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void SaveScriptableObject()
    {


        for (int i = 0; i < scriptableObjects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(scriptableObjects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }
}
