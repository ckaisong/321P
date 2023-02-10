using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// get all cards from ShoppingCart and render them as list of OrderItem objects onto ShoppingCart scene

public class ShoppingCartRenderer : MonoBehaviour
{
    [SerializeField] public GameObject orderItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ShoppingCart cart = GameObject.Find("ShoppingCart").GetComponent<ShoppingCart>();
        render(cart); 
    }

    public void render(ShoppingCart cart)
    {
        Debug.Log("Rendering order item"); 
        List<GreetingCard> cards = cart.getCards(); 
        int y = 0;
        int y_displace = -500;
        for (int i=0; i < cards.Count; i ++){
            // instantiate orderItem 
            Debug.Log("Instantiating OrderItem: " + cards[i].nameObj.text);
            Vector3 position = new Vector3(0, y + i * y_displace, 0);
            GameObject orderItemObj = Instantiate(orderItemPrefab, position, Quaternion.identity); 
            OrderItem orderItem = orderItemObj.GetComponent<OrderItem>(); 
            orderItem.transform.SetParent(transform, false); 
            Debug.Log(cards[i]);
            Debug.Log(orderItem); 
            orderItem.setGreetingCardData(cards[i]); 
        }
    }
}
