using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int p_player1WinsNumber = 0;
    private int p_player2WinsNumber = 0;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject temp = GameObject.Find("LevelManager");
        if (!(temp == null))
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public int addWin(string pPlayer, int pNumber)
    {
        Debug.Log("It's in the function !");
        if (pPlayer == "Player1")
        {
            p_player1WinsNumber = p_player1WinsNumber + pNumber;
            return (p_player1WinsNumber);
        }
        if (pPlayer == "Player2")
        {
            p_player2WinsNumber = p_player2WinsNumber + pNumber;
            return (p_player2WinsNumber);
        }
        return -1;
    }

   
}
