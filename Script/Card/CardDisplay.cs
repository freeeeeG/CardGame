using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public GameObject Card;
    public Card card;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI mo;   //墨值

    public TextMeshProUGUI infoText;

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
        mo.text = card.mo.ToString();
        infoText.text = card.effect;
    }
    
    public void ShowCard(Card _card)
    {
        cardName.text = _card.cardName;
        mo.text = _card.mo.ToString();
        infoText.text = _card.effect;
    }

}
