using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Shooting : MonoBehaviourPunCallbacks
{

    public Camera FPS_Camera;
    public GameObject hitParticleEffect;

    public float startHealth=100;
    private float health;
    public Image healthBar;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        healthBar.fillAmount = health / startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        RaycastHit hit;
        Ray ray = FPS_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray,out hit,100))
        {
            print(hit.collider.gameObject.name);

            photonView.RPC("CreateHitEffect", RpcTarget.All, hit.point);

            if(hit.collider.gameObject.CompareTag("Player")&&!hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.AllBuffered,10f);
             }
        }
    }

    [PunRPC]
    public void TakeDamage(float damage,PhotonMessageInfo info)
    {
        health -= damage;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0f)
        {
            //Die();
            print(info.Sender.NickName + " killed" + info.photonView.Owner.NickName);
        }
    }

    [PunRPC]
    public void CreateHitEffect(Vector3 position)
    {
        GameObject hitEffect = Instantiate(hitParticleEffect, position, Quaternion.identity);
        Destroy(hitEffect, 0.05f);
    }


    void Die()
    {
        if (photonView.IsMine)
        {
            animator.SetBool("IsDead", true);
            StartCoroutine(Respawn());
        }

    }

    IEnumerator Respawn()
    {
        GameObject respawntext=GameObject.Find("RespawnText");
        float respawnTime = 8f;
        while (respawnTime > 0.0f)
        {
            yield return new WaitForSeconds(1f);
            respawnTime -= 1f;
            transform.GetComponent<PlayerMovementController>().enabled = false;
            respawntext.GetComponent<Text>().text = "You are killed. Respawning in " + respawnTime.ToString();

        }

        animator.SetBool("IsDead", false);
        respawntext.GetComponent<Text>().text = "";

        int randomPoint = Random.Range(-20, 20);
        transform.position = new Vector3(randomPoint, 0, randomPoint);
        transform.GetComponent<PlayerMovementController>().enabled = true;

        photonView.RPC("RegainHealth", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RegainHealth()
    {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
    }
}
