using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegs : MonoBehaviour
{
    public bool isGoal;
    public int HP;
    public int Attack;
    public int Armor; 
    GamePlayManager gpm;
    public int reward;
    bool killed;
    bool RewardGet = false;

    void Start()
    {
        gpm = FindObjectOfType<GamePlayManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !killed) 
        {
            if (!isGoal)//普通obs
            {
                if (!RewardGet)
                {
                    gpm.score += reward;
                    RewardGet = true;
                }
                HP -= 1;
                if (HP <= 0)
                {
                    killed = true;
                    Destroy(gameObject, 0.2f);
                }
            }
            else//目标obs
            {
                HP -= gpm.Attack;
                if (HP>0)//敌人未死亡的情况下才会造成伤害
                gpm.Hp -= Attack;
                if (HP <= 0)
                {
                    killed = true;
                    gpm.goal -= 1;
                    gpm.score += reward;
                    Destroy(gameObject, 0.1f);
                }
            }
        }
    }
    public void ClearNormal()
    {
        if (RewardGet)
        {
            Destroy(gameObject, 0.2f);
        }
    }
    void Update()
    {
        
    }
}
