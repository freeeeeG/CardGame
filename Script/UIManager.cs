using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

struct tRight
{
    public string element;
    public string side;
    public string combine;

}

public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    public static UIManager instance;
    public List<Card> cardList = new List<Card>();
    #region BagUI
    [Header("Bag UI")]
    public GameObject[] elements;
    private GameObject[] sideCard = new GameObject[100];
    public GameObject combineCard;
    string[] eleList = { "金", "木", "水", "火", "土" };
    public GameObject sideCPrefab;
    public GameObject sideCGroup;
    #endregion

    // string[] txtRight = new string[100];
    string[] txtLeft = new string[10];
    string[] combine = new string[100];

    bool[] select = new bool[2] { false, false };
    tRight[] txtRight = new tRight[100];
    string tempo_r;
    string tempo_l;
    public int eachCount = 0;
    public bool isSwitch = false;

    void Start()
    {
        int j = 0;
        cardList = CardData.Instance.CardList;
        combineCard.SetActive(false);
        foreach (var items in cardList)
        {
            if (items is CombineCard)
            {
                var combCard = items as CombineCard;
                // Debug.Log(combCard.back_name);
                txtRight[j].side = combCard.back_name;  //获取旁卡的名称
                txtRight[j].element = combCard.cardName;
                j++;
            }
        }

        //创建旁牌并赋值
        for (int i = 0; i < txtRight.Length; i++)
        {
            GameObject newsideC = GameObject.Instantiate(sideCPrefab, sideCGroup.transform);
            newsideC.GetComponent<Button>().onClick.AddListener(() => SideCardUIClickEvent(newsideC));
            sideCard[i] = newsideC;
            sideCard[i].GetComponentInChildren<TextMeshProUGUI>().text = txtRight[i].side;
            // Debug.Log(txtRight[i].side);
        }
        //元素卡赋值
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].GetComponentInChildren<TextMeshProUGUI>().text = eleList[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ElementUIClickEvent(GameObject ele)
    {
        combineCard.SetActive(true);
        select[0] = true;
        select[1] = false;
        Array.Clear(txtLeft, 0, txtLeft.Length);
        Array.Clear(txtRight, 0, txtRight.Length);
        Array.Clear(combine, 0, combine.Length);

        string left = ele.GetComponentInChildren<TextMeshProUGUI>().text;
        int i = 0, j = 0;
        eachCount = 0;
        isSwitch = true;
        cardList = CardData.Instance.CardList;

        //重置元素牌的颜色
        if (ele.GetComponent<Image>().color == Color.white)
            for (int x = 0; x < elements.Length; x++)
                elements[x].GetComponent<Image>().color = Color.white;

        if (ele.GetComponent<Image>().color == Color.red)
            for (int x = 0; x < elements.Length; x++)
            {
                foreach (var items in cardList)
                {
                    if (items is CombineCard)
                    {
                        var eleCard = items as CombineCard;
                        if (left == eleCard.attribute && tempo_r == eleCard.back_name)
                        {
                            combineCard.GetComponentInChildren<TextMeshProUGUI>().text = eleCard.cardName;
                        }
                    }
                }
            }

        //记录点击元素对应的旁卡&组合卡
        foreach (var item in cardList)
        {
            if (item is CombineCard)
            {
                var comCard = item as CombineCard;
                if (comCard.attribute == left)
                {
                    txtRight[i].side = comCard.back_name;
                    txtRight[i].combine = comCard.cardName;
                    i++;
                }

            }
        }
        //点击设置旁卡的名称
        foreach (var item in txtRight)
        {
            if (j >= sideCard.Length - 1)
                j %= sideCard.Length;

            sideCard[j].GetComponentInChildren<TextMeshProUGUI>().text = item.side;
            j++;
            if (item.side != null)
            {
                eachCount++;
                Debug.Log(eachCount);
            }
        }

        select[0] = false;
    }


    public void SideCardUIClickEvent(GameObject ele)
    {
        Array.Clear(txtLeft, 0, txtLeft.Length);
        combineCard.SetActive(true);
        select[0] = false;
        select[1] = true;
        int index = -1;
        string right = ele.GetComponentInChildren<TextMeshProUGUI>().text;
        if (right != tempo_r)
        {
            tempo_r = right;
            for (int k = 0; k < elements.Length; k++)
            {
                elements[k].GetComponent<Image>().color = Color.white;
            }
        }

        int i = 0, j = 0;

        //记录点击旁卡对应的元素&组合卡
        foreach (var item in cardList)
        {
            if (item is CombineCard)
            {
                var comCard = item as CombineCard;
                if (comCard.back_name == right) //识别旁卡的名称
                {
                    txtLeft[i] = comCard.attribute; //记录元素
                    combine[i] = txtRight[i].combine;   //记录卡名
                    i++;
                    Debug.Log(comCard.attribute);
                }
            }
        }
        //点击设置元素卡的颜色
        foreach (var item in txtLeft)
        {
            index = Array.IndexOf(eleList, item);
            if (index != -1)
            {
                elements[index].GetComponent<Image>().color = Color.red;
            }
        }
        foreach (var item in cardList)
        {
            if (item is CombineCard)
            {
                var comCard = item as CombineCard;
                if (comCard.back_name == right)
                {
                    txtLeft[i] = comCard.attribute;
                    combine[i] = comCard.cardName;
                    i++;
                }
            }
        }
        //设置合成卡的名称
        foreach (var item in txtRight)
        {
            if (right == item.side)
            {
                combineCard.GetComponentInChildren<TextMeshProUGUI>().text = item.combine;
            }
        }
        select[1] = false;
    }

    public void CombineCardUIClickEvent(GameObject ele)
    {

    }

}

