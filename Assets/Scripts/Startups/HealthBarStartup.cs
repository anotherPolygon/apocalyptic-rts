using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.name = "David" + gameObject.GetInstanceID().ToString();// Constants.healthBarGameObjectName;
    }
}
