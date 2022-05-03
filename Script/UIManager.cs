using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    #region CardManager
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
    IEnumerable<Card> sideTempoList = new List<SideCard>();
    IEnumerable<Card> elementTempoList = new List<SpellCard>();

    #region 无用
    public int eachCount = 0;
    public bool isSwitch = false;
    #endregion
    public bool changePage = false;
    public Vector2 firstPagePos = new Vector2(1420, 1080);

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
        sideList = GetAllSideCard();
        combineList = GetAllCombineCard();
        elementList = GetAllElementCard();

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

        if (ele.GetComponent<Image>().color == Color.white)
        {
            for (int i = 0; i < elementCardCount; i++)
                elements[i].GetComponent<Image>().color = Color.white;
            combineFlag = false;
            combineSideCard = null;
        }
        else if (ele.GetComponent<Image>().color == Color.red)
        {
            combineFlag = true;
        }

        // Judge(tempList);

        // SideCardDisplay(tempList);
        // if (combineSideCard != null)
        // {
        //     CombineCardDispaly(CombineEvent(combineFlag));
        // }

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

    public List<SideCard> GetAllSideCard()
    {
        List<SideCard> list = new List<SideCard>();
        foreach (var item in cardList)
        {
            if (item is SideCard)
            {
                var sideCard = item as SideCard;
                list.Add(sideCard);
            }
        }
        return list;
    }
    public List<CombineCard> GetAllCombineCard()
    {
        List<CombineCard> list = new List<CombineCard>();
        foreach (var item in cardList)
        {
            if (item is CombineCard)
            {
                var combineCard = item as CombineCard;
                list.Add(combineCard);
            }
        }
        return list;
    }
    public List<SpellCard> GetAllElementCard()
    {
        List<SpellCard> list = new List<SpellCard>();
        foreach (var item in cardList)
        {
            if (item is SpellCard)
            {
                var spellCard = item as SpellCard;
                list.Add(spellCard);
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
                foreach (var ele in elementList)
                {
                    if (ele.cardName == item.attribute)
                    {
                        list.Add(ele);
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
                foreach (var side in sideList)
                {
                    if (side.cardName == item.back_name)
                    {
                        list.Add(side);
                        comList.Add(item);
                        // Debug.Log("sideCard.cardName: " + side.cardName);
                        // Debug.Log("comCard.back_name: " + item.cardName);
                    }
                }
            }
        }

        combineTempoList = comList;
        return list;
    }

    public void Judge(List<Card> tempoList)
    {
        Debug.Log("Judge:   /   " + sideTempoList + "   /   " + tempoList);
        if (sideTempoList != null || elementTempoList != null)
        {
            if (tempoList == sideTempoList || tempoList == elementTempoList)
            {
                CancelSelected(tempoList);
            }
        }
        else
        {
            if (tempoList is SideCard)
            {
                sideTempoList = tempoList;
                SideCardDisplay(tempoList);
            }
        }
    }
    public void SideCardDisplay<T>(List<T> list) { }

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
        {
            for (int i = 0; i < elementCardCount; i++)
                if (elements[i].GetComponentInChildren<TextMeshProUGUI>().text == item.cardName)
                {
                    elements[i].GetComponent<Image>().color = Color.red;
                }
        }
    }

    public List<T> CancelSelected<T>(List<T> cardList)
    {
        Debug.Log("CancelSelected: " + cardList);
        if (cardList is SpellCard)
        {
            Debug.Log("CancelSelected" + cardList);
            foreach (var item in sideCard)
            {
                item.SetActive(true);
            }
        }
        else if (cardList is SideCard)
        {
            Debug.Log("CancelSelected" + cardList);
            foreach (var item in elements)
            {
                item.GetComponent<Image>().color = Color.white;
            }
        }

        return null;
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

