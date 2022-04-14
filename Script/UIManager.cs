using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

struct tRight
{
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
    public GameObject[] sideCard = new GameObject[100];
    public GameObject combineCard;
    public string[] eleList = { "金", "木", "水", "火", "土" };
    public GameObject sideCPrefab;
    public GameObject sideCGroup;
    #endregion

    // string[] txtRight = new string[100];
    string[] txtLeft = new string[100];
    string[] combine = new string[100];

    bool[] select = new bool[2] { true, false };
    tRight[] txtRight = new tRight[100];


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
                txtRight[j].side = combCard.back_name;
                j++;
            }
        }

        for (int i = 0; i < txtRight.Length; i++)
        {
            GameObject newsideC = GameObject.Instantiate(sideCPrefab, sideCGroup.transform);
            newsideC.GetComponent<Button>().onClick.AddListener(() => SideCardUIClickEvent(newsideC));
            sideCard[i] = newsideC;
            sideCard[i].GetComponentInChildren<TextMeshProUGUI>().text = txtRight[i].side;

        }

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
        select[0] = !select[0];
        Array.Clear(txtLeft, 0, txtLeft.Length);
        Array.Clear(txtRight, 0, txtRight.Length);
        Array.Clear(combine, 0, combine.Length);

        string left = ele.GetComponentInChildren<TextMeshProUGUI>().text;
        int i = 0, j = 0;
        for (int x = 0; x < elements.Length; x++)
        {
            elements[x].GetComponent<Image>().color = Color.white;
        }
        cardList = CardData.Instance.CardList;
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

        foreach (var item in txtRight)
        {
            if (j >= sideCard.Length - 1)
                j %= sideCard.Length;

            sideCard[j].GetComponentInChildren<TextMeshProUGUI>().text = item.side;

            // if (item.side != null)
            j++;
        }
        // Debug.Log(j + " " + z);

        // for (z = j; z < txtRight.Length; z++)
        // {
        //     Debug.Log(j + " " + z);
        //     GameObject getPrefabs = GameObject.Find('');
        // }

    }


    public void SideCardUIClickEvent(GameObject ele)
    {
        combineCard.SetActive(true);
        select[1] = !select[0];
        // Array.Clear(txtLeft, 0, txtLeft.Length);
        // Array.Clear(txtRight, 0, txtRight.Length);
        // Array.Clear(combine, 0, combine.Length);
        string right = ele.GetComponentInChildren<TextMeshProUGUI>().text;

        int i = 0, j = 0;
        cardList = CardData.Instance.CardList;
        foreach (var item in cardList)
        {
            if (item is CombineCard)
            {
                var comCard = item as CombineCard;
                if (comCard.back_name == right)
                {
                    txtLeft[i] = comCard.attribute;
                    combine[i] = txtRight[i].combine;
                    i++;
                }
            }
        }

        foreach (var item in txtLeft)
        {
            if (j >= elements.Length - 1)
                j %= elements.Length;

            if (item == elements[j].GetComponentInChildren<TextMeshProUGUI>().text)
            {
                elements[j].GetComponent<Image>().color = Color.red;
                j++;
            }
            else
            {
                elements[j].GetComponent<Image>().color = Color.white;
                // j++;
            }
        }

        foreach (var item in txtRight)
        {
            if (right == item.side)
            {
                combineCard.GetComponentInChildren<TextMeshProUGUI>().text = item.combine;
            }
        }
    }

    public void CombineCardUIClickEvent(GameObject ele)
    {

    }

}

