using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
    public GameObject[] sideCard;
    public GameObject combineCard;
    public string[] eleList = { "金", "木", "水", "火", "土" };
    #endregion

    // string[] txtRight = new string[100];
    string[] txtLeft = new string[10];
    string[] combine = new string[10];

    bool[] select = new bool[2] { true, false };
    tRight[] txtRight = new tRight[100];
    void Start()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].GetComponentInChildren<TextMeshProUGUI>().text = eleList[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (select[0] == select[1])
        {
            Debug.Log("select same");
        }
    }



    public void ElementUIClickEvent(GameObject ele)
    {
        select[0] = !select[0];
        string left = ele.GetComponentInChildren<TextMeshProUGUI>().text;
        int i = 0, j = 0;
        Debug.Log(left);
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
                // Debug.Log(item.cardName);
                if (comCard.attribute == left)
                {
                    txtRight[i].side = comCard.back_name;
                    txtRight[i].combine = comCard.cardName;
                    // Debug.Log("put in bag: " + combineCard.back_name);
                    i++;
                }
            }
        }

        foreach (var item in txtRight)
        {
            if (j >= sideCard.Length - 1)
                j %= sideCard.Length;
            sideCard[j].GetComponentInChildren<TextMeshProUGUI>().text = item.side;
            j++;
            Debug.Log("sidecard: " + sideCard[j].GetComponentInChildren<TextMeshProUGUI>().text);
        }

    }


    public void SideCardUIClickEvent(GameObject ele)
    {
        select[1] = !select[0];
        string right = ele.GetComponentInChildren<TextMeshProUGUI>().text;
        int i = 0, j = 0;
        cardList = CardData.Instance.CardList;
        foreach (var item in cardList)
        {
            if (item is CombineCard)
            {
                var comCard = item as CombineCard;
                // Debug.Log(combineCard.attribute);
                if (comCard.back_name == right)
                {
                    txtLeft[i] = comCard.attribute;
                    combine[i] = txtRight[i].combine;
                    Debug.Log("get: " + i + txtLeft[i]);
                    i++;
                }
            }
        }

        foreach (var item in txtLeft)
        {
            if (j >= elements.Length - 1)
                j %= elements.Length;

            Debug.Log("Yes");

            if (item == elements[j].GetComponentInChildren<TextMeshProUGUI>().text)
            {
                elements[j].GetComponent<Image>().color = Color.red;
                combineCard.GetComponentInChildren<TextMeshProUGUI>().text = combine[j];
                Debug.Log("combineCard" + combine[j]);
                j++;
            }
            else
            {
                elements[j].GetComponent<Image>().color = Color.white;
                // j++;
            }
        }


    }

    public void CombineCardUIClickEvent(GameObject ele)
    {

    }
}

