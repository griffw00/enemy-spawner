using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Private fields   
    [SerializeField] private string className;
    [SerializeField] private float attackPower;
    [SerializeField] private float health;
    [SerializeField] private float speed; 
    [SerializeField] private float spawnRate;
    [SerializeField] private bool canSpawn;  

   // Properties
    public string ClassName
    {
        get { return className; }
        set { className = value; }
    }

    public float AttackPower
    {
        get { return attackPower; }
        set { attackPower = Mathf.Max(0, value); } // Attack is >= 0
    }

    public float Health
    {
        get { return health; }
        set { health = Mathf.Max(0, value); } // Health is >= 0
    }

    public float Speed
    {
        get { return speed; }
        set { speed = Mathf.Max(speed, value); } // Speed is >= 0
    }

    public float SpawnRate
    {
        get { return spawnRate; }
        set { spawnRate = value; }
    }

    public bool CanSpawn
    {
        get { return canSpawn; }
        set { canSpawn = value; }
    }

    // Property to get the material assigned to the enemy
    public Material GetMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        return renderer != null ? renderer.material : null;
    }
}
