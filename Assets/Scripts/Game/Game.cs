using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    // to define game as a singleton (so that class has reference to the only instance)
    public static Game Manager { get; private set; }
    
    public DebugConsole DebugConsole;
    public CameraController CameraController;
    public Mouse Mouse;
    public State State;
    //public Player Player;

    Dictionary<int, Entity> id2entity = new Dictionary<int, Entity>();
    Dictionary<Entity, int> entity2id = new Dictionary<Entity, int>();

    public enum SettlerRoles
    {
        Soldier,
        Worker,
    };
    

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
        Mouse = GetComponent<Mouse>();
        CameraController = GetComponent<CameraController>();
        State = GetComponent<State>();
    }

    public int RegisterEntity(Entity entityToRegister)
    {
        int _id = entityToRegister.unityObjects.gameObject.GetInstanceID();
        id2entity.Add(_id, entityToRegister);
        entity2id.Add(entityToRegister, _id);
        return _id;
    }

    public bool IsPlayer(int id)
    {
        return id2entity[id].isPlayer;
    }

    public Entity GameObject2Entity(GameObject gameObjectGiven)
    {
        Entity _foundEntity = null;
        id2entity.TryGetValue(gameObjectGiven.GetInstanceID(), out _foundEntity);

        return _foundEntity;
    }

}
