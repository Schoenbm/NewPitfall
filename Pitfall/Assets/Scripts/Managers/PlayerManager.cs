using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int aNumberPlayer;
    private Player[] aPlayers;

    public GameObject MagePrefab;
    public GameObject KnightPrefab;
    private Dictionary<string, GameObject> StringToPrefab;
    // only for caracter selection
    private string[] aPlayersCaractersName;
    //
    private GameEnding aGameEnding;
    private bool[] aPlayersAlive;
    private int[] aPlayersScore;
    private int aNumberPlayerWithName;
    private int aNumberPlayerAlive;
    private VirtualCam aVirtualCam;

    private Vector3[] aArenaRespawnPointsPosition = new Vector3[4];
    private Quaternion[] aArenaRespawnPointsRotation = new Quaternion[4];

    private void Start()
    {
        StringToPrefab = new Dictionary<string, GameObject>();
        StringToPrefab.Add("Mage", MagePrefab);
        StringToPrefab.Add("Knight", KnightPrefab);
        DontDestroyOnLoad(this.gameObject);
    }

    public void setPlayerManager(int n)
    {
        aNumberPlayer = n;
        aNumberPlayerAlive = n;
        aNumberPlayerWithName = 0;
        aPlayersCaractersName = new string[n];
        aPlayersScore = new int[n];
        aPlayersAlive = new bool[n];
        aPlayers = new Player[n];
    }

    //only for caracter selection
    public bool setNewPlayer(string pPlayerName)
    {
        aPlayersCaractersName[aNumberPlayerWithName] = pPlayerName;
        aNumberPlayerWithName++;

        if (aNumberPlayerWithName == aNumberPlayer)
        {
            return false;
        }
        return true;
    }

    //
    public void addPoint(int pPlayerNumber)
    {
        aPlayersScore[pPlayerNumber] ++;
    }

    public int getScore(int pPlayerNumber)
    {
        return aPlayersScore[pPlayerNumber];
    }

    public int getPlayersNumbers()
    {
        return (aNumberPlayer);
    }

    public void CreatePlayers()
    {
        aNumberPlayerAlive = aNumberPlayer;
        string vName;
        GameObject vPlayerGameObject;
        Player vPlayer;
        for ( int k = 0; k < aNumberPlayer; k++)
        {
            aPlayersAlive[k] = true;
            vName = aPlayersCaractersName[k];
            vPlayerGameObject = StringToPrefab[vName];
            
            vPlayerGameObject = Instantiate(vPlayerGameObject, aArenaRespawnPointsPosition[k], aArenaRespawnPointsRotation[k]);
            vPlayer = vPlayerGameObject.GetComponent<Player>();
            aPlayers[k] = vPlayer;
            vPlayer.setPlayer(k, this);
            aVirtualCam.setPlayer(vPlayer.transform, k);
        }

        while (aVirtualCam == null) { }

        for (int k = 0; k < aNumberPlayer; k++)
        {
            aVirtualCam.setPlayer(aPlayers[k].transform, k);
        }

    }

    public string getName(int n)
    {
        return aPlayersCaractersName[n];
    }

    public void playerDied(int n)
    {
        aNumberPlayerAlive--;
        Destroy(aPlayers[n].gameObject);
        aPlayersAlive[n] = false;
        
        if (aNumberPlayerAlive ==1)
        {
            int vWinner = this.findWinner();
            Destroy(aPlayers[vWinner].gameObject);
            aGameEnding.winnerFound(vWinner);
        }
    }

    public int findWinner()
    {
        for (int k = 0; k < aNumberPlayer; k++)
        {
            if (aPlayersAlive[k])
            {
                Debug.Log("i find a winner : " + k);
                return k;
            }
        }
        Debug.Log("ERROR, no players alive");
        return -1;
    }


    public void setGameEnding(GameEnding pGameEnding)
    {
        aGameEnding = pGameEnding;
        for (int k = 0; k < aNumberPlayer; k++)
        {
            aPlayersScore[k] = 0;
        }
        setVirtualCam();
        CreatePlayers();
    }

    public void setRespawnPoints(GameObject[] pRespawnPoints)
    {
        for (int k =0; k < pRespawnPoints.Length; k++)
        {
            aArenaRespawnPointsPosition[k] = pRespawnPoints[k].transform.position;
            aArenaRespawnPointsRotation[k] = pRespawnPoints[k].transform.rotation;
        }
    }

    public void setVirtualCam()
    {
        GameObject vPosCentrale = GameObject.Find("PositionCentrale");
        aVirtualCam = vPosCentrale.GetComponent<VirtualCam>();
        aVirtualCam.setNumberPlayers(aNumberPlayer);
    }
}