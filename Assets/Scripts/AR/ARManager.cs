using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

public class ARManager : MonoBehaviour
{
    #region OldARManager
    /*[SerializeField]
    GameObject[] m_TargetsInScene;

    List<ImageTargetBehaviour> m_ITBinScene;*/
    /*[SerializeField]
    GameObject[] m_PlaceableObjects;*/

    /*[SerializeField]
    HTLogger logger;*/
    /*public void ResetTracking()
    {
        foreach (ImageTargetBehaviour itb in m_ITBinScene)
        {
            if (itb.TargetStatus.Status == Status.TRACKED)
            {

            }
        }
    }*/
    /* public void SaveSceneBin()
     {
         *//*List<SaveLoadSystem.PosRotScale> PRSWine = new List<SaveLoadSystem.PosRotScale>();
         List<SaveLoadSystem.PosRotScale> PRSFlower = new List<SaveLoadSystem.PosRotScale>();*//*
         List<float[]> PRSWine = new List<float[]>();
         List<float[]> PRSFlower = new List<float[]>();
         //logger.AddMsg($"Saving to {Application.persistentDataPath}...");
         Debug.Log(Application.persistentDataPath);
         for (int i=0; i < transform.childCount; i++)
         {
             Transform child = transform.GetChild(i);
             string childName = child.name;
             childName = childName.Substring(0, childName.IndexOf(" "));
             logger.AddMsg(childName);
             if (childName == "Flower")
             {
                 *//*PRSFlower.Add(new SaveLoadSystem.PosRotScale(
                     child.position.x, child.position.y, child.position.z,
                     child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                     child.lossyScale.x, child.lossyScale.y, child.lossyScale.z));*//*
                 PRSFlower.Add(new float[]{
                     child.position.x, child.position.y, child.position.z,
                     child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                     child.lossyScale.x, child.lossyScale.y, child.lossyScale.z});
             }
             else if(childName == "Wine")
             {
                 *//*PRSWine.Add(new SaveLoadSystem.PosRotScale(
                     child.position.x, child.position.y, child.position.z,
                     child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                     child.lossyScale.x, child.lossyScale.y, child.lossyScale.z));*//*
                 PRSWine.Add(new float[]{
                     child.position.x, child.position.y, child.position.z,
                     child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, child.rotation.eulerAngles.z,
                     child.lossyScale.x, child.lossyScale.y, child.lossyScale.z });
             }
         }
         logger.AddMsg($"Wine count: {PRSWine.Count} | Flower count: {PRSFlower.Count}");
         SaveLoadSystem.SaveData data = new SaveLoadSystem.SaveData();
         data.m_CountOfEachObject = new int[]{ PRSWine.Count, PRSFlower.Count};
         //data.m_ListOfPosRotScale = new List<SaveLoadSystem.PosRotScale>();
         data.m_ListOfPosRotScale = new List<float[]>();
         foreach(float[] prs in PRSWine)
         {
             data.m_ListOfPosRotScale.Add(prs);
         }
         foreach (float[] prs in PRSFlower)
         {
             data.m_ListOfPosRotScale.Add(prs);
         }
         SaveLoadSystem.SaveBin("Scene1", data);
     }
     public void LoadSceneBin()
     {
         logger.AddMsg("Loading file >>>");
         SaveLoadSystem.SaveData data = SaveLoadSystem.LoadBin("Scene1");
         if (data.m_CountOfEachObject == null)
         {
             logger.AddMsg("File not loaded");
             return;
         }
         logger.AddMsg("File loaded");
         foreach (int count in data.m_CountOfEachObject)
         {
             logger.AddMsg($"Count read: {count}");
         }
         foreach (float[] prs in data.m_ListOfPosRotScale)
         {
             logger.AddMsg($"PRS read: {prs}");
         }
     }

     public void SaveScene()
     {
         //temporary lists to hold respective save datas
         List<SaveLoadSystem.PosScaRot> WinePSR = new();
         List<SaveLoadSystem.PosScaRot> FlowerPSR = new();
         //loop through each child
         for (int i = 0; i < transform.childCount; ++i)
         {
             Transform child = transform.GetChild(i);
             //extract child gameobject's name to check which 3d prefab model is it
             string childName = child.name;
             *//***************************************************************
              * May change in the future when more 3d object names are done *
              ***************************************************************//*
             childName = childName.Substring(0, childName.IndexOf(" "));
             //Add the transform information according to the asset name
             if (childName == "Flower")
             {
                 FlowerPSR.Add(new SaveLoadSystem.PosScaRot(
                                             child.localPosition,
                                             child.localScale,
                                             child.rotation));
             }
             else if (childName == "Wine")
             {
                 WinePSR.Add(new SaveLoadSystem.PosScaRot(
                                             child.localPosition,
                                             child.localScale,
                                             child.rotation));
             }
         }
         *//*SaveLoadSystem.GOData flowerDat = new SaveLoadSystem.GOData("Flower", FlowerPSR);
         SaveLoadSystem.GOData wineDat = new SaveLoadSystem.GOData("Wine", WinePSR);
         Debug.Log($"Flower {JsonUtility.ToJson(flowerDat.m_PSR)}");
         Debug.Log($"Wine {JsonUtility.ToJson(wineDat.m_PSR)}");
         string dataJSon = JsonUtility.ToJson(flowerDat, true) 
                         + JsonUtility.ToJson(wineDat, true);*//*

         //Convert the above datas to json
         string dataJSon = JsonUtility.ToJson(new SaveLoadSystem.GOData(_psrWine: WinePSR, _psrFlower: FlowerPSR));
         //Debug.Log(dataJSon);
         //save data to a file named "GoData" (file extension is added in the function)
         SaveLoadSystem.SaveJSon(_fileName: "GoData", dataJSon);
         //Log success message 
         logger.AddMsg("File Saved");
     }
     public void LoadScene()
     {
         //retrieve the json, in string form, read from the file "GoData"
         string rawJson = SaveLoadSystem.LoadJSon("GoData");
         //If string returned is "Fail" means file is not loaded
         if (rawJson == "Fail")
         {
             logger.AddMsg("Load Failed");
             return;
         }
         //Converts raw Json string to GOData struct
         SaveLoadSystem.GOData data = JsonUtility.FromJson<SaveLoadSystem.GOData>(rawJson);
         //Debug.Log($"File loaded: {data.m_PSRFlower.Count} flowers, {data.m_PSRWine.Count} wine");
         GameObject go;
         //Iterate through each set of position scale rotation
         foreach (SaveLoadSystem.PosScaRot psr in data.m_PSRFlower)
         {
             go = Instantiate(m_PlaceableObjects[0], transform);
             go.transform.localPosition = new Vector3(psr._position[0], psr._position[1], psr._position[2]);
             go.transform.localScale = new Vector3(psr._scale[0], psr._scale[1], psr._scale[2]);
             go.transform.rotation = new Quaternion(psr._rotation[0], psr._rotation[1], psr._rotation[2], psr._rotation[3]);
         }
         foreach (SaveLoadSystem.PosScaRot psr in data.m_PSRWine)
         {
             go = Instantiate(m_PlaceableObjects[1], transform);
             go.transform.localPosition = new Vector3(psr._position[0], psr._position[1], psr._position[2]);
             go.transform.localScale = new Vector3(psr._scale[0], psr._scale[1], psr._scale[2]);
             go.transform.rotation = new Quaternion(psr._rotation[0], psr._rotation[1], psr._rotation[2], psr._rotation[3]);
         }
         logger.AddMsg("File loaded");
     }*/
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(GameObject target in m_TargetsInScene)
        {
            m_ITBinScene.Add(target.GetComponent<ImageTargetBehaviour>());
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
