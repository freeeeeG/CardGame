using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBlock : MonoBehaviour, IPointerClickHandler
{
    public GameObject summonBlock;
    public GameObject attackBlock;// not require
    public bool hasMonster;
    public GameObject Card;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
    public void SetAttack()
    {
        attackBlock.SetActive(true);
    }

    public void UseSkill()
    {
        if(Card.GetComponent<CardDisplay>().card is Card)
        {

        }
    }
    public void CloseAll()
    {
        summonBlock.SetActive(false);
        attackBlock.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        
    }
}
