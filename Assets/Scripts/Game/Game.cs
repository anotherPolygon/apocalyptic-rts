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
    public QuaziTimer QuaziTimer;
    public Timer Timer;
    public NavMeshArrivalReporter ArrivalReporter;
    public ResourceManager RM;
    public UIManager uIManager;

    //public Player Player;

    Dictionary<int, Entity> id2entity = new Dictionary<int, Entity>();
    Dictionary<Entity, int> entity2id = new Dictionary<Entity, int>(); 
    //public List<Bounds> BuildingBounds = new List<Bounds>();
    public Dictionary<string, Bounds> BuildingBoundsDict = new Dictionary<String, Bounds>();


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
        // Have to happen before the Start() of other scripts that need these components
        DebugConsole = GetComponent<DebugConsole>();
        Mouse = GetComponent<Mouse>();
        CameraController = GetComponent<CameraController>();
        State = GetComponent<State>();
        QuaziTimer = GetComponent<QuaziTimer>();
        Timer = GetComponent<Timer>();
        ArrivalReporter = GetComponent<NavMeshArrivalReporter>();
        RM = GetComponent<ResourceManager>();
        uIManager = GetComponent<UIManager>();
    }

    public void Start()
    {
       
        
    }
    public void Update()
    {
        // pass
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
