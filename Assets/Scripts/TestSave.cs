using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestSave : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _go;

    [SerializeField]
    Texture2D _texImg;

    //private string user_ID;
    SaveSceneSystem ssSystem;
    // Start is called before the first frame update
    void Start()
    {
        ssSystem = GameObject.Find("SaveSystem").GetComponent<SaveSceneSystem>();
        foreach (GameObject go in _go)
        {
            go.GetComponent<SceneObject>().OnPlace();
        }
        /*User curruser = GameObject.Find("UserManager").GetComponent<User>();
        if (curruser != null)
        {
            StartCoroutine(GetUserID(curruser.session_id));
        }*/
    }

    public void SaveBtn()
    {
        ssSystem.SaveScene("Tooty", _texImg, "Mid-Autumn customisation");
    }
    public void LoadBtn()
    {
        ssSystem.LoadScene("MidAutumn23");
    }
    public void DeleteBtn()
    {
        //ssSystem.DeleteScene();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    /*private IEnumerator GetUserID(string _sess)
    {
        WWWForm form = new WWWForm();
        form.AddField("session_id", _sess);

        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/fetchUserProfilebyId", form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            var userProfileResponses = JsonUtility.FromJson<UserProfileResponseList>("{\"users\":" + www.downloadHandler.text + "}");

            if (userProfileResponses.users.Count > 0)
            {
                user_ID = userProfileResponses.users[0].user_id;
            }
        }

    }*/
}
