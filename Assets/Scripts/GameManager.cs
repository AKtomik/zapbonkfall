using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Tank> tanks;
    private List<Tank> aliveTanks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        OnMapLoad(); //temporary to test
    }

    public void OnMapLoad()
    {
        aliveTanks = tanks;
    }

    public void PlayerDied(GameObject deadTank)
    {
        Tank scriptDeadTank = deadTank.GetComponent<Tank>();
        tanks.Remove(scriptDeadTank);
        if(tanks.Count == 1)
        {
            EndOfGame(tanks[0]);
        }
    }

    public void EndOfGame(Tank winningTank)
    {
        winningTank.roundWon++;
        Debug.Log(winningTank.player + "win, score : " + winningTank.roundWon);
    }
}
