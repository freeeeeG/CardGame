using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading;
using System.Collections;
using System;
using System.Reflection;
public class BattleCard : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IPointerEnterHandler
{
    public BattleManager BattleManager;
    public int id;
    public bool hasAttacked;
    public Vector3 oringinalPosition;
    public Vector3 mousePos;
    public RectTransform rectTransform;
    public bool isCardFolled = false;
    public Card card;

    public Action<string, int, int, float, GameObject> useCard;//使用卡牌,参数：卡牌名称，卡ID，技能ID，时间,卡牌对象

    // Start is called before the first frame update
    void Start()
    {
        BattleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        oringinalPosition = transform.position;
        rectTransform = GetComponent<RectTransform>();
        useCard += Player.Instance.UseCard;
        useCard += BattleManager.Instance.UseCard;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;

        if (isCardFolled)
        {
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }

    }


    #region PointerEvent
    public void OnPointerDown(PointerEventData eventData)
    {
        if (BattleManager.Instance.currentPhase == GamePhase.playerAction)
        {
            isCardFolled = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        // rectTransform.position -= new Vector3(0, 0, 1);

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (BattleManager.Instance.currentPhase == GamePhase.playerAction)
        {
            if (transform.position.y > 500f)
            {
                UseCard();
            }
            transform.position = oringinalPosition;
            isCardFolled = false;
            BattleManager.Instance.CardByCard();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        BattleManager.Instance.CardByCard();
    }

    #endregion
    void UseCard()
    {
        transform.position = oringinalPosition;
        Debug.Log("UseCard");
        // TODO: 动画
        useCard.Invoke("Skill", id, 1, 0, gameObject);

        var t = Type.GetType("id_102");
        var obj = Activator.CreateInstance(t);

        MethodInfo method_1 = t.GetMethod("Skill");
        method_1.Invoke(obj, null);

        // StartCoroutine(BattleManager.Instance.CardDown(0.2f/(BattleManager.Instance.playerHandsCounts+1)));
        CamareManager.Instance.FollowPlayer(1f);

        SceneManagers.Instance.followMouseFlag = false;


        if (id == BattleManager.Instance.playerHandsCounts)
            gameObject.SetActive(false);



    }
    IEnumerator UseCardWaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("WaitForSeconds");
        SceneManagers.Instance.followMouseFlag = true;

        Player.Instance.GetComponent<Animator>().SetInteger("Skill", 0);
        if (BattleManager.Instance.currentPhase == GamePhase.playerAction)
        {

        }
        transform.position = oringinalPosition;
        BattleManager.Instance.CardByCard();



    }
}
