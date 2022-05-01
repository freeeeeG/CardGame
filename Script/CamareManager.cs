
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
    public float _speed_cf = 10;
    public float speed_cf = 10;
    public float dv = 1;
    // Start is called before the first frame update
    void Start()
    {
        CM = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        speed_cf = _speed_cf;
    }

    // Update is called once per frame
    void Update()
    {

        mousePos = Input.mousePosition;
        if (followPlayerFlag)
        {
            CentreFollow();
        }
        else
        {
            CentreScreen();
        }
        speed_cf -= dv * Time.deltaTime;
    }

    public void Shake()
    {
        camarePoint.transform.DOShakePosition(0.2f, 0.5f, 14, 90, false, true);
        Debug.Log("shake");
    }


    public void CentreFollow()
    {
        camarePoint.transform.position = new Vector3((Enemy.Instance.transform.position.x + Player.Instance.transform.position.x) / 2, camarePoint.transform.position.y, camarePoint.transform.position.z);
        if (CM.m_Lens.OrthographicSize >= 3)
            CM.m_Lens.OrthographicSize -= 2*speed_cf* Time.deltaTime;



    }
    public void CentreScreen()
    {
        camarePoint.transform.position = new Vector3(0, 0, -1);
        if (CM.m_Lens.OrthographicSize <= 5)
            CM.m_Lens.OrthographicSize += speed_cf * Time.deltaTime;

    }
}
