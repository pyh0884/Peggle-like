using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrap : MonoBehaviour
{
    public int Damage = 5;
    GamePlayManager gpm;
    // Start is called before the first frame update
    void Start()
    {
        gpm = FindObjectOfType<GamePlayManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gpm.Hp -= Damage;
            Destroy(collision.gameObject);
            gpm.clearNormal();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
