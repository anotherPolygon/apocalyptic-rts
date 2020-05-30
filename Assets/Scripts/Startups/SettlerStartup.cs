using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "settler" + gameObject.GetInstanceID().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
