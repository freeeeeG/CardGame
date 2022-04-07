using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamareManager : Singleton<CamareManager>
{


    public GameObject camarePoint;
    public Vector3 mousePos;

    public float speed_k = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        mousePos = Input.mousePosition;
        camarePoint.transform.position = new Vector3((mousePos.x-960f)/speed_k,camarePoint.transform.position.y, camarePoint.transform.position.z);
    }
}
    