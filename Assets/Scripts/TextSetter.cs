using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    
    private List<string> loadScreenTexts = new List<string>(){"Stabbing your sword in the ground ruins the damn sword.", "Standing too close to the fire is not recommended",
        "This loading screen is completely useless and for educational purposes only", "This game has four loading messages but only two load screens"};

    private string didYouKnow = "Did You know: ";

    public TMP_Text loadText;
    
    
    void Start() {
        loadText.text = didYouKnow + loadScreenTexts[Random.Range(0, loadScreenTexts.Count)];
        
    }

   
   
}
