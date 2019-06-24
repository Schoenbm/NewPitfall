using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnding : MonoBehaviour
{
    public float aFadeDuration = 1f;
    public float aDisplayImageDuration = 1f;
    
    public CanvasGroup aMageWin;
    public CanvasGroup aKnightWin;

    public int p_WinMaxCount;
    public GameObject aWallsPrefab;

    private Vector3 wallPos;
    private Quaternion wallRot;
    private PlayerManager aPlayerManager;
    private Respawn aRespawn;
    private float aTimer = 0;
    private GameObject aWalls;
    private bool aEndGame = false;
    private bool mageWinBool = false;

    public void setMageWinBool(bool pBool)
    {
        mageWinBool = pBool;
    }

    void Start()
    {
        aWalls = GameObject.Find("/Map/Walls");
        

        GameObject vRespawn = GameObject.Find("Respawn");
        aRespawn = vRespawn.GetComponent<Respawn>();
        aRespawn.setRespawnPoints();

        GameObject vPlayerManagerGameObject = GameObject.Find("PlayerManager");

        if (vPlayerManagerGameObject != null)
        {
            aPlayerManager = vPlayerManagerGameObject.GetComponent<PlayerManager>();
            aPlayerManager.setGameEnding(this);
        }


    }

    public void winnerFound(int pWinnerNumber)
    {
        aPlayerManager.addPoint(pWinnerNumber);
        int vScore = aPlayerManager.getScore(pWinnerNumber);

        if (vScore == p_WinMaxCount)
        {
            aEndGame = true;

        }
        else
        {
            ResetArena();
        }
    }

    private void Update()
    {
        if (aEndGame)
        {
            if (mageWinBool)
            {
                Debug.Log("Mage wins");
                EndGame(aMageWin, true);
            }
            else
            {
                EndGame(aKnightWin, true);
            }
        }
    }


    public void ResetArena()
    {
        aPlayerManager.CreatePlayers();
        wallPos = aWalls.transform.position;
        wallRot = aWalls.transform.rotation;
        Destroy(aWalls);
        aWalls = Instantiate(aWallsPrefab, wallPos, wallRot);
    }

    void EndGame(CanvasGroup pImageCanvasGroup, bool pDoRestart)
    {
        Debug.Log("EndGame");
        aTimer += Time.deltaTime;
        pImageCanvasGroup.alpha = aTimer / aFadeDuration;
        if (aTimer > aFadeDuration + aDisplayImageDuration)
        {
            if (pDoRestart)
            {
                Destroy(aPlayerManager.gameObject);
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("shouldquit");
                Application.Quit();
            }
        }
    }
}

