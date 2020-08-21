using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public bool TouchStart = false;
    private Vector2 StartPoint;
    private Vector2 EndPoint;
    [HideInInspector] public GameObject LeftCircle;
    [HideInInspector] public GameObject RightCircle;
    [HideInInspector] public GameObject InnerCircle;
    [HideInInspector] public GameObject Skill1;
    [HideInInspector] public GameObject Skill2;
    public Sprite[] spr;
    private Vector3 mousePos;
    private int SkillNum;//0=null,1=left,2=right
    public BallAI ball;
    bool resumed;
    public GameObject illusion;
    void DoubleJump(Vector2 direction)
    {
        ball.Jump(direction);
    }
    void Illusion()
    {
        var bal=Instantiate(illusion, ball.transform.position, Quaternion.identity);
        bal.GetComponent<Rigidbody2D>().velocity = new Vector2(-ball.GetComponent<Rigidbody2D>().velocity.x, ball.GetComponent<Rigidbody2D>().velocity.y);
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        #region Joystick Position
        if (Input.GetMouseButtonDown(0) && ball && mousePos.x > 1.5f && mousePos.x < 15.2f && mousePos.y > 2.5f && mousePos.y < 25.6f) //可操纵范围内
        {
            transform.position= new Vector2(mousePos.x, mousePos.y);
            StartPoint = new Vector2(mousePos.x, mousePos.y);
            LeftCircle.transform.position = StartPoint;
            RightCircle.transform.position = StartPoint;
            InnerCircle.transform.position = StartPoint;
            LeftCircle.GetComponent<SpriteRenderer>().enabled = true;
            RightCircle.GetComponent<SpriteRenderer>().enabled = true;
            InnerCircle.GetComponent<SpriteRenderer>().enabled = true;
            Skill1.GetComponent<SpriteRenderer>().enabled = true;
            Skill2.GetComponent<SpriteRenderer>().enabled = true;
            TouchStart = true;
            Time.timeScale = 0.5f;
            resumed = false;
        }
        else if (Input.GetMouseButtonDown(0) && ball && mousePos.x > 0 && mousePos.x < 17 && mousePos.y > 0 && mousePos.y < 27.5f)//可操纵范围外
        {
            StartPoint = new Vector2(Mathf.Clamp(mousePos.x, 1.5f, 15.2f), Mathf.Clamp(mousePos.y, 2.5f, 25.6f));
            LeftCircle.transform.position = StartPoint;
            RightCircle.transform.position = StartPoint;
            InnerCircle.transform.position = StartPoint;
            LeftCircle.GetComponent<SpriteRenderer>().enabled = true;
            RightCircle.GetComponent<SpriteRenderer>().enabled = true;
            InnerCircle.GetComponent<SpriteRenderer>().enabled = true;
            TouchStart = true;
            Time.timeScale = 0.5f;
            resumed = false;
        }
        if (Input.GetMouseButton(0) && TouchStart && ball)
        {
            EndPoint = new Vector2(mousePos.x, mousePos.y);
            Time.timeScale = 0.5f;
        }
        else if (!resumed)
        {            
            Time.timeScale = 1;
            resumed = true;
        }
        if (!ball)
        {
            TouchStart = false;
        }
        #endregion
        if (TouchStart)
        {
            Vector2 offset = StartPoint - EndPoint;
            if (offset.magnitude > 0.3f && offset.magnitude < 1)  //当处于中轮盘时选择技能
            {
                if (offset.x <= 0)//右边技能
                {
                    SkillNum = 2;
                    LeftCircle.GetComponent<SpriteRenderer>().sprite = spr[0];
                    RightCircle.GetComponent<SpriteRenderer>().sprite = spr[3];
                }
                else
                {
                    SkillNum = 1;//左边技能
                    LeftCircle.GetComponent<SpriteRenderer>().sprite = spr[1];
                    RightCircle.GetComponent<SpriteRenderer>().sprite = spr[2];
                }
            }
            else if (offset.magnitude < 0.3f) //当处于内轮盘时取消选择
            {
                SkillNum = 0;
                LeftCircle.GetComponent<SpriteRenderer>().sprite = spr[0];
                RightCircle.GetComponent<SpriteRenderer>().sprite = spr[2];
            }
            Vector2 dir = Vector2.ClampMagnitude(offset, 2.5f);
            if (ball && offset.magnitude >= 1 && SkillNum == 2)
            {
                ball.showArrow = true;
                ball.RotateArrow(dir);
            }
            InnerCircle.transform.position = new Vector2(StartPoint.x - dir.x, StartPoint.y - dir.y);
            if (offset.magnitude > 1 && Input.GetMouseButtonUp(0))
            {
                if (SkillNum == 1)//左边技能
                {
                    Illusion();
                }
                else if (SkillNum == 2)//右边技能
                {
                    DoubleJump(-dir);
                    ball.showArrow = false;
                }
                TouchStart = false;
            }
        }
        else
        {
            InnerCircle.GetComponent<SpriteRenderer>().enabled = false;
            LeftCircle.GetComponent<SpriteRenderer>().enabled = false;
            RightCircle.GetComponent<SpriteRenderer>().enabled = false;
            Skill1.GetComponent<SpriteRenderer>().enabled = false;
            Skill2.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
