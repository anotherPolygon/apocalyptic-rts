using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public DebugConsole DebugConsole;

    // Start is called before the first frame update

    public dynamic GetyGameObjectIdentity(GameObject gameObject)
    {
        Entity entityIdentityScript = gameObject.GetComponent<Entity>();
        Unit unitIdentityScript = gameObject.GetComponent<Unit>();
        Building buildingIdentityScript = gameObject.GetComponent<Building>();


        if (entityIdentityScript != null)
            return entityIdentityScript;

        else if (unitIdentityScript != null)
            return unitIdentityScript;

        else if (buildingIdentityScript != null)
            return buildingIdentityScript;

        else
            return null;
    }

    
}
