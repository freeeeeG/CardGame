using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class CamareManager : Singleton<CamareManager>
{


    public GameObject camarePoint;
    public Vector3 mousePos;
    public CinemachineVirtualCamera CM;

    public float speed_k = 1;
    // Start is called before the first frame update
    void Start()
    {
        CM = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {   
        mousePos = Input.mousePosition;
    }

    public void Shake()
    {

        camarePoint.transform.DOShakePosition(0.2f, 0.5f, 14, 90, false, true);
        Debug.Log("shake");
    }


    public void PlayerFollow()
    {
        camarePoint.transform.position = Player.Instance.transform.position;
        CM.m_Lens.OrthographicSize = 3;
    }
    public void CentreScreen()
    {
        camarePoint.transform.position = new Vector3(0, 0, -1);
        CM.m_Lens.OrthographicSize = 5;
    }
}
    