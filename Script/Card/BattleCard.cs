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
            transform.position = oringinalPosition;
            isCardFolled = false;
            UseCard();
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
        BattleManager.Instance.playerHandsCounts--;
        transform.position = oringinalPosition;
        Debug.Log("UseCard");
        // TODO: 动画
        Player.Instance.GetComponent<Animator>().SetInteger("Skill", 1);

        BattleManager.Instance.HandCardSort(id,gameObject);
        CamareManager.Instance.speed_cf = CamareManager.Instance._speed_cf; 
        CamareManager.Instance.dv = CamareManager.Instance.dv*=3; 
        CamareManager.Instance.followPlayerFlag = true;

        SceneManagers.Instance.followMouseFlag = false;
        // 离开屏幕外面
        StartCoroutine(UseCardWaitForSeconds(1));


    }
    IEnumerator UseCardWaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("WaitForSeconds");
        CamareManager.Instance.followPlayerFlag = false;
        CamareManager.Instance.speed_cf = CamareManager.Instance._speed_cf; 
        CamareManager.Instance.dv = CamareManager.Instance.dv/=3; 
        SceneManagers.Instance.followMouseFlag = true;
        CamareManager.Instance.Shake();
        Player.Instance.GetComponent<Animator>().SetInteger("Skill", 0);

        if (BattleManager.Instance.currentPhase == GamePhase.playerAction)
        {

        }     
        transform.position = oringinalPosition;
        BattleManager.Instance.CardByCard();
        if(id == BattleManager.Instance.playerHandsCounts)
            gameObject.SetActive(false);    


    }
}
