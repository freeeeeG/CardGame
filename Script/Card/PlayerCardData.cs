using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class PlayerCardData : MonoBehaviour
{
    public TextAsset playerData;
    public Text coinsText;
    public Text cardsText;
    public int totalCoins;
    public int[] playerCards;
    public int[] playerDeck;

   
    public List<Card> cardlistALL;
    public List<Card> cardlist;
    private void Start()
    {

        //测试单例调用
        cardlistALL = CardData.Instance.GetCard();
        cardlist = cardlistALL;
        foreach (var card in cardlistALL)
        {
            // Debug.Log("" + card.cardName);
        }
        //LoadPlayerData();  //再读取玩家数据
    }
    private void Update(){
    if(Input.GetKey(KeyCode.W)) {
        SavePlayerData(cardlist);
    }
}
    public void SavePlayerData(List<Card> _cardlist)
    {
        cardlist = _cardlist;


        List<string> datas = new List<string>();
        string path = Application.dataPath + "/Datas/test.csv";
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
        
        File.WriteAllLines(path, datas);
        
    }
    public void LoadPlayerData()
    {
        cardlist.Clear();
        string[] datarow = playerData.text.Split('\n');
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
            }
        }
    }
}
