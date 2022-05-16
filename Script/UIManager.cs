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
    public Card combineCardText;
    public GameObject combineCard;
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

    public int pageCount = 0;
    public bool isSwitch = false;

    #region 无用
    public List<Card> test = new List<Card>();
    public List<SpellCard> test2 = new List<SpellCard>();
    #endregion
    public Card lastClickCard;
    public bool changePage = false;
    public Vector2 firstPagePos = new Vector2(1420, 1080);
    public Action OnCardClick;
    public Func<int, int> OnCardCount;

    #endregion

    #region BattleUI
    public Slider playerSlider;
    public Slider enemySlider;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI enemyText;
    public int playerValue;
    public int enemyValue;
    public float playerMaxValue;
    public float playerMinValue;
    public float enemyMaxValue;
    public float enemyMinValue;
    // public bool Touch = false;

    #endregion
    void Start()
    {
        BagUIStart();
        BattleUIStart();
    }

    // Update is called once per frame
    void Update()
    {
        HpDataUpdate();
    }
    #region BagUI

    public void BattleUIStart()
    {
        playerMaxValue = Player.Instance.data.maxHp;
        enemyMaxValue = Enemy.Instance.data.maxHp;
        playerMinValue = playerMaxValue / 100 * (-43);
        enemyMinValue = enemyMaxValue / 100 * (-43);

        playerSlider = GameObject.Find("PlayerHP").GetComponent<Slider>();
        GameObject.Find("PlayerHP").GetComponent<Slider>().maxValue = playerMaxValue;
        GameObject.Find("PlayerHP").GetComponent<Slider>().minValue = playerMinValue;
        enemySlider = GameObject.Find("EnemyHP").GetComponent<Slider>();
        GameObject.Find("EnemyHP").GetComponent<Slider>().maxValue = enemyMaxValue;
        GameObject.Find("EnemyHP").GetComponent<Slider>().minValue = enemyMinValue;
    }

    public void HpDataUpdate()
    {
        if (Player.Instance.data.hp >= 0 && Player.Instance.data.hp <= playerMaxValue)
        {
            playerText.text = Player.Instance.data.hp.ToString() + "/" + playerMaxValue.ToString();
            playerSlider.value = Player.Instance.data.hp;
        }

        if (Enemy.Instance.data.hp >= 0 && Enemy.Instance.data.hp <= enemyMaxValue)
        {
            enemyText.text = Enemy.Instance.data.hp.ToString() + "/" + enemyMaxValue.ToString();
            enemySlider.value = Enemy.Instance.data.hp;
        }
    }

    public void BagUIStart()
    {
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
        // 实例化合成卡
    }
    public void ElementUIClickEvent(GameObject ele)
    {
        isSwitch = true;
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
                if (lastClickCard is SpellCard)
                {
                    foreach (var item in sideCard)
                    {
                        item.GetComponent<Image>().color = Color.white;
                    }
                }
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
        isSwitch = false;
        List<SpellCard> tempList = new List<SpellCard>();
        SideCard eleCard = null;
        foreach (var item in sideList)
        {
            if (item.cardName == ele.GetComponentInChildren<TextMeshProUGUI>().text)
            {
                eleCard = item;
            }
        }

        if (lastClickCard != null)
        {
            if (lastClickCard.cardName == ele.GetComponentInChildren<TextMeshProUGUI>().text)
            {
                combineFlag = false;
            }
            else
            {
                combineFlag = true;
                lastClickCard = eleCard;
                ele.GetComponent<Image>().color = Color.blue;
                foreach (var item in sideCard)
                {
                    if (item != ele)
                    {
                        item.GetComponent<Image>().color = Color.white;
                    }
                }
            }
        }
        else
        {
            combineFlag = false;
            lastClickCard = eleCard;
            ele.GetComponent<Image>().color = Color.blue;
        }

        SideCard tempCard = null;
        foreach (var item in sideList)
            if (item.cardName == ele.GetComponentInChildren<TextMeshProUGUI>().text)
                tempCard = item;

        combineSideCard = tempCard;
        tempList = Find(tempCard);
        ElementCardDisplay(tempList);
        CombineCard c = CombineEvent(combineFlag);
        CombineCardDispaly(CombineEvent(combineFlag));
        lastClickCard = tempCard;
        // Judge(tempList);
    }
    public void CombineCardUIClickEvent(GameObject ele)
    {

    }
    public CombineCard CombineEvent(bool flag)
    {
        if (combineSpellCard != null)
            foreach (var item in combineList)
            {
                // if(combineSpellCard != null)
                if (item.attribute == combineSpellCard.cardName && item.back_name == combineSideCard.cardName && flag)
                {
                    // TODO: 塞到牌库
                    Debug.Log("com: " + item.cardName + " " + item.mo + " " + item.num + " " + item.effect);
                    return item;
                }
            }
        return null;
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
        pageCount = 0;
        for (int i = 0; i < sideCardCount; i++)
            sideCard[i].SetActive(false);

        foreach (var item in list)
            for (int i = 0; i < sideCardCount; i++)
                if (sideCard[i].GetComponentInChildren<TextMeshProUGUI>().text == item.cardName)
                {
                    sideCard[i].SetActive(true);
                    pageCount++;
                }
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
    public void CombineCardDispaly(CombineCard card)
    {
        // combineCard.GetComponent<CardDisplay>().card = card;
        if (card != null)
            combineCard.GetComponent<CardDisplay>().ShowCard(card);
    }
    public void AddCard(Card card)
    {

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

