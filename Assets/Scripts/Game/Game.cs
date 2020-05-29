using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // to define game as a singleton (so that class has reference to the only instance)
    public static Game Manager { get; private set; }

    public DebugConsole DebugConsole;
    //public Player Player;
    public CameraController CameraController;
    public Mouse Mouse;

    Dictionary<int, Entity> id2entity = new Dictionary<int, Entity>();
    Dictionary<Entity, int> entity2id = new Dictionary<Entity, int>();

    int last_id = 0;

    private void Awake()
    {
        // if no Game object exists yet
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Manager != this)
        { 
            Destroy(gameObject); // to make sure there is only one Game
        }
    }

    public void Start()
    {
        DebugConsole = GetComponent<DebugConsole>();
    }

    private int GenerateId()
    {
        last_id += 1;
        return last_id;
    }

    public int RegisterEntity(Entity entityToRegister)
    {
        int id = GenerateId();
        id2entity.Add(id, entityToRegister);
        entity2id.Add(entityToRegister, id);
        return id;
    }

    public bool IsPlayer(int id)
    {
        return id2entity[id].isPlayer;
    }

}
