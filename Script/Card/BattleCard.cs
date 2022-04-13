using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading;
using System.Collections;
public enum CardState //位置和所属状态
{
    inPlayerHand
}
public class BattleCard : MonoBehaviour, IPointerDownHandler, IPointerExitHandler,IPointerUpHandler,IPointerEnterHandler
{
    public BattleManager BattleManager;
    public int id;
    public CardState cardState = CardState.inPlayerHand;

    public bool hasAttacked;
    public Vector3 oringinalPosition;
    public Vector3 mousePos;
    public RectTransform rectTransform;
    public bool isCardFolled = false;


    // Start is called before the first frame update
    void Start()
    {
        BattleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        oringinalPosition = transform.position;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;

        if(isCardFolled)
        {
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }



    }


    #region PointerEvent
    public void OnPointerDown(PointerEventData eventData)
    {
        if(BattleManager.Instance.currentPhase == GamePhase.playerAction)
        {
            isCardFolled = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        // rectTransform.position -= new Vector3(0, 0, 1);

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(BattleManager.Instance.currentPhase == GamePhase.playerAction)
        {
            if(transform.position.y > 500f)
            UseCard();
            isCardFolled = false;
            transform.position = oringinalPosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        if(BattleManager.Instance.currentPhase == GamePhase.playerAction)
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(BattleManager.Instance.currentPhase == GamePhase.playerAction)
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    #endregion
    void UseCard()
    {
        BattleManager.Instance.playerHandsCounts--;
        transform.position = oringinalPosition;
        Debug.Log("UseCard");
        // TODO: 动画

        CamareManager.Instance.PlayerFollow();
        StartCoroutine(UseCardWaitForSeconds(3));     

    }
    IEnumerator UseCardWaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("WaitForSeconds");

        CamareManager.Instance.CentreScreen();
        this.gameObject.SetActive(false);
        CamareManager.Instance.Shake();
        BattleManager.Instance.HandCardSort(id);
        if (cardState == CardState.inPlayerHand && BattleManager.Instance.currentPhase == GamePhase.playerAction)
        {
            //可以攻击敌方的卡牌
            if (transform.GetComponent<CardDisplay>().card is Card)
            {
                BattleManager.Instance.AttackRequest(transform.position, 0, transform.gameObject);
            }
            //可以对自己使用的卡牌
            else if (transform.GetComponent<CardDisplay>().card is Card)
            {
                BattleManager.Instance.UseRequest(transform.position, 1, transform.gameObject);
            }   
        }
    }
}
