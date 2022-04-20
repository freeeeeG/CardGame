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
    public bool followPlayerFlag = false;
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
        if (followPlayerFlag)
            CentreFollow();
        else
            CentreScreen();

    }

    public void Shake()
    {

        camarePoint.transform.DOShakePosition(0.2f, 0.5f, 14, 90, false, true);
        Debug.Log("shake");
    }


    public void CentreFollow()
    {
        camarePoint.transform.position = new Vector3((Enemy.Instance.transform.position.x + Player.Instance.transform.position.x) / 2, camarePoint.transform.position.y, camarePoint.transform.position.z);
        CM.m_Lens.OrthographicSize = 3;
    }
    public void CentreScreen()
    {
        camarePoint.transform.position = new Vector3(0, 0, -1);
        CM.m_Lens.OrthographicSize = 5;
    }
}
