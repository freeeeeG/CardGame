using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : Singleton<GameManager>
{

    public bool restart = false;
    public GameObject battleScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void Update() {
        if(BattleManager.Instance.currentPhase == GamePhase.gameStart)
        {
            //TODO: 加载场景，敌人出现
            battleScene.SetActive(true);
            BattleManager.Instance.GameStart();
        }
        
    }
    void GameOver(bool isDead)
    {
        if (isDead)
        {
            Debug.Log("Game Over");
            restart = true;
        }
    }



    void Restart()
    {
        //TODO: Restart
        restart = false;
        Player.Instance.playerStatus.Init();
    }
}
