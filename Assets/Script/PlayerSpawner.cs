using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float minX, maxX, minY, maxY;

    private void Start()
    {
        //player = Resources.Load<GameObject>(Constance.PLAYERCHARACTER);
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(player.name, randomPos, Quaternion.identity);
    }
}
