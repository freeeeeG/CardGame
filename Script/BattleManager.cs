using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GamePhase
{
    playerDraw, playerAction, enemyAction, gameStart
}
public class BattleManager : Singleton<BattleManager>
{


    public GameObject playerData; // 数据
    public int playerHandsCounts; // 手牌数
    public GameObject[] playerHands; // 手牌
    public GameObject enemyHands;
    public Transform canvas;
    
    private GameObject waitingMonster;
    public GameObject cardPrefab;

    public GameObject arrowPrefab;//召唤指示箭头
    public GameObject attackPrefab;//攻击指示箭头
    private GameObject arrow;
    public List<Card> playerDeckList = new List<Card>(); // 卡组
    public List<Card> enemyDeckList = new List<Card>();
    // 生命值
    public int playerHealthPoint;
    public int enemyHealthPoint;

    public GameObject playerIcon;
    public GameObject enemyIcon;
    //回合阶段
    public GamePhase currentPhase = GamePhase.gameStart;

    private CardData cardDate;
    private int waitingID;
    public GameObject attackingMonster;
    private int attackingID;
    

    public bool isPlayerTurn = true;
    // Start is called before the first frame update




    public void GameStart()
    {
        // 敌方先手
        Debug.Log("Game Start");
        if(isPlayerTurn)
        currentPhase = GamePhase.playerDraw;
        else
        currentPhase = GamePhase.enemyAction;
        ReadDeck();
        OnPlayerDrawCard();
    }


    // 读取卡组
    void ReadDeck()
    {
        PlayerCardData pdm = playerData.GetComponent<PlayerCardData>();
        for (int i = 0; i < pdm.playerDeck.Length; i++)
        {
            if (pdm.playerDeck[i] != 0)
            {
                int counter = pdm.playerDeck[i];
                for (int j = 0; j < counter; j++)
                {
                    playerDeckList.Add(cardDate.CopyCard(i));
                }
            }
        }
    
    }


    //抽牌
    public void DrawCard(int _player, int _number)
    {
        if (_player == 0)
        {

            if(playerHandsCounts < 5)
            for (int i = 0; i < _number &&  playerHandsCounts < 5; i++)
            {
                playerHands[playerHandsCounts].SetActive(true);
                playerHandsCounts++;
                // TODO: 显示抽到的牌
                // newCard.GetComponent<CardDisplay>().card = playerDeckList[0];
                // playerDeckList.RemoveAt(0);
            }
            else
            {
                playerHands[playerHandsCounts].SetActive(true);
                playerHandsCounts++;
            }
        }
    }

    #region 每回合生命周期


    public void OnPlayerDrawCard()
    {
        if (currentPhase == GamePhase.playerDraw)
        {
            Debug.Log("抽牌");
            DrawCard(0, Player.Instance.drawCardCount);
            currentPhase = GamePhase.playerAction;
        }
    }
    public void OnClickTurnEnd()
    {
        TurnEnd();
    }
    void TurnEnd()
    {
        if (currentPhase == GamePhase.playerAction)
        {
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
            TurnEnd();
        }

    }


    #endregion
    public void AttackRequest(Vector2 _startPoint, int _player, GameObject _monster)
    {
        if (arrow == null)
        {
            arrow = GameObject.Instantiate(attackPrefab, canvas);
        }

        arrow.GetComponent<ArrowFollow>().SetStartPoint(_startPoint);

        // 直接攻击条件
        bool strightAttack = true;
        if (_player == 1)
        {
            foreach (var block in playerHands)
            {
                if (block.GetComponent<CardBlock>().Card != null)
                {
                    block.GetComponent<CardBlock>().SetAttack();
                    strightAttack = false;
                }
            }
            if (strightAttack)
            {
                // 可以直接攻击对手玩家
            }
        }

        attackingMonster = _monster;
        attackingID = _player;

    }


    public void UseRequest(Vector2 _startPoint, int _player, GameObject _monster)
    {
        if (arrow == null)
        {
            arrow = GameObject.Instantiate(attackPrefab, canvas);
        }

        arrow.GetComponent<ArrowFollow>().SetStartPoint(_startPoint);

        //TODO: 技能使用
        bool strightAttack = true;
        if (_player == 1)
        {
            foreach (var block in playerHands)
            {
                if (block.GetComponent<CardBlock>().Card != null)
                {
                    block.GetComponent<CardBlock>().SetAttack();
                    strightAttack = false;
                }
            }
            if (strightAttack)
            {
                // 可以直接攻击对手玩家
            }
        }

        attackingMonster = _monster;
        attackingID = _player;

    }
    public void HandCardSort(int id)
    {
        for(int i = id; i < playerHandsCounts; i++)
        {
            playerHands[i].SetActive(true);

        }
        playerHands[playerHandsCounts].SetActive(false);
    }

}
