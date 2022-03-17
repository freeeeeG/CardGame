using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : Singleton<GameManager>
{

    public bool restart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

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
