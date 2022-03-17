using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public List<Card> CardList = new List<Card>(); // 存储卡牌数据的链表
    public TextAsset cardListData; // 卡牌数据txt文件
    // Start is called before the first frame update


    //加载卡组
    public void LordCardList()
    {
        string[] dataArray = cardListData.text.Split('\n');
        foreach (var row in dataArray)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "s")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int rank = int.Parse(rowArray[3]);
                string type = rowArray[4];
                string effect = rowArray[5];
                CardList.Add(new SpellCard(id, name, rank, type, effect));
            }
            else if (rowArray[0] == "t")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                string type = rowArray[3];
                string effect = rowArray[4];
                CardList.Add(new ItemCard(id, name, type, effect));
            }
        }
    
    }


    
    void TestCopy()//复制数据，需要创建一个新的实例，从原有的数据中构建
    {
        List<Card> copylist = new List<Card>();
        copylist = CardList;
        Card card1 = copylist[1];
        Card card2 = new Card(card1.id, card1.cardName);
        Card card3 = CopyCard(1);

        card1.cardName = "DarkKnight";
        print("test copy");
        print(card3.cardName + "," + card1.GetType());
        print(card2.cardName + "," + card2.GetType());
        print(CardList[1].cardName + "," + CardList[1].GetType());

    }

        public Card CopyCard(int _id) // 从卡牌数据中复制一个实体，这个实体的改变不会影响原始数据
    {
        Card card = CardList[_id];
        Card copyCard = new Card(_id, card.cardName);

        if (card is SpellCard)
        {
            var spellcard = card as SpellCard;
            copyCard = new SpellCard(_id, spellcard.cardName, spellcard.rank, spellcard.type, spellcard.effect);
        }
        else if (card is ItemCard)
        {
            var itemcard = card as ItemCard;
            copyCard = new ItemCard(_id, itemcard.cardName, itemcard.type, itemcard.effect);
        }
        return copyCard;
    }
}
