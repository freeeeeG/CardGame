using UnityEngine;

public class SceneManagers : Singleton<SceneManagers>
{


    public GameObject Scene_0;
    public GameObject Scene_1;
    public GameObject Scene_2;
    public GameObject Scene_3;
    public GameObject Scene_4;
    

    public Vector3 originalPos_0;

    public Vector3 originalPos_1;

    public Vector3 originalPos_2;

    public Vector3 originalPos_3;

    public Vector3 originalPos_4;

    public float speed_0 = 1;
    public float speed_1 = 1;
    public float speed_2 = 1;
    public float speed_3 = 1;
    public float speed_4 = 1;
    public Vector3 mousePos;
    public bool followMouseFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        Scene_0 = GameObject.Find("Scene_0");
        Scene_1 = GameObject.Find("Scene_1");
        Scene_2 = GameObject.Find("Scene_2");
        Scene_3 = GameObject.Find("Scene_3");
        Scene_4 = GameObject.Find("Scene_4");

        originalPos_0 = Scene_0.transform.position;
        originalPos_1 = Scene_1.transform.position;
        originalPos_2 = Scene_2.transform.position;
        originalPos_3 = Scene_3.transform.position;
        originalPos_4 = Scene_4.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        if(followMouseFlag)
        FollowMouse();

    }

    public void FollowMouse()
    {
        Scene_0.transform.position = new Vector3((-mousePos.x+960f)*speed_0/1000,(-mousePos.y+540f)*speed_0/1000, 0) + originalPos_0;
        Scene_1.transform.position = new Vector3((-mousePos.x+960f)*speed_1/1000,(-mousePos.y+540f)*speed_1/1000, 0) + originalPos_1;
        Scene_2.transform.position = new Vector3((-mousePos.x+960f)*speed_2/1000,(-mousePos.y+540f)*speed_2/1000, 0) + originalPos_2;
        Scene_3.transform.position = new Vector3((-mousePos.x+960f)*speed_3/1000,(-mousePos.y+540f)*speed_3/1000, 0) + originalPos_3;
        Scene_4.transform.position = new Vector3((-mousePos.x+960f)*speed_4/1000,(-mousePos.y+540f)*speed_4/1000, 0) + originalPos_4;
    }
}
