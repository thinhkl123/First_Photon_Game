using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem deathVFX;
    [SerializeField] private PhotonView view;

    private PlayerController[] players;
    private PlayerController nearestPlayer;

    private void Start()
    {
        players = FindObjectsOfType<PlayerController>();
    }

    private void Update()
    {
        float distance1 = Vector2.Distance(transform.position, players[0].transform.position);
        float distance2 = Vector2.Distance(transform.position, players[1].transform.position);

        if (distance1 < distance2)
        {
            nearestPlayer = players[0];
        }
        else
        {
            nearestPlayer = players[1];
        }
        
        if (nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.CompareTag("Ray"))
            {
                UIManager.Ins.GetUI<PlayUI>().AddScore();
                view.RPC(nameof(SpawnParticle), RpcTarget.All);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC] void SpawnParticle()
    {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
    }
}
