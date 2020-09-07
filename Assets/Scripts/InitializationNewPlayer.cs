using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationNewPlayer : MonoBehaviour
{
    public List<Mesh> meshes;// the fit of the Character fat || fitness || normal
    public List<Material> materials;//The Color of The Character
    public List<GameObject> Hair;
    public int[] SaveDetails;
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
            skinnedMeshRenderer.sharedMesh = meshes[SaveDetails[0] = Random.Range(0 , meshes.Capacity)];
            skinnedMeshRenderer.material = materials[SaveDetails[1] = Random.Range(0, materials.Capacity)];
            Hair[SaveDetails[2] = Random.Range(0, Hair.Capacity)].SetActive(true);

            //add here hair change
            //add here beard change for men!..!..!
            //add here tatto
            //
        }
    }
}
