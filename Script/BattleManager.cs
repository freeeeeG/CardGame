using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum GamePhase
{
    playerDraw, playerAction, enemyAction, gameStart
}
public class BattleManager : Singleton<BattleManager>
{

    public Action playerTurnEnd;
    public GameObject playerData; // 数据

    public GameObject enemyHands;
    public Transform canvas;


    private GameObject waitingMonster;
    public GameObject cardPrefab;

    public GameObject arrowPrefab;//召唤指示箭头
    public GameObject attackPrefab;//攻击指示箭头
    private GameObject arrow;
    public List<Card> playerDeckList = new List<Card>(); // 卡组

    public int drawCardCount = 0; // 抽卡数
    //回合阶段
    public GamePhase currentPhase = GamePhase.gameStart;

    private CardData cardDate;
    private int waitingID;
    public GameObject attackingMonster;
    private int attackingID;
    //手牌整理
    [Header("手牌整理")]
    public int playerHandsCounts; // 手牌数
    public GameObject[] playerHands; // 手牌
    public float playerHandsDistance = 1152; // 手牌间距
    public float playerHandsHigh = -281; // 手牌起始高度


    public bool isPlayerTurn = true;
    // Start is called before the first frame update




    public void GameStart()
    {
        // 敌方先手
        Debug.Log("Game Start");
        if (isPlayerTurn)
            currentPhase = GamePhase.playerDraw;
        else
            currentPhase = GamePhase.enemyAction;
        ReadDeck();
        OnPlayerDrawCard();
    }


    // 读取卡组
    void ReadDeck()
    { 
        playerDeckList = PlayerCardData.Instance.cardDeskList;
    }


    //抽牌
    public void DrawCard(int _player, int _number)
    {
        if (playerHandsCounts < 5)
            for (int i = 0; i < _number && playerHandsCounts < 5; i++)
            {
                playerHands[playerHandsCounts].SetActive(true);
                playerHandsCounts++;
                // TODO: 显示抽到的牌
                // playerDeckCardCount++;
                // if (playerDeckCardCount == playerDeckList.Count)
                // SortCard()，palyerDeckCardCount = 0;
                // playerHands[playerHandsCounts].GetComponent<CardDisplay>().card = playerDeckList[drawCardCount++];
                // if(drawCardCount == playerDeckList.Count)
                // {
                //     // TODO: 洗牌
                //     drawCardCount = 0;
                // }

            }
        else
        {
            playerHands[playerHandsCounts].SetActive(true);
            playerHandsCounts++;
        }

    }

    #region 每回合生命周期


    public void OnPlayerDrawCard()
    {
        if (currentPhase == GamePhase.playerDraw)
        {
            Debug.Log("抽牌");
            DrawCard(0, Player.Instance.drawCardCount);
            HandCardFan();
            CardByCard();
            currentPhase = GamePhase.playerAction;
        }
    }
    public void OnClickTurnEnd()
    {
        if (currentPhase == GamePhase.playerAction)
            TurnEnd();
    }
    void TurnEnd()
    {
        if (currentPhase == GamePhase.playerAction)
        {
            playerTurnEnd.Invoke();
            currentPhase = GamePhase.enemyAction;
            OnEnemyAction();
        }
        else if (currentPhase == GamePhase.enemyAction)
        {
            currentPhase = GamePhase.playerDraw;
            OnPlayerDrawCard();
        }

        //TODO: 更新回合(八卦牌)

    }

    public void OnEnemyAction()
    {
        if (currentPhase == GamePhase.enemyAction)
        {
            //TODO: 敌方行动
            StartCoroutine(EnemyAction(Enemy.Instance.EnemyAction()));

        }


    }
    #endregion

    //整理手牌
    public void HandCardSort(int id, GameObject card)
    {
        for (int i = id; i < playerHandsCounts; i++)
        {
            playerHands[i].GetComponent<CardDisplay>().card = playerHands[i + 1].GetComponent<CardDisplay>().card;

        }
        if (id == playerHandsCounts)
        {
            card.transform.position += new Vector3(10000, 0, 0);
        }
        else
        {
            playerHands[playerHandsCounts].SetActive(false);
        }
        HandCardFan();
        CardByCard();
    }

    public void HandCardFan()
    {
        Debug.Log("手牌整理");
        float st = 0 - playerHandsDistance / 2;
        float dx = playerHandsDistance / (playerHandsCounts + 1);
        float dTheta = 60 / (playerHandsCounts + 1);
        for (int i = 0; i < playerHandsCounts; i++)
        {
            playerHands[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(st + dx * (i + 1), playerHandsHigh, 0);

            playerHands[i].GetComponent<BattleCard>().oringinalPosition = playerHands[i].transform.position;
        }
    }

    public void CardByCard()
    {
        for (int i = 0; i < playerHandsCounts; i++)
        {
            playerHands[i].transform.SetAsFirstSibling();
            playerHands[i].GetComponent<BattleCard>().transform.position = playerHands[i].GetComponent<BattleCard>().oringinalPosition;
        }
    }

    public IEnumerator CardDown(float time)
    {
        for (int i = 0; i < playerHandsCounts; i++)
        {
            yield return new WaitForSeconds(time);
            playerHands[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(playerHands[i].GetComponent<RectTransform>().anchoredPosition3D.x, playerHands[i].GetComponent<RectTransform>().anchoredPosition3D.y - 1000, 0);
        }
        // CardByCard();
    }

    public void UseCard(string name, int id, int skillid, float time, GameObject card)
    {
        Instance.playerHandsCounts--;
        HandCardSort(id, gameObject);
        // gameObject.GetComponent<CardDisplay>().card.Skill();
    }

    IEnumerator EnemyAction(float time)
    {
        yield return new WaitForSeconds(time);
        TurnEnd();
    }
}
