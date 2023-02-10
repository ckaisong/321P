using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using System.Text;
using UnityEngine.SceneManagement;




public class Login : MonoBehaviour
{
    private const string PASSWORD_REGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,24})";

    [SerializeField] private string loginEndpoint = "https://lunar-byte-371808.et.r.appspot.com/api/userlogin/";
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button loginButton;
    
    public void OnLoginClick()
    {
        alertText.text = "";
        TMP_InputField inpuptID = GameObject.Find("UserName").GetComponent(typeof(TMP_InputField)) as TMP_InputField;
		TMP_InputField inputPswd = GameObject.Find("Password").GetComponent(typeof(TMP_InputField)) as TMP_InputField;
        string username = inpuptID.text;
		string password = inputPswd.text;
        if (username == "" && password == "")
        {
            alertText.text = "Please enter your username or password";
        }
        else
        {
        // User contains username and password
        User user = GameObject.Find("UserManager").GetComponent<User>();
        user.setUser(username, password);
        ActivateButtons(false);
        StartCoroutine(TryLogin(user));
        }
    }

    // public void OnCreateClick()
    // {
    //     alertText.text = "Creating account...";
    //     ActivateButtons(false);

    //     StartCoroutine(TryCreate());
    // }

    private IEnumerator TryLogin(User user)
    {
        string userData = JsonUtility.ToJson(user);
        UnityWebRequest www = UnityWebRequest.Post(loginEndpoint, "login", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(userData);
		www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
		www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        // DownloadHandler objects are helper objects. 
        // When attached to a UnityWebRequest, they define how to handle HTTP response body data received from a remote server. 
        // Generally, they are used to buffer, stream and/or process response bodies
	    www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        //return request

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.result);
            Debug.Log(www.downloadHandler.text);
            var jsonLoggedIn = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);   
            Debug.Log("POST done!");
            Debug.Log(jsonLoggedIn.session_id);
            user.setSessionId(jsonLoggedIn.session_id);
            Debug.Log(user.session_id);
            if (jsonLoggedIn.code == 1)
            {
                
                SceneManager.LoadScene("MainPage");
                loginButton.interactable = false;
            }
            else
            {
                alertText.text = "Your username or password is wrong";
                loginButton.interactable = true;
            }
        }
        else
        {
            alertText.text = "Error Connecting to the server.";
            loginButton.interactable = true;
        
			// StringBuilder sb = new StringBuilder();
            // foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            // {
            //     sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
            // }
			// if(www.downloadHandler.text == "Loggedin")
			// {
			// 	User user = JsonUtility.FromJson<User>(userData);
			// 	Debug.Log(user.id);
			// }
			// else
			// {
			// 	Debug.Log(www.downloadHandler.text); //convert the result to text
			// }

        }
        yield return null;
    }

    private void onGuestLoginClick(){
        User user = GameObject.Find("UserManager").GetComponent<User>();
        user.setUser("Guest", "");
        user.setSessionId("guest_session");
    }

    private void ActivateButtons(bool toggle)
    {
        loginButton.interactable = toggle;
        //createButton.interactable = toggle;
    }

   
}
