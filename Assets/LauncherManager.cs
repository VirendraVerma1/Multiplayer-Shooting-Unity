using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LauncherManager : MonoBehaviourPunCallbacks
{

    public InputField playername;
    public GameObject EnterGamePannel;
    public GameObject ConnectingGamePannel;
    public GameObject JoinGamePannel;

    //for players variable
    public GameObject PlayerPrefab;
    public Transform[] spawnPointsBlue;
    public Transform[] spawnPointsRed;
    public GameObject spawnpoint1;
    public GameObject spawnpoint2;
    public GameObject spawnpoint3;

    private PhotonView photonView;

    GameObject go;
    public static int playerIndex;
    public static int myplayerIndex;

    void Start()
    {
        playerIndex = 1;
      

        photonView = GetComponent<PhotonView>();
        EnterGamePannel.SetActive(true);
        ConnectingGamePannel.SetActive(false);
        JoinGamePannel.SetActive(false);
    }

    public void SetPlayerName()
    {
        PhotonNetwork.NickName = playername.text;
        EnterGamePannel.SetActive(false);
        ConnectingGamePannel.SetActive(true);
        JoinGamePannel.SetActive(false);
        ConnectToPhotonServer();
    }

    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to photon server");
        EnterGamePannel.SetActive(false);
        ConnectingGamePannel.SetActive(false);
        JoinGamePannel.SetActive(true);
        
    }

    public void JoinRandomGameButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnected()
    {
        print("Connected to internet");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        CreateAndJoinRoom();
    }

   

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        print(newPlayer.NickName + " joined " + PhotonNetwork.CurrentRoom.Name);
    }

    void CreateAndJoinRoom()
    {
        string randomRoomName = "R" + Random.Range(0, 1000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    public void CancelButton()
    {
        PhotonNetwork.LeaveLobby();
        EnterGamePannel.SetActive(true);
        ConnectingGamePannel.SetActive(false);
        JoinGamePannel.SetActive(false);
    }

    public static void UpdateTeams()
    {
        if (playerIndex == 1)
        {
            playerIndex = 2;
            
        }
        else if (playerIndex == 2)
        {
            playerIndex = 1;
            
        }
    }


    public override void OnJoinedRoom()
    {

        if (playerIndex == 1)
        {
            
            go = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnpoint1.transform.position, spawnpoint1.transform.rotation, 0);
        }
        else if (playerIndex == 2)
        {
           
            go = PhotonNetwork.Instantiate(PlayerPrefab.name, spawnpoint2.transform.position, spawnpoint2.transform.rotation, 0);
        }


        
        print(PhotonNetwork.NickName + " joined " + PhotonNetwork.CurrentRoom.Name);
        EnterGamePannel.SetActive(false);
        ConnectingGamePannel.SetActive(false);
        JoinGamePannel.SetActive(false);



        /*
        if (PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count >= PunTeams.PlayersPerTeam[PunTeams.Team.red].Count)
        {
         GameObject player = PhotonNetwork.Instantiate("PlayerRed", spawnPointsRed[PunTeams.PlayersPerTeam[PunTeams.Team.red].Count].position, spawnPointsRed[PunTeams.PlayersPerTeam[PunTeams.Team.red].Count].rotation, 0);
         PhotonNetwork.player.SetTeam(PunTeams.Team.red);
         }
     else if (PunTeams.PlayersPerTeam[PunTeams.Team.red].Count >= PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count)
        {
         GameObject player = PhotonNetwork.Instantiate("PlayerBlue", spawnPointsBlue[PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count].position, spawnPointsBlue[PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count].rotation, 0);
         PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
         }
     else if (PunTeams.PlayersPerTeam[PunTeams.Team.red].Count == PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count)
        {
         GameObject player = PhotonNetwork.Instantiate("PlayerRed", spawnPointsRed[PunTeams.PlayersPerTeam[PunTeams.Team.red].Count].position, spawnPointsRed[PunTeams.PlayersPerTeam[PunTeams.Team.red].Count].rotation, 0);
         PhotonNetwork.player.SetTeam(PunTeams.Team.red);
        }*/
    }
}

