
public class Card
{
    //卡牌编号
    public int id;
    public string cardName;
    public Card(int _id, string _cardName)
    {
        this.id = _id;
        this.cardName = _cardName;
    }
}


// 法术卡类 继承自卡牌类
public class SpellCard : Card
{
    public int rank;
    public string type;
    public string effect;

    public SpellCard(int _id, string _cardName, int _rank, string _type, string _effect) : base(_id, _cardName)
    {
        this.rank = _rank;
        this.type = _type;
        this.effect = _effect;
    }
}
// 消耗卡类 继承自卡牌类
public class ItemCard : Card
{
    public string effect;
    public string type;
    public ItemCard(int _id, string _cardName, string _type, string _effect) : base(_id, _cardName)
    {
        this.type = _type;
        this.effect = _effect;
    }
}

// 武器卡类 继承自卡牌类
public class WeaponCard : Card
{
    public string effect;
    public string type;
    public WeaponCard(int _id, string _cardName, string _type, string _effect) : base(_id, _cardName)
    {
        this.type = _type;
        this.effect = _effect;
    }
}