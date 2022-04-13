using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    }

    public void Shake()
    {
        Camera.main.transform.DOShakePosition(0.1f, 0.1f, 20, 90, false, true);
        Debug.Log("shake");
    }
}
    