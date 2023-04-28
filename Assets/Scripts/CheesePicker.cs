using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheesePicker : MonoBehaviour
{
	public float cheeseCollected = 0;
	public float offeringCost;
	public TextMeshProUGUI collectedCheeseText;
	
	private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Cheese"))
        {
            cheeseCollected ++;
            collectedCheeseText.text = cheeseCollected.ToString();
            Destroy(other.gameObject);

        }
    }


    public void OfferCheese(){
    	if(cheeseCollected >= offeringCost){
    		cheeseCollected -= offeringCost;
    		collectedCheeseText.text = cheeseCollected.ToString();
    	}
    }
}