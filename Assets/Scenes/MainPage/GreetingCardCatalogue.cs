using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.SceneManagement;
using System;


public class GreetingCardCatalogue : MonoBehaviour
{
    [SerializeField] public GameObject greetingCardPrefab;
    public static GreetingCardCatalogue Instance;
    
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    // Start is called before the first frame update
    void Start()
    {
        User user = GameObject.Find("UserManager").GetComponent<User>();
        string session_id = "guest";
        if (user != null) {
            session_id = user.session_id.ToString();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        Debug.Log(session_id);
        StartCoroutine(RetrieveProduct(session_id));            
    }
    
    // function to ensure catalogue only visible in MainPage scene
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
         Scene m_Scene = SceneManager.GetActiveScene();
         string sceneName = m_Scene.name;
         Debug.Log("Current scene:"+ sceneName);

         if (sceneName == "MainPage"){
            gameObject.SetActive(true);
         }else{
            gameObject.SetActive(false);
         }
    }

    // Update is called once per frame
     private IEnumerator RetrieveProduct(string session_id)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://lunar-byte-371808.et.r.appspot.com/api/getGreetingCards");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Debug.Log("GetProduct Response: " + www.downloadHandler.text);
            var getGreetingCardResponseList = JsonUtility.FromJson<GreetingCardResponseList>(www.downloadHandler.text);   
            var greetingCardsResponse = getGreetingCardResponseList.greeting_cards; 


            // int cols = 2; 
            // int col_index = 0; 
            // int row_index = 0; 
            // int y_displace = 865;
            // int x_displace = 500; 
            // int y = 165; 
            // int x = -250; 
            // int z = 0;

            for (int i=0; i < greetingCardsResponse.Count; i ++){
                
                Vector3 test_position = new Vector3(0, 0, 0);
                GameObject greetingCard = Instantiate(greetingCardPrefab, test_position, Quaternion.identity);
                greetingCard.GetComponent<GreetingCard>().setName(greetingCardsResponse[i].name);
                greetingCard.GetComponent<GreetingCard>().setPrice(greetingCardsResponse[i].price);
                greetingCard.GetComponent<GreetingCard>().setDescription(greetingCardsResponse[i].description);
                // set gc image
                string rawImageBytes = greetingCardsResponse[i].image.Split(',')[1];
                greetingCard.GetComponent<GreetingCard>().setImage(rawImageBytes);
                
                greetingCard.transform.SetParent(transform, false); 
            }
        }
    }

}
