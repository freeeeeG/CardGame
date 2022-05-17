using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DataShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Battle Data Touch
    [Header("Touch Event")]
    public GameObject playerData;
    public GameObject enemyData;
    #endregion

    #region Battle Data Position Change
    [Header("Position Change")]
    public GameObject playerPosition;
    public GameObject enemyPosition;
    public GameObject playerHealthBar;
    public GameObject enemyHealthBar;
    public Vector3 playerHealthPosition;
    public Vector3 enemyHealthPosition;
    #endregion

    #region Battle Data Show
    [Header("Show Data")]
    public GameObject checkpoint;
    public int tempoCP;
    public GameObject mo;
    public int tempoMo;
    public TextMeshProUGUI tempoText;
    public string tempoNum;
    public bool moChange = false;
    public string[] chinese = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
    public GameObject rotatingCircle;
    public GameObject player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Touch
        playerData.SetActive(false);
        enemyData.SetActive(false);
        // playerData = GameObject.Find("PlayerValue");
        // enemyData = GameObject.Find("EnemyValue");

        //Position
        playerPosition = GameObject.Find("Player");
        enemyPosition = GameObject.Find("Enemy");

        playerHealthBar = GameObject.Find("PlayerHealthBar");
        enemyHealthBar = GameObject.Find("EnemyHealthBar");

        //Data
        player = GameObject.Find("Player");
        checkpoint = GameObject.Find("Checkpoint");
        mo = GameObject.Find("Mo");
        rotatingCircle = GameObject.Find("Bagua");

    }

    // Update is called once per frame
    void Update()
    {
        DataDisplayFollow();
        DataDisplay();
        RotatingCircle();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("enter" + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == "P_outside")
        {
            playerData.SetActive(true);
        }
        else if (eventData.pointerEnter.name == "E_outside")
        {
            enemyData.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("exit" + eventData.pointerEnter.name);
        if (eventData.pointerEnter.name == "P_outside")
        {
            playerData.SetActive(false);
        }
        else if (eventData.pointerEnter.name == "E_outside")
        {
            enemyData.SetActive(false);
        }
    }
    public void DataDisplayFollow()
    {
        playerHealthPosition = Camera.main.WorldToScreenPoint(new Vector3(playerPosition.transform.position.x + 0.65f, playerPosition.transform.position.y + 2.92f, 0));
        enemyHealthPosition = Camera.main.WorldToScreenPoint(new Vector3(enemyPosition.transform.position.x + 0.65f, enemyPosition.transform.position.y + 2.92f, 0));

        playerHealthBar.transform.position = playerHealthPosition;
        enemyHealthBar.transform.position = enemyHealthPosition;
    }
    public void DataDisplay()
    {
        tempoCP = BattleManager.Instance.turnCount;
        ChineseDisplay(checkpoint, tempoCP);

        if (tempoMo != player.GetComponent<PlayerData>().mo)
        {
            tempoMo = player.GetComponent<PlayerData>().mo;
            moChange = true;
            ChineseDisplay(mo, tempoMo);
        }
        else
        {
            moChange = false;
        }
    }
    public void ChineseDisplay(GameObject gb, int Num)
    {
        if (Num > 9)
        {
            gb.GetComponentInChildren<TextMeshProUGUI>().text = chinese[Num % 10] + chinese[Num / 10];
            if (Num > 9 && Num < 20)
            {
                if (Num == 10)
                { 
                    gb.GetComponentInChildren<TextMeshProUGUI>().text = "十";
                }
                else
                {
                    gb.GetComponentInChildren<TextMeshProUGUI>().text = "十" + chinese[Num % 10];
                }
            } else {
                if (Num % 10 == 0) {
                    gb.GetComponentInChildren<TextMeshProUGUI>().text = chinese[Num / 10] + "十";
                } else {
                    gb.GetComponentInChildren<TextMeshProUGUI>().text = chinese[Num / 10] + "十" + chinese[Num % 10];
                }
            }
        }
        else
        {
            gb.GetComponentInChildren<TextMeshProUGUI>().text = chinese[Num];
            // tempoText = gb.GetComponentInChildren<TextMeshProUGUI>();
            // tempoNum = chinese[Num];
            // tempoText.DOText(tempoNum, 2.0f, true, ScrambleMode.Numerals, null);
        }
    }
    public void RotatingCircle()
    {
        if (moChange)
            rotatingCircle.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360);
    }
}