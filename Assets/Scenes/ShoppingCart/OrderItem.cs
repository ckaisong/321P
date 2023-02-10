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


/* Contains GreetingCard */

public class OrderItem : MonoBehaviour
{   
    public GreetingCard gcObj; 
    [SerializeField] public GameObject removeButton; 
    [SerializeField] public TextMeshProUGUI name; 
    [SerializeField] public TextMeshProUGUI price; 
    [SerializeField] public TextMeshProUGUI description; 
    [SerializeField] public GameObject image; 

    public string id; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setId(string _id){
        id = _id; 
    }

    public void setGreetingCardData(GreetingCard card){
        name.text = card.nameObj.text; 
        price.text = card.priceObj.text; 
        image.GetComponent<Image>().sprite = card.imageObj.GetComponent<Image>().sprite; 
        description.text = card.descriptionObj.text;
    }

    public void onRemoveButtonClick(){
        Debug.Log("Remove bbutton clicked");
        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        cart.remove(this.gcObj); 
        cart.getCards();
    }
}
