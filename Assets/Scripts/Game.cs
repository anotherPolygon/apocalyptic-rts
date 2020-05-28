using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // to define game as a singleton (so that class has reference to the only instance)
    public static Game Instance { get; private set; }

    public DebugConsole DebugConsole;
    Dictionary<int, GameObject> id2object = new Dictionary<int, GameObject>();
    Dictionary<GameObject, int> object2id = new Dictionary<GameObject, int>();
    Dictionary<int, Entity> id2entity = new Dictionary<int, Entity>();
    Dictionary<Entity, int> entity2id = new Dictionary<Entity, int>();

    int last_id = 0;

    private void Awake()
    {
        // if no Game object exists yet
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        { 
            Destroy(gameObject); // to make sure there is only one Game
        }
    }

    private int GenerateId()
    {
        last_id += 1;
        return last_id;
    }

    public void RegisterObject(GameObject gameObjectToRegister, Entity entityToRegister)
    {
        int id = GenerateId();
        id2object.Add(id, gameObjectToRegister);
        object2id.Add(gameObjectToRegister, id);
        id2entity.Add(id, entityToRegister);
        entity2id.Add(entityToRegister, id);
    }

    public bool IsPlayer(int id)
    {
        return id2entity[id].isPlayer;
    }

}
