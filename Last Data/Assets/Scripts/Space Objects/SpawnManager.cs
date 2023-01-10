using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPool asteroidPool;
    [SerializeField] private float astSpawnDelay_min, astSpawnDelay_max;
    [SerializeField] private float InDetectRangeFraction;
    [SerializeField] private uint firstAsteroidsAmount;

    private void Start()
    {
        for (int i = 0; i < firstAsteroidsAmount; i++)
        {
            SpawnAsteroid(true);
        }
        InvokeRepeating("SpawnAsteroid", 
            Random.Range(astSpawnDelay_min, astSpawnDelay_max) / GameManager.Instance.Speed,
            Random.Range(astSpawnDelay_min, astSpawnDelay_max) / GameManager.Instance.Speed);
    }

    private void SpawnAsteroid() => SpawnAsteroid(false);

    private void SpawnAsteroid(bool firstSpawn)
    {
        var ast = asteroidPool.GetPooledObject();
        float rangeResult = Random.value;
        if (rangeResult > InDetectRangeFraction)
        {
            ast.gameObject.transform.position = RandomPosition(false, firstSpawn);
        }
        else
        {
            ast.gameObject.transform.position = RandomPosition(true, firstSpawn);
        }
        ast.GetComponent<AsteroidController>().Respawn();
        ast.GetComponent<ShowDistance>().ClearOutlining();
    }

    private Vector3 RandomPosition(bool inDetectRange, bool firstSpawn)
    {
        if (firstSpawn)
        {
            if (inDetectRange)
            {
                return new Vector3(Random.Range(GameManager.Instance.ObjDetectRange_min.x, 
                    GameManager.Instance.ObjDetectRange_max.x),
                    Random.Range(GameManager.Instance.ObjDetectRange_min.y, GameManager.Instance.ObjDetectRange_max.y),
                    Random.Range(GameManager.Instance.AstSpawnRange_min.z, GameManager.Instance.AstSpawnRange_max.z));
            }
            else
            {
                return new Vector3(Random.Range(GameManager.Instance.AstSpawnRange_min.x, 
                    GameManager.Instance.AstSpawnRange_max.x),
                    Random.Range(GameManager.Instance.AstSpawnRange_min.y, GameManager.Instance.AstSpawnRange_max.y),
                    Random.Range(GameManager.Instance.AstSpawnRange_min.z, GameManager.Instance.AstSpawnRange_max.z));
            }
        }
        else
        {
            if (inDetectRange)
            {
                return new Vector3(Random.Range(GameManager.Instance.ObjDetectRange_min.x, 
                    GameManager.Instance.ObjDetectRange_max.x),
                    Random.Range(GameManager.Instance.ObjDetectRange_min.y, GameManager.Instance.ObjDetectRange_max.y),
                    GameManager.Instance.AstSpawnRange_max.z);
            }
            else
            {
                return new Vector3(Random.Range(GameManager.Instance.AstSpawnRange_min.x, 
                    GameManager.Instance.AstSpawnRange_max.x),
                    Random.Range(GameManager.Instance.AstSpawnRange_min.y, GameManager.Instance.AstSpawnRange_max.y),
                    GameManager.Instance.AstSpawnRange_max.z);
            }
        }
    }
}
