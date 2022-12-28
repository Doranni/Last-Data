using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPool asteroidPool;
    [SerializeField] private float astSpawnDelay_min, astSpawnDelay_max;
    [SerializeField] private Vector2 astSpawnRange_min, astSpawnRange_max;
    [SerializeField] private float astSpawnPosZ;

    private void Start()
    {
        InvokeRepeating("SpawnAsteroid", Random.Range(astSpawnDelay_min, astSpawnDelay_max),
            Random.Range(astSpawnDelay_min, astSpawnDelay_max));
    }

    private void SpawnAsteroid()
    {
        var ast = asteroidPool.GetPooledObject();
        ast.gameObject.transform.position = new Vector3(Random.Range(astSpawnRange_min.x, astSpawnRange_max.x),
            Random.Range(astSpawnRange_min.y, astSpawnRange_max.y), astSpawnPosZ);
        ast.GetComponent<AsteroidController>().Respawn();
        ast.GetComponent<ShowDistance>().ClearOutlining();
    }
}
