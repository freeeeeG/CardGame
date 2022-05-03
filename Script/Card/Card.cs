public class Card
{
    
    //卡牌编号
    public int id;
    public string cardName;
    public int mo;  //墨
    public int num; //卡牌数量
  
    public Card(int _id, string _cardName,int _mo,int _num)
    {
        this.id = _id;
        this.cardName = _cardName;
        this.mo = _mo;
        this.num = _num;
        
    }
    public virtual void Skill()
    {
        
    }
}


// 法术卡类 继承自卡牌类   （属性牌）
public class SpellCard : Card
{
  
    public string effect;  //卡牌效果
    

    public SpellCard(int _id, string _cardName,int _mo, int _num,string _effect) : base(_id, _cardName,_mo,_num)
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
    public CombineCard(int _id,string _cardName,int _mo,int _num,string _attribute,  string _back_name, string _effect) :base(_id, _cardName, _mo,_num)
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

    public SideCard(int _id, string _cardName,int _mo, int _num,string _effect) : base(_id, _cardName,_mo,_num)
    {
        this.effect = _effect;
    }


}
//卦象牌，需要使用八卦牌才会加入手牌的牌，frontid和backid表示需要的前后八卦牌的卡牌id
public class DivinationCard :Card
{
    
    public string effect;
    public string[] attibute ;
    public DivinationCard(int _id,string _cardName,int _mo,int _num,string _effect,string[] _attibute):base(_id,_cardName,_mo,_num)
    {
       
        this.effect = _effect;
        this.attibute = _attibute;
    }

}



// 消耗卡类 继承自卡牌类
public class ItemCard : Card
{
    public string effect;
    public string type;
    public ItemCard(int _id, string _cardName, int _mo,int _num, string _attribute, string _type, string _effect) : base(_id, _cardName, _mo,_num)
    {
        {
            this.type = _type;
            this.effect = _effect;
        }
    }
}

