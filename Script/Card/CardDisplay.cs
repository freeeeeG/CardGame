using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Text cardName;
    public Text mo;   //墨值


    public Text infoText;

    public Image background;

    public Color besideColor;
    public Color feildColor;
    public Color spellColor;
    public Color divinationColor;

    // Start is called before the first frame update
    void Start()
    {
        // ColorUtility.TryParseHtmlString("#387445", out monsterColor);
        // ColorUtility.TryParseHtmlString("#556874", out itemColor);
        // ColorUtility.TryParseHtmlString("#79548E", out spellColor);
        if (card != null)
        {
            ShowCard();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowCard()
    {
        cardName.text = card.cardName;
        //if (card is ItemCard)
        //{
        //    var itemcard = card as ItemCard;
        //    cardAttack.gameObject.SetActive(false);
        //    cardHealth.gameObject.SetActive(false);
        //    background.color = itemColor;
        //    infoText.text = itemcard.effect;
        //}
        // 如果是旁牌，则不需要显示墨值
        if(card is SideCard)
        {
            var besidecard = card as SideCard;
            mo.gameObject.SetActive(false);
            background.color = besideColor;
            infoText.text = besidecard.effect;
        }
  
        else if (card is SpellCard)
        {
            var spellcard = card as SpellCard;

            background.color = spellColor;
            infoText.text = spellcard.effect;
        }

        else if (card is DivinationCard)
        {
            var divinationcard = card as DivinationCard;
            background.color = feildColor;
            infoText.text = divinationcard.effect;


        }

    }
}
