using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> enemtSpawnPosList;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeToSpawn;

    private float timeSpawn;

    private void Start()
    {
        timeSpawn = timeToSpawn;
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return;
        }

        if (timeSpawn <= 0f)
        {
            int ranIdx = Random.Range(0, enemtSpawnPosList.Count);
            PhotonNetwork.Instantiate(enemyPrefab.name, enemtSpawnPosList[ranIdx].position, Quaternion.identity);

            timeSpawn = timeToSpawn;
        }
        else
        {
            timeSpawn -= Time.deltaTime;
        }
    }
}
