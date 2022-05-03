using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
public class PlayerCardData : MonoBehaviour
{
    private TextAsset playerData;
    public Text coinsText;
    public Text cardsText;
    public int totalCoins;
    public int[] playerCards;
    public int[] playerDeck;

    public string datapath = "/Datas/PlayerCard.csv";
    public List<Card> cardlistALL;
    public List<Card> cardlist;
    private void Start()
    {

        //测试单例调用
        cardlistALL = CardData.Instance.GetCard();
        //cardlist = cardlistALL;
       
        foreach (var card in cardlistALL)
        {
            // Debug.Log("" + card.cardName);
        }
        //LoadPlayerData();  //再读取玩家数据
        //AddCard(cardlistALL[6]); //测试添加卡
    }
    private void Update(){
    //if(Input.GetKey(KeyCode.W)) {
    //    SavePlayerData(cardlist);
    //}
    //if(Input.GetKey(KeyCode.S))
    //    {
    //        LoadPlayerData();
    //        TestPlayerCard();
    //    }
    //if(Input.GetKey(KeyCode.A))
    //    {
    //        AddCard(cardlistALL[6]);
    //    }
}
    public void SavePlayerData(List<Card> _cardlist)
    {
        cardlist = _cardlist;


        List<string> datas = new List<string>();
        string path = Application.dataPath + datapath;
        // Debug.Log(totalCoins);
        datas.Add("cards," + cardlist.Count.ToString());
        datas.Add("#,卡id,卡名,墨,数量");
        foreach(var card in cardlist)   //保存卡组
        {
            if(card!=null)
            {
                datas.Add("card," + card.id +","+ card.cardName + "," + card.mo.ToString() + "," + card.num.ToString());
            }

            //if(card is SpellCard)
            //{
            //    datas.Add("spell," + card.id+card.cardName+card.num.ToString());
            //}
            //if (card is CombineCard)
            //{
            //    datas.Add("combine," + card.id + card.cardName + card.num.ToString());
            //}
            //if (card is SideCard)
            //{
            //    datas.Add("side," + card.id + card.cardName + card.num.ToString());
            //}
            //if (card is DivinationCard)
            //{
            //    datas.Add("divination," + card.id + card.cardName + card.num.ToString());
            //}
        }
        
        File.WriteAllLines(path, datas,Encoding.UTF8);
       

    }
    public void LoadPlayerData()
    {
        cardlist.Clear();


        string[] datarow = File.ReadAllLines(Application.dataPath+datapath);
        //string[] datarow = playerData.text.Split('\n');
        foreach (var row in datarow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0]=="card")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int mo = int.Parse(rowArray[3]);
                int num = int.Parse(rowArray[4]);
                cardlist.Add(new Card(id, name, mo, num));
              //  Debug.Log("" + name + id.ToString());
            }
        }
    }
    public void AddCard(Card _card)
    {
        bool flag = false;

            for (int i = 0; i < cardlist.Count; i++)    //按照id循环查找卡牌
            {
                //Debug.Log("卡id" + cardlist[i].id + "i:" + i);
                if (cardlist[i].id == _card.id)
                {
                    cardlist[i].num += 1;
                    Debug.Log("添加的卡" + cardlist[i].id.ToString() + "数量" + cardlist[i].num);
                    flag = true;
                    break;
                }

            }
        
        if(flag)
        {
            return;
        }
        else
        {
            cardlist.Add(new Card(_card.id, _card.cardName, _card.mo, 1));
        }
        
    }
    public void DeleteCard(Card _card)
    {
        bool flag = false;
        for (int i = 0; i < cardlist.Count; i++)    //循环查找卡牌
        {
            if (cardlist[i].id == _card.id)
            {
                if(cardlist[i].num>0)
                {
                    cardlist[i].num--;
                }
                else if(cardlist[i].num<=0)
                {
                    cardlist.RemoveAt(i);
                    Debug.Log("删除成功");
                }
                
                
                flag = true;
                return;
            }

        }
        if(flag)
        Debug.Log("未找到卡牌");
        else
        {
            return;
        }
    }
    public void TestPlayerCard()  //测试读取到的卡牌
    {
        for(int i=0;i<cardlist.Count;i++)
        {
            Debug.Log(cardlist[i].cardName + cardlist[i].id + "数量" + cardlist[i].num);
        }
    }
}
