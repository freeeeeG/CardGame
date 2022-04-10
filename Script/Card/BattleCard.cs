using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CardState //位置和所属状态
{
    inPlayerHand
}
public class BattleCard : MonoBehaviour, IPointerDownHandler
{
    public BattleManager BattleManager;

    public CardState cardState = CardState.inPlayerHand;

    public bool hasAttacked;


    // Start is called before the first frame update
    void Start()
    {
        BattleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("click card");
        if (cardState == CardState.inPlayerHand && BattleManager.Instance.currentPhase == GamePhase.playerAction)
        {

            //可以攻击敌方的卡牌
            if (transform.GetComponent<CardDisplay>().card is Card)
            {
                BattleManager.Instance.AttackRequest(transform.position, 0, transform.gameObject);
            }
            //可以对自己使用的卡牌
            else if (transform.GetComponent<CardDisplay>().card is Card)
            {
                BattleManager.Instance.UseRequest(transform.position, 1, transform.gameObject);
            }   
        }
        Destroy(gameObject);

    }
}
