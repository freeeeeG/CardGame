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

   
    public List<Card> cardlist;
    private void Start()
    {

        //测试单例调用
        cardlist = CardData.Instance.GetCard();

        foreach (var card in cardlist)
        {
            // Debug.Log("" + card.cardName);
        }
        //LoadPlayerData();  //再读取玩家数据
    }
    private void Update(){
    if(Input.GetKey(KeyCode.W)) {
        SavePlayerData();
    }
}
    public void SavePlayerData()
    {
        List<string> datas = new List<string>();
        string path = Application.dataPath + "/Datas/test.csv";
        // Debug.Log(totalCoins);
        datas.Add("coins," + totalCoins.ToString());
        for (int i = 0; i < playerCards.Length; i++)
        {
            if (playerCards[i] != 0)
            {
                datas.Add("card," + i.ToString() + "," + playerCards[i].ToString());
            }
        }
        for (int i = 0; i < playerDeck.Length; i++)
        {
            if (playerDeck[i] != 0)
            {
                datas.Add("deck," + i.ToString() + "," + playerDeck[i].ToString());
            }
        }
        foreach (var item in datas)
        {
            // Debug.Log(item);
        }
        File.WriteAllLines(path, datas);
        
    }
    public void LoadPlayerData()
    {
        playerCards = new int[cardlist.Count];
        string[] datarow = playerData.text.Split('\n');
        foreach (var row in datarow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "coins")
            {
                totalCoins = int.Parse(rowArray[1]);
            }
            else if (rowArray[0] == "card")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);   //卡牌数量

            }
        }
    }
}
