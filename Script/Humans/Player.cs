using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    #region 
    //Player status
    public PlayerStatusData playerStatus;
    public bool IsDead = false;
    #endregion

    //Player pysical
    #region
    public Vector2 moveDir;
    public Rigidbody2D rb;
    public CheakAround onFlood;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
        Jump();
    }


    public void Move()
    {
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");
        moveDir = new Vector2(Horizontal, Vertical);
        rb.velocity = new Vector2(moveDir.x * playerStatus.speed, moveDir.y);
        if(Horizontal>0)
        transform.localScale = new Vector3(1, 1, 1);
        else if(Horizontal<0)
        transform.localScale = new Vector3(-1, 1, 1);
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, playerStatus.speed * 2));
        }
    }



}
