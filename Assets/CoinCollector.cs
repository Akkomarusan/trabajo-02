using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{

    public int coins;

    [SerializeField] Text coinText;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Coin")
        {
            coins = coins + 1;
           // Col.gameObject.SetActive(false);
            Destroy(Col.gameObject);

            coinText.text = "Coins: " + coins;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
