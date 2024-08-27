using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  
    private List<GameObject> modifiedPrefabs;  // Modified prefabs for spawning
    private DayCycle dayCycle; 

    private void Start()
    {
        dayCycle = FindObjectOfType<DayCycle>();

        // Get the modified prefabs for this spawn point
        modifiedPrefabs = dayCycle.GetModifiedPrefabs(enemyPrefabs);

        // Spawn enemies using the modified prefabs
        foreach (GameObject prefab in modifiedPrefabs)
        {
            StartCoroutine(SpawnEnemies(prefab));
        }

        // Clear the list after spawning to avoid retaining references
        modifiedPrefabs.Clear();
    }

    // EFFECTS: Spawns enemies with a random offset from the spawn point 
    // I am assuming that enemies with spawnRate == 0 do not spawn
    private IEnumerator SpawnEnemies(GameObject prefab)
    {
        Enemy enemy = prefab.GetComponent<Enemy>();
        int enemyCount = 0; 
        
        while (enemyCount < 50)
        {
            if (enemy != null && enemy.CanSpawn && enemy.SpawnRate > 0)
            {
                Instantiate(prefab, GetRandomSpawnPosition(), Quaternion.identity);

                float spawnDelay = enemy.SpawnRate < 0 ? 0 : 1 / enemy.SpawnRate; // Calculate spawn delay
                yield return new WaitForSeconds(spawnDelay);
            }

            enemyCount++;
        }
    }

    // EFFECTS: Gets a random offset from the spawn point
    private Vector3 GetRandomSpawnPosition()
    {
        float offsetX = Random.Range(-2.5f, 2.5f);
        float offsetZ = Random.Range(-2.5f, 2.5f);
        return new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z + offsetZ);
    }
}
