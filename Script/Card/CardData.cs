using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CardData : Singleton<CardData>
{
    public List<Card> CardList = new List<Card>(); // 存储卡牌数据的链表

    
    public TextAsset cardListData; // 卡牌数据txt文件
    // Start is called before the first frame update

    private void OnEnable() {
        // Debug.Log("执行");
        LordCardList();
        TestCard();
    }
    void Start()
    {

    }

    //加载卡组
    public void LordCardList()
    {
        // Debug.Log("执行2");
        string[] dataArray = cardListData.text.Split('\n');
        foreach (var row in dataArray)
        {
            string[] rowArray = row.Split(',');

            //for(int i=0;i<rowArray.Length;i++)  //测试读取情况
            //{
            //    Debug.Log(""+rowArray[i]);
            //}


            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "spell")
            {
                // Debug.Log("执行2");
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int mo = int.Parse(rowArray[3]);
                
                
                
                string effect = rowArray[4];
                CardList.Add(new SpellCard(id, name,  mo, effect ));
            }

            else if(rowArray[0]=="side")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int mo = int.Parse(rowArray[3]);
                string type = rowArray[4];
                string effect = rowArray[5];
                CardList.Add(new SideCard(id, name, mo, effect));
            }
            else if(rowArray[0]== "divination")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int mo = int.Parse(rowArray[3]);
                
                string effect = rowArray[6];
                int front_id= int.Parse(rowArray[4]);
                int back_id= int.Parse(rowArray[5]);
                CardList.Add(new DivinationCard(id, name, mo,  effect,front_id,back_id));
            }
            else if(rowArray[0]=="combine")
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int mo = int.Parse(rowArray[3]);
                string attibute = rowArray[4];
                string effect = rowArray[6];
                
                string back_name = rowArray[5];

                CardList.Add(new CombineCard(id, name, mo, attibute, effect, back_name));
            }
        }
    
    }
    public void TestCard()  //测试读取到的卡牌
    {
        foreach (var cards in CardList)
        {
            if(cards is CombineCard)
            {
                var combineCard =cards as CombineCard;
                // Debug.Log(combineCard.attribute + combineCard.back_name);
            }

        }
    }


    
    void TestCopy()//复制数据，需要创建一个新的实例，从原有的数据中构建
    {
        List<Card> copylist = new List<Card>();
        copylist = CardList;
        Card card1 = copylist[1];
        Card card2 = new Card(card1.id, card1.cardName,card1.mo);
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
        Card copyCard = new Card(_id, card.cardName,card.mo);

        if (card is SpellCard)
        {
            var spellcard = card as SpellCard;
            copyCard = new SpellCard(_id, spellcard.cardName, spellcard.mo, spellcard.effect);
        }

        return copyCard;
    }
    public List <Card>  GetCard()
    {
        return CardList;
    }
}
