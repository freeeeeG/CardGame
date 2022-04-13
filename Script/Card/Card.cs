

public class Card
{
    
    //卡牌编号
    public int id;
    public string cardName;
    public int mo;  //墨
  
    public Card(int _id, string _cardName,int _mo)
    {
        this.id = _id;
        this.cardName = _cardName;
        this.mo = _mo;
        
    }
    public void Skill()
    {
        
    }
}


// 法术卡类 继承自卡牌类   （属性牌）
public class SpellCard : Card
{
  
    public string effect;  //卡牌效果
    

    public SpellCard(int _id, string _cardName,int _mo, string _effect) : base(_id, _cardName,_mo)
    {
      
    
        this.effect = _effect;
        
    }
}
//组合卡牌  偏和旁组合而成的卡牌
public class CombineCard:Card
{
    public string effect;   //卡牌效果
    public string attribute;  //属性 
    public string back_name;    //旁卡牌名称
    public CombineCard(int _id,string _cardName,int _mo,string _attribute, string _effect, string _back_name):base(_id, _cardName, _mo)
    {
        this.effect = _effect;
        this.attribute = _attribute;
        this.back_name = _back_name;
    }

}


//旁卡 用来和属性牌组成字
public class SideCard : Card
{
   
    public string effect;

    public SideCard(int _id, string _cardName,int _mo, string _effect) : base(_id, _cardName,_mo)
    {
        this.effect = _effect;
    }


}
//卦象牌，需要使用八卦牌才会加入手牌的牌，frontid和backid表示需要的前后八卦牌的卡牌id
public class DivinationCard :Card
{
    
    public string effect;
    public int front_id;
    public int back_id;
    public DivinationCard(int _id,string _cardName,int _mo,string _effect,int _front_id,int _back_id):base(_id,_cardName,_mo)
    {
       
        this.effect = _effect;
        this.front_id = _front_id;
        this.back_id = _back_id;
    }

}


// 消耗卡类 继承自卡牌类
public class ItemCard : Card
{
    public string effect;
    public string type;
    public ItemCard(int _id, string _cardName, int _mo, string _attribute, string _type, string _effect) : base(_id, _cardName, _mo)
    {
        {
            this.type = _type;
            this.effect = _effect;
        }
    }
}





