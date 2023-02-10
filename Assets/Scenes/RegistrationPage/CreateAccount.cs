using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;


public class CreateAccount : MonoBehaviour
{
    private const string PASSWORD_REGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,24})";

    [SerializeField] private string registerEndpoint = "https://lunar-byte-371808.et.r.appspot.com/api/insertuser/";

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button createButton;
    [SerializeField] private TMP_InputField EmailInputField;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    
    public void OnCreateClick()
    {
        
        ActivateButtons(false);

        StartCoroutine(TryCreate());
    }

   private IEnumerator TryCreate()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string email = EmailInputField.text;

        if (username.Length < 3 || username.Length > 24)
        {
            alertText.text = "Invalid username";
            ActivateButtons(true);
            yield break;
        }
        
        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);
        form.AddField("rEmail", email);


        UnityWebRequest www = UnityWebRequest.Post(registerEndpoint, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            
        }
    } 

    private void ActivateButtons(bool toggle)
    {
        
        createButton.interactable = toggle;
    }

   public void BackToMainMenu(int sceneID)

    {
        SceneManager.LoadScene(sceneID);
    }

}
