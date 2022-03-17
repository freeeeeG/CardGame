using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GamePhase
{
    playerDraw, playerAction, enemyDraw, enemyAction, gameStart
}
public class BattleManager : Singleton<BattleManager>
{
    public GameObject playerData; // 数据
    public GameObject enemyData;
    public GameObject playerHands; // 手牌
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
    public GamePhase currentPhase;


    private int waitingID;
    public GameObject attackingMonster;
    private int attackingID;


    public GameObject[] playerBlocks; // 怪兽区
    public GameObject[] enemyBlocks;

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }
    void GameStart()
    {
        currentPhase = GamePhase.gameStart;
        ReadDeck();
    }

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
                    // playerDeckList.Add(CardDate.CopyCard(i));
                }
            }
        }
    
    }


    public void DrawCard(int _player, int _number)
    {
        if (_player == 0)
        {
            for (int i = 0; i < _number; i++)
            {
                GameObject newCard = GameObject.Instantiate(cardPrefab, playerHands.transform);
                // newCard.GetComponent<CardDisplay>().card = playerDeckList[0];
                playerDeckList.RemoveAt(0);
                // newCard.GetComponent<BattleCard>().cardState = CardState.inPlayerHand;
            }

        }
        else if (_player == 1)
        {
            for (int i = 0; i < _number; i++)
            {
                GameObject newCard = GameObject.Instantiate(cardPrefab, enemyHands.transform);
                // newCard.GetComponent<CardDisplay>().card = enemyDeckList[0];
                enemyDeckList.RemoveAt(0);
                // newCard.GetComponent<BattleCard>().cardState = CardState.inEnemyHand;
            }

        }
    }

    //每回合生命周期
    #region 
    //抽牌阶段
    public void OnPlayerDrawCard()
    {
        if (currentPhase == GamePhase.playerDraw)
        {
            DrawCard(0, 1);
            currentPhase = GamePhase.playerAction;
        }
    }

        public void OnEnemyDrawCard()
    {
        if (currentPhase == GamePhase.enemyDraw)
        {
            DrawCard(1, 1);
            currentPhase = GamePhase.enemyAction;
        }
    }
        public void OnClickTurnEnd()
    {
        TurnEnd();
    }

    #endregion
    void TurnEnd()
    {
        if (currentPhase == GamePhase.playerAction)
        {
            currentPhase = GamePhase.enemyDraw;
        }
        else if (currentPhase == GamePhase.enemyAction)
        {
            currentPhase = GamePhase.playerDraw;
        }
    }



    public void AttackRequst(Vector2 _startPoint, int _player, GameObject _monster)
    {
        if (arrow == null)
        {
            arrow = GameObject.Instantiate(attackPrefab, canvas);
        }

        arrow.GetComponent<ArrowFollow>().SetStartPoint(_startPoint);

        // 直接攻击条件
        bool strightAttack = true;
        if (_player == 0)
        {
            foreach (var block in enemyBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard != null)
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
        if (_player == 1)
        {
            foreach (var block in playerBlocks)
            {
                if (block.GetComponent<CardBlock>().monsterCard != null)
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

    public void SummonCofirm(Transform _block) // 召唤确认，点击格子时触发
    {
        Summon(waitingMonster, waitingID, _block);
        waitingMonster = null;
    }
    public void Summon(GameObject _monster, int _id, Transform _block)
    {
        //TODO：召唤人物或者移动到格子
    }
}
