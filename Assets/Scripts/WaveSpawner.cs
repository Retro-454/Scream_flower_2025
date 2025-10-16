using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnpoints; //where enemies can show
    public Wave[] waves; //how many waves

    private int currentWaveIndex = 0;
    private int enemiesAlive;
    public CameraController cameraController;
    private void Start()
    {
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            yield break;
        }
        if (cameraController != null)
        {
            cameraController.lockMovement = true;
            cameraController.lockX = Camera.main.transform.position.x;
        }
        Wave currentWave = waves[currentWaveIndex];
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            Transform spawn = spawnpoints[Random.Range(0, spawnpoints.Length)];
            GameObject enemy = Instantiate(currentWave.Enemy1, spawn.position, Quaternion.identity);

            enemy enemyScript = enemy.GetComponent<enemy>();
            if (enemyScript != null)
            {
                enemyScript.spawner = this;
            }
            enemiesAlive++;
            yield return new WaitForSeconds(currentWave.spawnDelay);

        }
    }
    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            if (cameraController != null)
            {
                cameraController.lockMovement = false;
            }
            currentWaveIndex++;
            StartCoroutine(SpawnWave());
        }
    }
}
