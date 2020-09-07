using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace common
{
    namespace objects
    {
        // class for mouse button information
        public class MouseButton
        {
            readonly int id;
            public bool isClicked;
            public bool isHeld;
            public bool wasHeld;
            public float clickDuration;
            public bool hasClickJustStarted;
            public bool hasClickJustEnded;
            public float dragDistance;
            public Vector3 lastClickPosition = new Vector3(0f, 0f, 0f);

            public MouseButton(int buttonId)
            {
                id = buttonId;
                isClicked = false;
                isHeld = false;
                wasHeld = false;
                clickDuration = 0f;
                hasClickJustEnded = false;
                hasClickJustStarted = false;
                dragDistance = 0f;
            }

            public void Update(bool isClicked)
            {
                this.hasClickJustStarted = !this.isClicked & isClicked;
                this.hasClickJustEnded = this.isClicked & !isClicked;
                this.wasHeld = !isClicked & this.isHeld;

                if (this.isClicked)
                    this.clickDuration += Time.deltaTime;
                else
                    this.clickDuration = 0;

                if (this.hasClickJustStarted)
                    this.lastClickPosition = Input.mousePosition;

                this.isClicked = isClicked;

                this.isHeld = this.clickDuration > Constants.mouseLongestClick;
                this.isHeld |= CheckIfDraging();

            }

            private bool CheckIfDraging()
            {
                bool _hasMouseMoved;

                // TODO: this should be changed because all members of MouseButton should
                //       be set in Update function.
                dragDistance = CalculateDragDistance(this.lastClickPosition);
                _hasMouseMoved = dragDistance > Constants.mouseDragThreshold;

                return _hasMouseMoved & this.isClicked;
            }

            private float CalculateDragDistance(Vector3 from)
            {
                return Vector2.Distance(from, Input.mousePosition);
            }

            public override string ToString()
            {
                string result = "";
                if (isClicked)
                    result += "isClicked";
                if (isHeld)
                {
                    result += " - isHeld - ";
                    result += this.lastClickPosition.ToString();
                }

                return result;
            }
        }

        // class of unity objects for Entity to hold
        public class UnityObjects
        {
            public readonly GameObject gameObject;

            public readonly Transform transform;
            public readonly Renderer renderer;
            public readonly Image image;
            public readonly Canvas canvas;
            public readonly RectTransform rectTransform;
            public readonly NavMeshAgent navMeshAgent;
            public readonly Animator animator;
            public readonly AudioSource audioSource;
            public readonly Collider collider;

            public readonly Dictionary<string, UnityObjects> childs = new Dictionary<string, UnityObjects>();

            public UnityObjects(GameObject givenGameObject)
            {
                gameObject = givenGameObject;
                transform = givenGameObject.transform;
                renderer = givenGameObject.GetComponent<Renderer>();
                image = givenGameObject.GetComponent<Image>();
                canvas = givenGameObject.GetComponent<Canvas>();
                rectTransform = givenGameObject.GetComponent<RectTransform>();
                navMeshAgent = givenGameObject.GetComponent<NavMeshAgent>();
                animator = givenGameObject.GetComponent<Animator>();
                audioSource = givenGameObject.GetComponent<AudioSource>();
                collider = givenGameObject.GetComponent<Collider>();

                InitializeChildUnityObjects();
            }

            private void InitializeChildUnityObjects()
            {
                UnityObjects _childUnityObjects;
                foreach (Transform child in transform)
                {
                    if (child.name == gameObject.name)
                        continue;

                    _childUnityObjects = new UnityObjects(child.gameObject);
                    childs.Add(child.name, _childUnityObjects);
                }
            }
        }
    }
    public class Utils
    {
        public static bool IsTerrain(GameObject gameObjectGiven)
        {
            return gameObjectGiven.tag == Constants.terrainGameObjectTag;
        }

        public static string GetDictionaryKeysInfo<T>(Dictionary<string, T> dictionary)
        {
            string _result = "";
            foreach (KeyValuePair<string, T> entry in dictionary)
            {
                _result += entry.Key.ToString() + "|";// + ": " + entry.Value.ToString() + "||";
            }

            return _result;
        }

        public static string GetUnityObjectChildsInfo(common.objects.UnityObjects unityObject)
        {
            string _result = "unityObject " + unityObject.gameObject.name + " childs: ";
            _result += GetDictionaryKeysInfo<common.objects.UnityObjects>(unityObject.childs);
            return _result;
        }

        // A method to find the center between several game objects
        public static Vector3 FindCenterPoint(GameObject[] gos)
        {
            if (gos.Length == 0)
                return Vector3.zero;

            if (gos.Length == 1)
                return gos[0].transform.position;

            Bounds bounds = new Bounds(gos[0].transform.position, Vector3.zero);
            for (int i = 1; i < gos.Length; i++)
                bounds.Encapsulate(gos[i].transform.position);
            return bounds.center;
        }


        // Dosent work: --POSSIBLE FIX: TRY VCONERT TYPE TO --> ConvertAll
        public static Entity FindClosestEntityType(GameObject lookingGameObject, List<Entity> typesList)
        {
            float minDist = Mathf.Infinity;

            // change avboe lkist to List<Type>
            //List<Type> lp = lpf.ConvertAll(new Converter<Entity, Type>(TypeToEntity));
            // TypeToEntity is a converter functions.. which will use Type t as Entity like this:
            // SomeClass obj2 = t as SomeClass; --> dont know how to use it

            Entity closestEntityOfType = null; // pay attention the might return null!
            Vector3 currentPosition = lookingGameObject.transform.position;
            foreach (Entity entityOfType in typesList)
            {
                float dist = Vector3.Distance(entityOfType.gameObject.transform.position, currentPosition);
                if (dist < minDist)
                {
                    closestEntityOfType = entityOfType;
                    minDist = dist;
                }
            }
            return closestEntityOfType;
        }

        public static void wnaderAround(Animated animated)
        {
            Vector3 refferncePoint = animated.gameObject.transform.position;

            Vector3 newDestination = new Vector3(refferncePoint.x + UnityEngine.Random.Range(-animated.ProgressDistance, animated.ProgressDistance),
                    refferncePoint.y + UnityEngine.Random.Range(-animated.ProgressDistance, animated.ProgressDistance),
                    refferncePoint.z + UnityEngine.Random.Range(-animated.ProgressDistance, animated.ProgressDistance));

            animated.unityObjects.navMeshAgent.SetDestination(newDestination);
        }
    }
}