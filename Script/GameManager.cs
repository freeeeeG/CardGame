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
        battleScene = GameObject.Find("BattleScene");
    }

    // Update is called once per frame

    private void Update() {

        if(Input.GetKeyDown(KeyCode.R))
        {
            BattleManager.Instance.currentPhase = GamePhase.gameStart;
        }
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

    public void GameStop()
    {
        
    }
    void Restart()
    {
        //TODO: Restart
        restart = false;
        Player.Instance.data.datas.Init();
    }
}
