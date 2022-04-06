

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
}


// 法术卡类 继承自卡牌类   （属性牌）
public class SpellCard : Card
{
  
    public string type;  //卡牌类型
    public string effect;  //卡牌效果
    public string attribute;  //属性 金 木 水 火 土

    public SpellCard(int _id, string _cardName,int _mo,string _attribute, string _type, string _effect) : base(_id, _cardName,_mo)
    {
      
        this.type = _type;
        this.effect = _effect;
        this.attribute = _attribute;
    }
}
//场地卡类 继承自卡牌类 （八卦牌）
public class FeildCard : Card
{
    public string type;
    public string effect;

    public FeildCard(int _id,string _cardName,int _mo,string _type,string _effect) :base(_id,_cardName,_mo)
    {
        this.type = _type;
        this.effect = _effect;
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

    //武器卡类 继承自卡牌类
    public class WeaponCard : Card
    {
        public int attack;
        public int range;
        public WeaponCard(int _id, string _cardName, int _mo, string _attribute, int _attack, int _range) : base(_id, _cardName, _mo)
        {
            this.attack = _attack;
            this.range = _range;
        }
    }
    //魔法卡类 继承自卡牌类
    public class MagicCard : Card
    {
        public int rank;
        public string type;
        public string effect;
        public MagicCard(int _id, string _cardName, int _mo, string _attribute, int _rank, string _type, string _effect) : base(_id, _cardName, _mo)
        {
            this.rank = _rank;
            this.type = _type;
            this.effect = _effect;
        }
    }
}



