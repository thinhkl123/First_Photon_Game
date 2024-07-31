using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Score
    {
        get
        {
            return score;
        }
    }

    public float Health
    {
        get 
        { 
            return health; 
        }
    }

    private int score;
    private float health;

    void Start()
    {
        score = 0;
        health = 1;
    }

    public void UpdateScore(int idx)
    {
        score += idx;
    }

    public void UpdateHealth(float idx)
    {
        health += idx;
    }
}
