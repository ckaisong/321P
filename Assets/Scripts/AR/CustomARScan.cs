using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class CustomARScan : MonoBehaviour
{
    [Header("Pre scanning UI elements")]
    [SerializeField]
    [Tooltip("UI panel for requesting customization id")]
    GameObject m_PreScanPanel;
    [SerializeField]
    [Tooltip("The inputfield where user specify the customization ID")]
    TMP_InputField m_CustomizationIDInput;
    [SerializeField]
    [Tooltip("The display text to show loading progress")]
    TextMeshProUGUI m_ResponseText;
    [SerializeField]
    [Tooltip("Results panel to display the retrieved data")]
    GameObject m_ResultPanel;

    [Header("UI when scanning AR ")]
    [SerializeField]
    [Tooltip("UI panel for during scanning of AR")]
    GameObject m_ScanningPanel;
    [SerializeField]
    [Tooltip("Image target reference when scanning")]
    RawImage m_duringScanImgPrompt;

    private ImageTargetBehaviour m_imgTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSearchCustomization()
    {
        m_ResponseText.gameObject.SetActive(true);
        m_ResponseText.text = "Searching for customization...";
        StartCoroutine(GetCustomizations(m_CustomizationIDInput.textComponent.text));

    }
    IEnumerator GetCustomizations(string _custShareID)
    {
        WWWForm getCustForm = new WWWForm();
        getCustForm.AddField("customizationsShareLink", _custShareID);
        
        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/getCustomizationSharebyShareLink", getCustForm);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            m_ResponseText.text = "Customization found!";
            Debug.Log($"cust:{www.downloadHandler.text}");
            //converts response json to ShareCodeCustResponse data format
            var resCust = JsonUtility.FromJson<ShareCodeCustResponseList>("{\"sccrList\":"+www.downloadHandler.text+"}");
            if (resCust.sccrList.Count > 0)
            {
                //Converts base64 image
                byte[] imgTarget = Convert.FromBase64String(resCust.sccrList[0].image.Split(",")[1]);
                //Create texture2D to load image
                Texture2D tmpTexture = new Texture2D(2, 2);
                //Load img byte into texture2D
                tmpTexture.LoadImage(imgTarget);
                //enables results panel
                m_ResultPanel.SetActive(true);
                //get ResultEntry script
                ResultEntry reDisp = m_ResultPanel.GetComponent<ResultEntry>();
                reDisp.SetNameOfCust(resCust.sccrList[0].name);
                //sets display image of image target
                reDisp.SetImage(tmpTexture);
                //adds listener onto proceed button
                reDisp.GetProceedButton().onClick.AddListener(() =>
                {
                    //disable pre-scanning ui
                    m_PreScanPanel.SetActive(false);
                    //Create image target
                    m_imgTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(tmpTexture, 0.148f, "CustomTarget");
                    //Load customisation onto image target
                    SaveSceneSystem.LoadCustJsonToTransform(resCust.sccrList[0].options, m_imgTarget.transform);
                    //sets during scanning ui's image reference
                    m_duringScanImgPrompt.texture = tmpTexture;
                    //enable during-scanning's ui
                    m_ScanningPanel.SetActive(true);
                });
            }

        }
        else
        {
            m_ResponseText.text = "Couldn't find customization";
        }
    }
}
