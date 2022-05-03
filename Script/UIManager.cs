using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    #region UIGameObjectManager
    [Header("Bag UI")]

    public List<GameObject> elements;
    public List<GameObject> sideCard;
    public List<Card> combineCards = null;
    public List<GameObject> combineCard = null;
    public GameObject cardPrefab;
    public GameObject sideCardGroup;
    public GameObject elementCardGroup;

    public ScrollViewTurnPages scrollViewTurnPages;

    #endregion

    #region BagUI
    public bool combineFlag = false;
    public SpellCard combineSpellCard;
    public SideCard combineSideCard;
    public List<Card> cardList = new List<Card>();
    public List<SideCard> sideList = new List<SideCard>();
    public List<CombineCard> combineList = new List<CombineCard>();
    public List<SpellCard> elementList = new List<SpellCard>();
    public int sideCardCount = 0;
    public int elementCardCount = 0;

    public List<CardDisplay> cardDisplays = new List<CardDisplay>();

    List<CombineCard> combineTempoList = new List<CombineCard>();
    List<Card> sideTempoList = new List<SideCard>().Cast<Card>().ToList();
    List<Card> elementTempoList = new List<SpellCard>().Cast<Card>().ToList();


    #region 无用
    public int eachCount = 0;
    public bool isSwitch = false;
    public List<Card> test = new List<Card>();
    public List<SpellCard> test2 = new List<SpellCard>();
    #endregion
    public Card lastClickCard;
    public bool changePage = false;
    public Vector2 firstPagePos = new Vector2(1420, 1080);
    public Action OnCardClick;
    public Func<int,int> OnCardCount;

    #endregion
    void Start()
    {

        BagUIStart();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region BagUI

    public void BagUIStart()
    {
        eachCount = 0;
        isSwitch = true;
        cardList = CardData.Instance.CardList;
        sideList = GetAllCard<SideCard>();
        combineList = GetAllCard<CombineCard>();
        elementList = GetAllCard<SpellCard>();
        //创建旁牌&赋值
        foreach (var item in sideList)
        {
            GameObject newsideC = GameObject.Instantiate(cardPrefab, sideCardGroup.transform);

            newsideC.GetComponent<Button>().onClick.AddListener(() => SideCardUIClickEvent(newsideC));
            sideCard.Add(newsideC);
            sideCard[sideCardCount].GetComponentInChildren<TextMeshProUGUI>().text = item.cardName;

            sideCardCount++;
        }
        //创建元素卡&赋值
        foreach (var item in elementList)
        {
            GameObject newElementC = GameObject.Instantiate(cardPrefab, elementCardGroup.transform);
            newElementC.GetComponent<Button>().onClick.AddListener(() => ElementUIClickEvent(newElementC));
            elements.Add(newElementC);
            elements[elementCardCount].GetComponentInChildren<TextMeshProUGUI>().text = item.cardName;
            elementCardCount++;
        }
    }
    public void ElementUIClickEvent(GameObject ele)
    {
        scrollViewTurnPages.pages = 1;
        scrollViewTurnPages.ResetPagePos(firstPagePos);
        SpellCard eleCard = null;
        foreach (var item in elementList)
        {
            if (item.cardName == ele.GetComponentInChildren<TextMeshProUGUI>().text)
            {
                eleCard = item;
            }
        }
        combineSpellCard = eleCard;
        List<SideCard> tempList = new List<SideCard>();
        tempList = Find(eleCard);

        if (lastClickCard != null && lastClickCard.cardName == eleCard.cardName)
        {
            combineFlag = false;
            combineSideCard = null;
            for (int i = 0; i < elementCardCount; i++)
                elements[i].GetComponent<Image>().color = Color.white;
            SideCardDisplay(GetAllCard<SideCard>());
            lastClickCard = null;
        }
        else
        {
            if (ele.GetComponent<Image>().color == Color.white)
            {
                for (int i = 0; i < elementCardCount; i++)
                    elements[i].GetComponent<Image>().color = Color.white;
                ele.GetComponent<Image>().color = Color.green;
                combineFlag = false;
                combineSideCard = null;
            }
            else if (ele.GetComponent<Image>().color == Color.red)
            {
                combineFlag = true;
            }
            SideCardDisplay(tempList);
            if (combineSideCard != null)
            {
                CombineCardDispaly(CombineEvent(combineFlag));
            }
            lastClickCard = eleCard;
        }
    }
    public void SideCardUIClickEvent(GameObject ele)
    {
        List<SpellCard> tempList = new List<SpellCard>();
        SideCard tempCard = null;
        foreach (var item in sideList)
            if (item.cardName == ele.GetComponentInChildren<TextMeshProUGUI>().text)
                tempCard = item;

        combineSideCard = tempCard;
        tempList = Find(tempCard);
        ElementCardDisplay(tempList);
        CombineCardDispaly(CombineEvent(combineFlag));
        lastClickCard = tempCard;
        // Judge(tempList);
    }
    public void CombineCardUIClickEvent(GameObject ele)
    {

    }
    public CombineCard CombineEvent(bool flag)
    {

        foreach (var item in combineList)
        {
            if (item.attribute == combineSpellCard.cardName && item.back_name == combineSideCard.cardName && flag)
            {
                return item;
            }
        }
        return null;
    }

    public void CombineCardDispaly(CombineCard card)
    {

    }
    public List<T> GetAllCard<T>() where T : Card
    {
        List<T> list = new List<T>();
        foreach (var item in cardList)
        {
            if (item is T)
            {
                list.Add(item as T);
            }
        }
        return list;
    }
    public List<T> Find<T, U>(U card) where T : Card where U : Card
    {
        List<T> list = new List<T>();
        List<CombineCard> comList = new List<CombineCard>();
        foreach (var item in combineList)
        {
            if (item is T && item.cardName == card.cardName)
            {
                list.Add(item as T);
            }
        }
        return list;
    }
    public List<SpellCard> Find(SideCard card)
    {
        //TODO: 查找合成卡片
        List<SpellCard> list = new List<SpellCard>();
        List<CombineCard> comList = new List<CombineCard>();
        foreach (var item in combineList)
        {
            if (item.back_name == card.cardName) //find element by sideCard
            {
                foreach (var card1 in elementList)
                {
                    if (card1.cardName == item.attribute)
                    {
                        list.Add(card1);
                        comList.Add(item);
                    }
                }
            }
        }
        combineTempoList = comList;
        return list;
    }
    public List<SideCard> Find(SpellCard card)
    {
        List<SideCard> list = new List<SideCard>();
        List<CombineCard> comList = new List<CombineCard>();
        foreach (var item in combineList)
        {
            if (item.attribute == card.cardName)
            {
                foreach (var card1 in sideList)
                {
                    if (card1.cardName == item.back_name)
                    {
                        list.Add(card1);
                        comList.Add(item);
                    }
                }
            }
        }
        combineTempoList = comList;
        return list;
    }
    public void SideCardDisplay(List<SideCard> list)
    {
        for (int i = 0; i < sideCardCount; i++)
            sideCard[i].SetActive(false);
        foreach (var item in list)
            for (int i = 0; i < sideCardCount; i++)
                if (sideCard[i].GetComponentInChildren<TextMeshProUGUI>().text == item.cardName)
                    sideCard[i].SetActive(true);
    }
    public void ElementCardDisplay(List<SpellCard> list)
    {
        for (int i = 0; i < elementCardCount; i++)
            elements[i].GetComponent<Image>().color = Color.white;
        foreach (var item in list)
            for (int i = 0; i < elementCardCount; i++)
                if (elements[i].GetComponentInChildren<TextMeshProUGUI>().text == item.cardName)
                    elements[i].GetComponent<Image>().color = Color.red;

    }


    #endregion
    #region BattleUI
    public void PlayerHealthBar(int hp)
    {
        // playerHealthBar.GetComponent<Image>().fillAmount = hp / 100f;
    }
    public void EnemyHealthBar(int hp)
    {
        // playerHealthBar.GetComponent<Image>().fillAmount = hp / 100f;
    }

    public void PlayerAttack(int attack)
    {
        // playerAttack.GetComponent<TextMeshProUGUI>().text = attack.ToString();
    }
    public void EnemyAttack(int attack)
    {
        // playerAttack.GetComponent<TextMeshProUGUI>().text = attack.ToString();
    }
    #endregion

}

