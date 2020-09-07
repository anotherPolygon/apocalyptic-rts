using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationNewPlayer : MonoBehaviour
{
    public List<Mesh> meshes;// the fit of the Character fat || fitness || normal
    public List<Material> materials;//The Color of The Character
    public List<GameObject> Hair;
    public List<GameObject> Beard;
    public int[] SaveDetails;
    [SerializeField] public bool Man;//for man value true for woman value false
    SkinnedMeshRenderer skinnedMeshRenderer;
    void Start()
    {
        SaveDetails = new int[5];//for save data of the player
       // GameObject PlayerBody = gameObject.transform.FindChild("Body").gameObject;
        skinnedMeshRenderer = transform.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
  
        //Hair.Add(transform.Find("Hair1").gameObject);
        /*//
        for(int i =1;i<=Head.transform.childCount;i++)
        {
                Hair.Add(Head.transform.FindChild("Hair"+i).gameObject);
                Debug.Log("the i is " + i);
        }
                Debug.Log("Capacity is" + Hair.Capacity);
        */

            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < Hair.Capacity; i++)
                Hair[i].SetActive(false);
            if(Man)
                for (int i = 0; i < Beard.Capacity; i++)
                    Beard[i].SetActive(false);
            skinnedMeshRenderer.sharedMesh = meshes[SaveDetails[0] = Random.Range(0 , meshes.Capacity)];
            skinnedMeshRenderer.material = materials[SaveDetails[1] = Random.Range(0, materials.Capacity)];
            Hair[SaveDetails[2] = Random.Range(0, Hair.Capacity)].SetActive(true);
            if(Man)
               if (Random.Range(0 , 2) == 1f)//the chance if the player will be have a beard
                    Beard[SaveDetails[3] = Random.Range(0, Beard.Capacity)].SetActive(true);
               else
                    SaveDetails[3] = -1;// dont have a beard
            //add here hair change
            //add here beard change for men!..!..!
            //add here tatto
            //
        }
    }
}
