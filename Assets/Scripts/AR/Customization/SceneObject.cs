using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField]
    SaveSceneSystem.ObjectType m_type;

    //SaveSceneSystem _ssSystem;
    public void OnPlace()
    {
        GameObject.Find("SaveSystem").GetComponent<SaveSceneSystem>().AddToTracking(m_type, this.gameObject);
    }

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
