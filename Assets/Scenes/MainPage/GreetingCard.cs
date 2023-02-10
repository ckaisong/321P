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

/* Contains some data from GreetingCardResponse and add button*/

public class GreetingCard : MonoBehaviour
{   
    [SerializeField] public TextMeshProUGUI nameObj; 
    [SerializeField] public TextMeshProUGUI priceObj; 
    [SerializeField] public TextMeshProUGUI descriptionObj; 
    [SerializeField] public GameObject imageObj; 
    [SerializeField] public GameObject addButton; 
    public static GreetingCard Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPrice(string price) {
        priceObj.text = "$" + price; 
    }

    public void setName(string name) {
        nameObj.text = name.ToUpper(); 
    }

    public void setDescription(string description) {
        descriptionObj.text = description; 
    }

    public void setImage(string rawImageBytes){
        byte[] imageBytes = Convert.FromBase64String(rawImageBytes);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);     
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);           
        imageObj.GetComponent<Image>().sprite = sprite; 
    }

    public void onAddButtonClick(){
        Debug.Log("Add button clicked");
        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        cart.add(this); 
    }
}
