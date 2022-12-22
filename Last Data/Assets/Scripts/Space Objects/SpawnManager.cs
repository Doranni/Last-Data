using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPool asteroidPool;
    [SerializeField] private float astSpawnDelay_min, astSpawnDelay_max;
    [SerializeField] private Vector3 astPosition_min, astPosition_max;

    private float asteroidTimeBeforeSpawn;

    private void Start()
    {
        asteroidTimeBeforeSpawn = Random.Range(astSpawnDelay_min, astSpawnDelay_max);
    }

    void Update()
    {
        asteroidTimeBeforeSpawn -= Time.deltaTime;
        if (asteroidTimeBeforeSpawn < 0)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        asteroidTimeBeforeSpawn = Random.Range(astSpawnDelay_min, astSpawnDelay_max);
        var ast = asteroidPool.GetPooledObject();
        ast.gameObject.transform.position = new Vector3(Random.Range(astPosition_min.x, astPosition_max.x),
            Random.Range(astPosition_min.y, astPosition_max.y), Random.Range(astPosition_min.z, astPosition_max.z));
    }
}
