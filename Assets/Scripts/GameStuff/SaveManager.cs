using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager gameSave;
	public List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();

	private void OnEnable()
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
	}

	//private void OnDisable()
	//{
	//    SaveScriptableObject();
	//}
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
	public void LoadScriptableObjects()
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
	}
}
