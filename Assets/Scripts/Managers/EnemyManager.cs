using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    public int spawnCount = 10;

    private Queue<GameObject> enemyPrefabPool = new Queue<GameObject>();



    void Start ()
    {
        EnemyHealth enemyPrefabHealthComponent = enemy.GetComponent<EnemyHealth>();

        for(int i = 0; i < spawnCount; i++)
        {
            EnemyHealth pooledEnemy = Instantiate<EnemyHealth>(enemyPrefabHealthComponent);
            pooledEnemy.enemyManagerPool = this;
            enemyPrefabPool.Enqueue(pooledEnemy.gameObject);
            pooledEnemy.gameObject.SetActive(false);
        }


        InvokeRepeating (nameof(Spawn), spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if (playerHealth.currentHealth <= 0f) return;

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        if(enemyPrefabPool.Count > 0)
        {
            GameObject spawnedEnemy = enemyPrefabPool.Dequeue();
            spawnedEnemy.SetActive(true);
            enemy.transform.position = spawnPoints[spawnPointIndex].position;
            enemy.transform.rotation = spawnPoints[spawnPointIndex].rotation;
        }

    }


    public void ReAddToQueue(GameObject enemyPrefab)
    {
        enemyPrefabPool.Enqueue(enemyPrefab);
    }

}
