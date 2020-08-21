using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    public Transform EmitPoint;
    public GameObject Bullet;
    public float RotationNum; //-80~80
    public int ShootingSpeed;
    public float RotationSpeed;
    public bool CanShoot;
    LineRenderer lr;
    public float VelocityX;
    public float VelocityY;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        RotationNum = 0;
    }
    public void CannonTrajectory(float angle)
    {
        lr.enabled = true;
        lr.SetPosition(0, EmitPoint.position);
        if (angle < 0)
        {
            VelocityX = -ShootingSpeed * Mathf.Sin(-angle / 180 * Mathf.PI);
        }
        else
        {
            VelocityX = ShootingSpeed * Mathf.Sin(angle / 180 * Mathf.PI);
        }
        VelocityY = -ShootingSpeed * Mathf.Cos(angle / 180 * Mathf.PI);
        //VelocityZ = 1 / 1.2f + 10 * 1.2f;
        float i = 0.02f;
        while (i < 0.7f)
        {
            lr.positionCount = Mathf.RoundToInt(i / 0.05f) + 1;
            lr.SetPosition(Mathf.RoundToInt(i / 0.05f), new Vector3(EmitPoint.position.x + VelocityX * i, EmitPoint.position.y + (VelocityY * i + 0.5f * -9.8f * 0.75f * i * i), 0));
            i += 0.05f;
        }
    }

    void Update()
    {
        #region Rotation
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            RotationNum -= Time.deltaTime * RotationSpeed;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            RotationNum += Time.deltaTime * RotationSpeed;
        }
        RotationNum = Mathf.Clamp(RotationNum, -80, 80);
        transform.rotation = Quaternion.Euler(0, 0, RotationNum);
        #endregion
        #region Shoot
        if (Input.GetKeyDown(KeyCode.Space)&&CanShoot)
        {
            CanShoot = false;
            var bul = Instantiate(Bullet, EmitPoint.position, Quaternion.identity);
            bul.GetComponent<Rigidbody2D>().velocity = (EmitPoint.position - transform.position).normalized * ShootingSpeed;
            FindObjectOfType<GamePlayManager>().ball = bul.GetComponent<BallAI>();
            FindObjectOfType<JoyStick>().ball = bul.GetComponent<BallAI>();
        }
        #endregion
        #region Trajectory
        if (CanShoot)
        {
            CannonTrajectory(RotationNum);
        }
        else
        {
            lr.enabled = false;
        }
        #endregion
    }
}
