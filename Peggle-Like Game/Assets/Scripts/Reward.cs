using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public int reward;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Destroy(collision.gameObject);
            FindObjectOfType<GamePlayManager>().score += reward;
            FindObjectOfType<GamePlayManager>().WinScene();
        }
    }
}
