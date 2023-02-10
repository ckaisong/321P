using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singleton/GameObjectFactory")]
public class GameObjectFactory : MonoBehaviourSingleton<GameObjectFactory>
{
    [SerializeField]
    [Tooltip("List of all path (relative to Resources folder) of where prefabs are stored")]
    List<string> directory = new List<string>() { "Prefabs/Customization" };
    Dictionary<string, GameObject> goPrefabs;
    

    public GameObject GetPrefab(string _nameOfPrefab)
    {
        if (goPrefabs == null)
        {
            goPrefabs = new Dictionary<string, GameObject>();
            foreach (string path in directory)
            {
                GameObject[] arrGo = Resources.LoadAll<GameObject>(path);
                foreach (GameObject go in arrGo)
                {
                    try
                    {
                        goPrefabs[go.name] = go;
                    }
                    catch
                    {

                    }                    //Debug.Log(go.name);
                }
            }
        }
        try
        {
            return goPrefabs[_nameOfPrefab];
        }
        catch(Exception)
        {
            return null;
        }
    }
}
