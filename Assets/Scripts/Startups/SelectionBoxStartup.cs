using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBoxStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.name = Constants.selectionBoxGameObjectName;
    }
}
