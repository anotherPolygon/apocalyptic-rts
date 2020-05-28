using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{
    private GameObject HealthBar;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        HealthBar = transform.Find("HealthBar").gameObject;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    public void Select()
    {
        HealthBar.SetActive(true);
    }

    public void Diselect()
    {
        HealthBar.SetActive(false);
    }
}
