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
    public string dataPathDesk = "/Datas/PlayerDeskCard.csv";
    public List<Card> cardListALL = new List<Card>();  //全卡组列表，来自carddata
    public List<Card> cardList = new List<Card>();    //背包卡列表
    public List<Card> cardDeskList = new List<Card>();   //玩家卡组的id的列表
    private void Start()
    {

        //测试单例调用
        cardListALL = CardData.Instance.GetCardList();
        LoadDeskCard();
        LoadPlayerData();

    }
    private void Update()
    {
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

    public void ResetCardList()  //重置背包卡组
    {
        cardList = cardListALL;
        SavePlayerData(cardListALL);
        Debug.Log("重置背包卡组为初始状态");
    }
    #endregion


    #region 卡组

    #endregion
    public void SaveDeskCard(List<Card> _cardLsit)  // 保存卡组  
    {
        cardDeskList = _cardLsit;

        List<string> datas = new List<string>();
        string path = Application.dataPath + dataPathDesk;
        Debug.Log(path);
   
        datas.Add("#,卡id");
        foreach (var card in cardDeskList)   //保存卡组
        {
            if (card != null)
            {
                datas.Add("card," + card.id );
            }
        }

        File.WriteAllLines(path, datas, Encoding.UTF8);

    }
    public void LoadDeskCard()    //读取卡组
    {
        cardDeskList.Clear();
        string[] datarow = File.ReadAllLines(Application.dataPath + dataPathDesk);
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
                var tempCard = CardData.Instance.GetCard(id);
                Debug.Log("读取卡组" + tempCard.cardName + tempCard.id);
                cardDeskList.Add(tempCard);

            }
            else
            {
                Debug.Log("error");
            }
        }
    }
    public void ResetDeskCard()  //重置玩家卡组
    {
        cardDeskList.Clear();
        SaveDeskCard(cardDeskList);
    }
    public void AddCardToDesk(Card  _card)    //将背包的卡加入玩家卡组
    {
        for(int i=0;i<cardList.Count;i++)
        {
            if(cardList[i].id==_card.id)
            {
                if(cardList[i].num<=0)
                {
                    Debug.Log("添加进入卡组失败，卡牌数量为0");
                }
                else if(cardList[i].num>0)
                {
                    cardList[i].num--;
                    var tempCard = CardData.Instance.GetCard(_card.id);
                    cardDeskList.Add(tempCard);
                    Debug.Log("添加卡牌进入卡组成功");
                }
                else
                {
                    Debug.Log("添加错误error");
                }
                
            }
        }

    }
    public void DeleteCardFDesk(Card _card)    //将卡从玩家卡组中删除
    {
        for(int i=0;i<cardDeskList.Count;i++)
        {
            if(cardDeskList[i].id==_card.id)
            {
                cardDeskList.RemoveAt(i);  //找到就删除卡牌
                AddCard(_card);   //删除后卡牌数量增加
            }
            else
            {
                Debug.Log("未在卡组找到卡牌");
            }
        }
    }


}