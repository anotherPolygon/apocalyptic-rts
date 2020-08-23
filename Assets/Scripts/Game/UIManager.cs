using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI junkCount;

    public Dictionary<string, TextMeshProUGUI> resource2Text = new Dictionary<string, TextMeshProUGUI>();



    // Start is called before the first frame update
    void Start()
    {
        junkCount = GameObject.Find("JunkCount").GetComponent<TextMeshProUGUI>();

        resource2Text.Add(Constants.junkResourceName, junkCount);
    }
    // Update is called once per frame
    void Update()
    {
        
    } 

    public void updateResourceText(string respurceKey, int newValue)
    {
        resource2Text[respurceKey].text = newValue.ToString();
    }
}
