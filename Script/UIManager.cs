using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    #region CardManager
    public List<Card> cardList = new List<Card>();
    public List<Card> sideList = new List<Card>();
    public List<Card> combineList = new List<Card>();
    public List<Card> elementList = new List<Card>();
    #endregion

    #region BagUI
    [Header("Bag UI")]
    public List<GameObject> elements;
    public List<GameObject> sideCard;
    public List<Card> combineCards = null;
    public int sideCardCount = 0;
    public int elementCardCount = 0;
    public GameObject cardPrefab;
    public GameObject sideCardGroup;
    public GameObject elementCardGroup;

    public List<CardDisplay> cardDisplays = new List<CardDisplay>();

    string tempo_r;
    string tempo_l;
    public int eachCount = 0;
    public bool isSwitch = false;
    public bool changePage = false;
    public ScrollViewTurnPages scrollViewTurnPages;
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
        // reset the page number to 1 when click the element card
        scrollViewTurnPages.pages = 1;
        scrollViewTurnPages.ResetPagePos(firstPagePos);

        Card eleCard = null;

        // find the element card in the cardList
        foreach (var item in elementList)
        {
            if (item.cardName == ele.GetComponentInChildren<TextMeshProUGUI>().text)
            {
                eleCard = item;
                break;
            }
            else
            {
                throw new Exception("没有找到元素牌");
            }
        }

        List<Card> tempList = new List<Card>();
        // find all the side card that has the same element
        tempList = Find(eleCard);
        sideCardCount = tempList.Count;
        for (int i = 0; i < sideCardCount; i++)
        {
            CombineCard card = tempList[i] as CombineCard;
            sideCard[i].GetComponentInChildren<TextMeshProUGUI>().text = card.back_name;
        }

        // reset the color of the whole element card
        if (ele.GetComponent<Image>().color == Color.white)
            for (int i = 0; i < elementCardCount; i++)
                elements[i].GetComponent<Image>().color = Color.white;
        // set the color of the element card
        else if ((ele.GetComponent<Image>().color == Color.red))
        {
            //TODO: scene1(flag = 0): show combine card when click the element card
            //TODO: scene2(flag = 0): cancel the selection of element card then show all side card 
            CombineEvent(tempList, 0);
        }
    }
    public void SideCardUIClickEvent(GameObject ele)
    {
        List<Card> tempList = new List<Card>();
        Card tempCard = null;
        string selectElement = ele.GetComponentInChildren<TextMeshProUGUI>().text;

        // find the side card in the cardList
        foreach (var item in sideList)
        {
            if (item.cardName == selectElement)
            {
                tempCard = item;
                break;
            }
            else
            {
                throw new Exception("没有找到旁牌");
            }
        }
        // find all the element card with the same side name
        tempList = Find(tempCard);
        foreach (var item in tempList)
        {
            CombineCard card = item as CombineCard;
            foreach (var element in elements)
            {
                if (selectElement == card.attribute)
                {
                    // highlight the element card
                    element.GetComponent<Image>().color = Color.red;
                }
            }
        }
        // TODO: scene1(flag = 1): show combine card when click the side card
        // TODO: scene2(flag = 1): mark down the side card for the scene1 of "ElementUIClickEvent"
        // TODO: scene3(flag = 1): if the element card is not selected, show all side card
        CombineEvent(tempList, 1);

        // Array.Clear(txtLeft, 0, txtLeft.Length);
        // combineCard.SetActive(true);
        // int index = -1;
        // string right = ele.GetComponentInChildren<TextMeshProUGUI>().text;
        // if (right != tempo_r)
        // {
        //     tempo_r = right;
        //     // for (int k = 0; k < elements.Length; k++)
        //     // {
        //     //     elements[k].GetComponent<Image>().color = Color.white;
        //     // }
        // }

        // int i = 0, j = 0;

        // //记录点击旁卡对应的元素&组合卡
        // foreach (var item in cardList)
        // {
        //     if (item is CombineCard)
        //     {
        //         var comCard = item as CombineCard;
        //         if (comCard.back_name == right) //识别旁卡的名称
        //         {
        //             txtLeft[i] = comCard.attribute; //记录元素
        //             combine[i] = txtRight[i].combine;   //记录卡名
        //             i++;
        //         }
        //     }
        // }
        //点击设置元素卡的颜色
        // foreach (var item in txtLeft)
        // {
        //     index = Array.IndexOf(eleList, item);
        //     if (index != -1)
        //     {
        //         elements[index].GetComponent<Image>().color = Color.red;
        //     }
        // }
        // foreach (var item in cardList)
        // {
        //     if (item is CombineCard)
        //     {
        //         var comCard = item as CombineCard;
        //         if (comCard.back_name == right)
        //         {
        //             txtLeft[i] = comCard.attribute;
        //             combine[i] = comCard.cardName;

        //             i++;
        //         }
        //     }
        // }
        // //设置合成卡的名称
        // foreach (var item in txtRight)
        // {
        //     if (right == item.side)
        //     {
        //         combineCard.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = item.combine;
        //     }
        // }

    }

    public void CombineCardUIClickEvent(GameObject ele)
    {

    }
    public void CombineEvent(List<Card> list, int flag)
    {
        List<Card> tempList = new List<Card>();
        Card tempCard = null;

        // show combine card 0, 1
        // show all side card 0, 1
        // cancel the selection of element card 0
        // mark down the selected side card 1
        // mark down the selected element card 0

        if (flag == 0) {
            
        } else {

        }

        // for (int x = 0; x < elements.Length; x++)
        // {
        //     foreach (var items in cardList)
        //     {
        //         if (items is CombineCard)
        //         {
        //             var eleCard = items as CombineCard;
        //             if (left == eleCard.attribute && tempo_r == eleCard.back_name)
        //             {
        //                 combineCard.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = eleCard.cardName;
        //             }
        //         }
        //     }
        // }
    }

    public List<Card> GetAllSideCard()
    {
        List<Card> list = new List<Card>();
        foreach (var item in cardList)
        {
            if (item is SideCard)
            {
                list.Add(item);
            }
        }
        return list;
    }
    public List<Card> GetAllCombineCard()
    {
        List<Card> list = new List<Card>();
        foreach (var item in cardList)
        {
            if (item is CombineCard)
            {
                list.Add(item);
            }
        }
        return list;
    }
    public List<Card> GetAllElementCard()
    {
        List<Card> list = new List<Card>();
        foreach (var item in cardList)
        {
            if (item is SpellCard)
            {
                list.Add(item);
                Debug.Log(item.cardName);
            }
        }
        return list;
    }
    public List<Card> Find(Card card)
    {
        //TODO: 查找合成卡片
        List<Card> list = new List<Card>();

        if (card is SpellCard)
        {
            foreach (var item in combineList)
            {
                var comCard = item as CombineCard;
                if (comCard.attribute == card.cardName) //find element by sideCard
                {
                    list.Add(comCard);
                }
            }
            return list;
        }
        if (card is SideCard)
        {
            foreach (var item in cardList)
            {
                if (item is CombineCard)
                {
                    var comCard = item as CombineCard;
                    if (comCard.back_name == card.cardName) //find sideCard by element
                    {
                        list.Add(comCard);
                    }
                }
            }
            return list;
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

