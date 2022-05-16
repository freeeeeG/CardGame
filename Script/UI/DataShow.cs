using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
    public GameObject mo;
    public TextMeshPro tempoMo;
    public string[] moList = new string[11];
    public GameObject rotatingCircle;
    public GameObject player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {   
        //Touch
        playerData.SetActive(false);
        enemyData.SetActive(false);
        Debug.Log("name: " + playerData);

        // playerData = GameObject.Find("PlayerValue");
        // enemyData = GameObject.Find("EnemyValue");

        //Position
        playerPosition = GameObject.Find("Player");
        enemyPosition = GameObject.Find("Enemy");

        playerHealthBar = GameObject.Find("PlayerHealthBar");
        enemyHealthBar = GameObject.Find("EnemyHealthBar");

        //Data
        checkpoint = GameObject.Find("Checkpoint");
        mo = GameObject.Find("Mo");
        rotatingCircle = GameObject.Find("Bagua");
        
    }

    // Update is called once per frame
    void Update()
    {
        DataDisplayFollow();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter" + eventData.pointerEnter.name);
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
        Debug.Log("exit" + eventData.pointerEnter.name);
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
        checkpoint.GetComponent<TextMeshPro>().text = BattleManager.Instance.turnCount.ToString();    
        mo.GetComponent<TextMeshPro>().text = player.GetComponent<PlayerData>().mo.ToString();
    }
}