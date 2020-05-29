using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MobileManagerGameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            int randomPoint = Random.Range(-10, 10);
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPoint, 1, randomPoint), Quaternion.identity);
        }
        else
        {
            print("Place playerRefab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
