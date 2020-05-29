using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public Image img;
    public PhotonView photonView;
    public TextMeshProUGUI playerNameText;

    private int score = 0;
    public TextMeshProUGUI scoreNameText;
    private int myteam = 0;

    private bool team = false;
    // Start is called before the first frame update
    void Start()
    {
        team = false;
         score = 0;
        scoreNameText.text = score.ToString();
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine)
            photonView.RPC("RPC_GETteam", RpcTarget.MasterClient);
        playerNameText.text = photonView.Owner.NickName;
        
    }


    [PunRPC]
    void RPC_GETteam()
    {
       
        myteam = LauncherManager.playerIndex;
        LauncherManager.UpdateTeams();

        photonView.RPC("RPC_SetTeam", RpcTarget.OthersBuffered, myteam);
    }

    [PunRPC]
    void RPC_SetTeam(int whichTeam)
    {
        myteam = whichTeam;
        
    }

    void Update()
    {
        if (myteam == 1 && !team)
        {
            team = true;
            gameObject.tag = "Team1";
            print(gameObject.tag+"1");
            photonView.RPC("UpdateObjectToall", RpcTarget.OthersBuffered, "Team1");
        }
        if (myteam == 2&&!team)
        {
            team = true;
            gameObject.tag = "Team2";
            print(gameObject.tag + "2"); 
            photonView.RPC("UpdateObjectToall", RpcTarget.OthersBuffered, "Team2");
        }
    }

    [PunRPC]
    void UpdateObjectToall(string teamTag)
    {
        gameObject.tag = teamTag;
    }

    public void ScoreButton()
    {
        if(photonView.IsMine)
        photonView.RPC("UpdateScore", RpcTarget.All);
    }

    [PunRPC]
    public void UpdateScore()
    {
        score++;
        scoreNameText.text = score.ToString();
    }
}
