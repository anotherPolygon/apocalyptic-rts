using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "junk" + gameObject.GetInstanceID().ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}