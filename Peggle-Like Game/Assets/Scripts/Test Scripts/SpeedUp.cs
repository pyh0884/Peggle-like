using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public void speed()
    {
        Time.timeScale = 5;
    }
    public void resume()
    {
        Time.timeScale = 1;
    }
}
