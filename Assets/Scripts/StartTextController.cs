using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartTextController : MonoBehaviour {
    private TextMeshProUGUI textMP;
    public List<Color> textColors = new List<Color>();
    private int randomColor;
    private float colorChangeFrequency = 0.7f;

    void Start() {
        textMP = GetComponent<TextMeshProUGUI>();
        StartCoroutine(AnimateText());
    }


    IEnumerator AnimateText() {
        while (true) {
             randomColor = Random.Range(0, textColors.Capacity);
             textMP.color = textColors[randomColor];
             yield return new WaitForSeconds(colorChangeFrequency);
        }
       
    }
}
