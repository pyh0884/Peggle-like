using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public GameObject Sticks;
    public GameObject Rewards;
    public GameObject Obstacles;//结束时摧毁全部obs/维持不变，增加积分
    public Pegs[] obstaclePegs;
    public int goal;
    public int score;
    public Text ScoreTxt;
    public Text HpTxt;
    public Text AtkTxt;
    public Text TimeScale;
    public GameObject win;
    public GameObject lose;
    public BallAI ball;
    public int Hp = 100;
    public int Attack = 10;
    bool endGame = false;
    bool GameOver = false;
    bool resumed;
    void Start()
    {
        
    }
    public void WinScene()
    {
        GameOver = true;
        Time.timeScale = 0;
        FindObjectOfType<CannonControl>().CanShoot = false;
        win.SetActive(true);
    }
    public void LoseScene()
    {
        GameOver = true;
        Time.timeScale = 0;
        FindObjectOfType<CannonControl>().CanShoot = false;
        lose.SetActive(true);
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.3f);
        endGame = true;
    }
    public void clearNormal()
    {
        foreach (Pegs p in obstaclePegs)
        {
            if (p!=null)
            p.ClearNormal();
        }
    }
    void Update()
    {
        TimeScale.text = "TimeScale: " + Time.timeScale;
        ScoreTxt.text = "Score: " + score;
        HpTxt.text = "HP: " + Hp;
        AtkTxt.text = "ATK: " + Attack;
        if (!GameOver)
        {
            if (Hp <= 0)
            {
                LoseScene();
            }
            if (goal == 1 && ball)
            {
                StartCoroutine("EndGame");
            }
            if (endGame && ball) 
            {
                if (ball.willHit())
                {
                    Time.timeScale = 0.35f;
                    resumed = false;
                }
                else if (!resumed)
                {
                    Time.timeScale = 1;
                    resumed = true;
                }
            }
            if (goal <= 0)
            {
                Time.timeScale = 1;
                //结束时摧毁全部obs
                Destroy(Obstacles);
                // 
                Destroy(Sticks);
                Rewards.SetActive(true);
            }
        }
    }
}
