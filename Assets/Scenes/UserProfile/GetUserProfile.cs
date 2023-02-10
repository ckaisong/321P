using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.SceneManagement;

public class GetUserProfile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI user_id;
    [SerializeField] private TextMeshProUGUI first_name;
    [SerializeField] private TextMeshProUGUI last_name;
    [SerializeField] private TextMeshProUGUI phone_number;
    [SerializeField] private TextMeshProUGUI profile_picture;
    [SerializeField] private TextMeshProUGUI homeaddress;
    [SerializeField] private TextMeshProUGUI date_of_birth;
    [SerializeField] private TextMeshProUGUI gender;

    // Start is called before the first frame update
    void Start()
    {   
        
        User user = GameObject.Find("UserManager").GetComponent<User>();
        string session_id = user.session_id.ToString();
        //User obj = GameObject.Find("UserManager").GetComponent<User>();
        // string serializedSessionId = JsonConvert.SerializeObject(Obj);
        Debug.Log(session_id);
        StartCoroutine(RetrieveUserProfile(session_id));

    }
    

     private IEnumerator RetrieveUserProfile(string session_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("session_id", session_id);

        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/fetchUserProfilebyId", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("GetUserProfile UserResponse: " + www.downloadHandler.text);
            // gender.text = www.downloadHandler.text;
            // Debug.Log(www.downloadHandler.text.id);
            var userProfileResponses = JsonUtility.FromJson<UserProfileResponseList>("{\"users\":" + www.downloadHandler.text + "}");   
            
            Debug.Log(userProfileResponses.users);
            if (userProfileResponses.users.Count > 0){
                Debug.Log("userProfileResponse.list[0].gender: " + userProfileResponses.users[0].gender);

                user_id.text = userProfileResponses.users[0].user_id;
                first_name.text = userProfileResponses.users[0].first_Name;
                last_name.text = userProfileResponses.users[0].last_name;
                phone_number.text = userProfileResponses.users[0].phone_number;
                homeaddress.text = userProfileResponses.users[0].homeaddress;
                date_of_birth.text = userProfileResponses.users[0].date_of_birth;
                gender.text = userProfileResponses.users[0].gender;

            }

        }


}
}

