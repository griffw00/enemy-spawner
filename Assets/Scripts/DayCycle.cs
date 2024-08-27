using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public enum TimeOfDay { Morning, Afternoon, Night }
    public TimeOfDay currentTime;
    [SerializeField] private Material brownMaterial; 

    private void Awake()
    {
        // Randomly select a time of day at runtime
        currentTime = (TimeOfDay)Random.Range(0, 3);
        Debug.Log("Current time of day is: " + currentTime);
    }

    // EFFECTS: Returns a list of modified GameObjects according to the specified DayCycle
    public List<GameObject> GetModifiedPrefabs(GameObject[] enemyPrefabs)
    {
        List<GameObject> modifiedPrefabs = new List<GameObject>();

        foreach (GameObject prefab in enemyPrefabs)
        {
            // Instantiate a copy so that the original is not modified
            GameObject modifiedPrefab = Instantiate(prefab); 
            Enemy enemy = modifiedPrefab.GetComponent<Enemy>();

            if (enemy != null)
            {
                ModifyAttributesForEnemy(enemy);
                modifiedPrefabs.Add(modifiedPrefab);
            }
        }
        return modifiedPrefabs;
    }
    
    // EFFECTS: Modifies the attributes for a given Enemy depending on the DayCycle
    private void ModifyAttributesForEnemy(Enemy enemy)
    {
        switch (currentTime)
        {
            case TimeOfDay.Morning:
                if (enemy.ClassName == "Archer")
                    enemy.SpawnRate = ModifySpawnRate(enemy.SpawnRate, 0.2f, 0.4f);
                
                if (enemy.GetMaterial().GetColor("_Color") == brownMaterial.GetColor("_Color"))
                    enemy.SpawnRate = ModifySpawnRate(enemy.SpawnRate, -0.1f, -0.3f); 
                break;

            case TimeOfDay.Afternoon:
                if (enemy.ClassName == "Assassin")
                    enemy.CanSpawn = false;
                else if (enemy.ClassName == "Grunt")
                    enemy.AttackPower += 1;
                else
                    enemy.SpawnRate = ModifySpawnRate(enemy.SpawnRate, -0.2f, 0.2f); 
                break;

            case TimeOfDay.Night:
                if (enemy.ClassName == "Assassin")
                    enemy.Speed += Random.Range(0f, 2f); 
                break;
        }
    }

    // EFFECTS: Returns a modified spawnRate if new spawnRate > 0, else returns 0
    // I am assuming that the spawnRate cannot be negative 
    private float ModifySpawnRate(float spawnRate, float min, float max) {
        float newRate = spawnRate += Random.Range(min, max); 
        return newRate > 0 ? newRate : 0; 
    }
}
