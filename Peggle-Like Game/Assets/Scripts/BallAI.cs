using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAI : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10;
    float vy;
    float vx;
    float angle;
    float timer;
    public Transform RaycastPoint;
    public LayerMask EnemyLayer;
    public float willHitDistance = 1.5f;
    int atk;
    public GameObject Arrow;
    public bool showArrow;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        atk = FindObjectOfType<GamePlayManager>().Attack;
    }
    public void RotateArrow(Vector2 dir)
    {
        if (dir.x >= 0)
        {
            Arrow.transform.rotation = Quaternion.Euler(180, 0, Vector2.Angle(Vector2.up, dir));
        }
        else
        {
            Arrow.transform.rotation = Quaternion.Euler(180, 0, -Vector2.Angle(Vector2.up, dir));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//针对所有碰撞
    {
        angle = Vector3.Angle(rb.velocity, collision.contacts[0].normal);
        vy = Mathf.Cos(angle) * rb.velocity.magnitude;
        rb.velocity = (collision.contacts[0].normal.normalized * vy * 2 + rb.velocity).normalized * speed;
        //是否在击中小球时施加向上的速度
        //if (collision.gameObject.tag == "Obstacle")
        //{
        //    Debug.Log("Up");
        //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + 2);
        //}
    }
    private void OnCollisionStay2D(Collision2D collision)//避免卡住
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            Destroy(collision.gameObject, 0.1f);
        }
    }
    void Update()
    {if (Arrow)
        Arrow.gameObject.SetActive(showArrow);
    }
    public bool willHit()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, willHitDistance, EnemyLayer);
        if (!col)
            return false;
        else return col.GetComponent<Pegs>().HP - atk <= 0;
    }
    public void Jump(Vector2 dir)
    {
        rb.velocity = rb.velocity / 3;
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
    }
    private void OnDestroy()
    {
        FindObjectOfType<CannonControl>().CanShoot = true;
    }
}
