using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
public class PlayerCardData : Singleton<PlayerCardData>
{
    private TextAsset playerData;
    public int totalCoins;
    public int[] playerCards;
    public int[] playerDeck;

    public int cardCount;

    public string datapath = "/Datas/PlayerCard.csv";
    public List<Card> cardlistALL = new List<Card>();
    public List<Card> cardList = new List<Card>();
    public List<Card> cardDeskList = new List<Card>();
    private void Start()
    {

        //测试单例调用
        cardlistALL = CardData.Instance.GetCard();

        foreach (var card in cardlistALL)
        {
            // Debug.Log("" + card.cardName);
        }
        LoadPlayerData();  //再读取玩家数据
        foreach (var card in cardList)
        {
            Debug.Log("" + card.cardName);
        }
        //AddCard(cardlistALL[6]); //测试添加卡
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SavePlayerData(cardList);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadPlayerData();
            TestPlayerCard();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddCard(cardlistALL[6]);
        }
    }

    #region 背包
    public void SavePlayerData(List<Card> _cardlist)
    {
        cardList = _cardlist;


        List<string> datas = new List<string>();
        string path = Application.dataPath + datapath;
        Debug.Log(path);
        // Debug.Log(totalCoins);
        datas.Add("cards," + cardCount);
        datas.Add("#,卡id,卡名,墨,数量");
        foreach (var card in cardList)   //保存卡组
        {
            if (card != null)
            {
                datas.Add("card," + card.id + "," + card.cardName + "," + card.mo.ToString() + "," + card.num.ToString());

            }
        }

        File.WriteAllLines(path, datas, Encoding.UTF8);


    }
    public void LoadPlayerData()
    {
        cardList.Clear();


        string[] datarow = File.ReadAllLines(Application.dataPath + datapath);
        //string[] datarow = playerData.text.Split('\n');
        foreach (var row in datarow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "card")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[4]);
                var tempCard = CardData.Instance.GetCard(id);
                tempCard.num = num;
                cardList.Add(tempCard);
            }
            else if (rowArray[0] == "cards")
            {
                cardCount = int.Parse(rowArray[1]);
            }
            else
            {
                Debug.Log("error");
            }
        }
    }
    public void AddCard(Card _card)
    {


        for (int i = 0; i < cardList.Count; i++)    //按照id循环查找卡牌
        {
            //Debug.Log("卡id" + cardlist[i].id + "i:" + i);
            if (cardList[i].id == _card.id)
            {
                cardList[i].num += 1;
                cardCount++;
                Debug.Log("添加的卡" + cardList[i].id.ToString() + "数量" + cardList[i].num);
                break;
            }

        }

    }
    public void DeleteCard(Card _card)
    {
        for (int i = 0; i < cardList.Count; i++)    //循环查找卡牌
        {
            if (cardList[i].id == _card.id)
            {
                if (cardList[i].num == 0)
                {
                    // TODO: 取消删除
                    return;
                }
                if (cardList[i].num > 0)
                {
                    cardList[i].num--;
                }
                return;
            }

        }
    }
    public void TestPlayerCard()  //测试读取到的卡牌
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            Debug.Log(cardList[i].cardName + cardList[i].id + "数量" + cardList[i].num);
        }
    }
    #endregion


    #region 卡组

    #endregion



}