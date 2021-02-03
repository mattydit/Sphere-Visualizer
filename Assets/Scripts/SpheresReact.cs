using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresReact : MonoBehaviour
{
    public List<GameObject> sphereList;


    // Start is called before the first frame update
    void Start()
    {
        sphereList = gameObject.GetComponent<InstantiateSpheres>().sphereList;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in sphereList)
        {
            
            //go.transform.position = new Vector3(Mathf.Lerp())
        }
    }
}
