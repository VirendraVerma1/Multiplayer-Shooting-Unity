using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerSetUp : MonoBehaviourPunCallbacks
{

    public GameObject[] FPS_Hands_ChildGameObjects;
    public GameObject[] Soilder_ChildGameObjects;

    public GameObject playerUIPrefab;
    private PlayerMovementController playermovementController;
    public Camera FPSCamera;
    private RigidbodyFirstPersonController rigidbodyFirstPersionControllerscript;
    private Animator animator;
    private Shooting shooter;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        shooter = GetComponent<Shooting>();
        rigidbodyFirstPersionControllerscript = GetComponent<RigidbodyFirstPersonController>();
        playermovementController = GetComponent<PlayerMovementController>();
        if (photonView.IsMine)
        {
            //activate FPS hands , Deactivate Soilder

            foreach (GameObject gameObject in FPS_Hands_ChildGameObjects)
            {
                gameObject.SetActive(true);
            }
            foreach (GameObject gameObject in Soilder_ChildGameObjects)
            {
                gameObject.SetActive(false);
            }

            GameObject playerUIGameObject = Instantiate(playerUIPrefab);
            playermovementController.joystick = playerUIGameObject.transform.Find("Fixed Joystick").GetComponent<Joystick>();
            playermovementController.fixedTouchField = playerUIGameObject.transform.Find("RotationTouchField").GetComponent<FixedTouchField>();

            playerUIGameObject.transform.Find("FireButton").GetComponent<Button>().onClick.AddListener(() => shooter.Fire());
            playerUIGameObject.transform.Find("JumpButton").GetComponent<Button>().onClick.AddListener(() => rigidbodyFirstPersionControllerscript.JumpButton());
            FPSCamera.enabled = true;

            animator.SetBool("IsSoilder", false);
        }
        else
        {
            foreach (GameObject gameObject in FPS_Hands_ChildGameObjects)
            {
                gameObject.SetActive(false);
            }
            foreach (GameObject gameObject in Soilder_ChildGameObjects)
            {
                gameObject.SetActive(true);
            }
            playermovementController.enabled=false;
            FPSCamera.enabled = false;
            GetComponent<RigidbodyFirstPersonController>().enabled = false;
            animator.SetBool("IsSoilder", true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
