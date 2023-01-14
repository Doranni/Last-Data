using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPool meteoritesPool;
    [SerializeField] private float meteoriteSpawnDelay_min = 1, meteoriteSpawnDelay_max = 8;
    [SerializeField]
    [Range(0,1)]
    private float InDetectRangeFraction = 0.2f;
    [SerializeField] private uint firstMeteoritesAmount = 50;

    private void Start()
    {
        for (int i = 0; i < firstMeteoritesAmount; i++)
        {
            SpawnMeteorite(true);
        }
        InvokeRepeating("SpawnMeteorite", 
            Random.Range(meteoriteSpawnDelay_min, meteoriteSpawnDelay_max) / GameManager.Instance.Speed,
            Random.Range(meteoriteSpawnDelay_min, meteoriteSpawnDelay_max) / GameManager.Instance.Speed);
    }

    private void SpawnMeteorite() => SpawnMeteorite(false);

    private void SpawnMeteorite(bool firstSpawn)
    {
        var ast = meteoritesPool.GetPooledObject();
        float rangeResult = Random.value;
        if (rangeResult > InDetectRangeFraction)
        {
            ast.gameObject.transform.position = RandomPosition(false, firstSpawn);
        }
        else
        {
            ast.gameObject.transform.position = RandomPosition(true, firstSpawn);
        }
        ast.GetComponent<MeteoriteController>().Respawn();
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

                    Random.Range(GameManager.Instance.ObjDetectRange_min.y, 
                    GameManager.Instance.ObjDetectRange_max.y),

                    Random.Range(GameManager.Instance.MeteoriteSpawnRange_min.z, 
                    GameManager.Instance.MeteoriteSpawnRange_max.z));
            }
            else
            {
                return new Vector3(Random.Range(GameManager.Instance.MeteoriteSpawnRange_min.x, 
                    GameManager.Instance.MeteoriteSpawnRange_max.x),

                    Random.Range(GameManager.Instance.MeteoriteSpawnRange_min.y, 
                    GameManager.Instance.MeteoriteSpawnRange_max.y),

                    Random.Range(GameManager.Instance.MeteoriteSpawnRange_min.z, 
                    GameManager.Instance.MeteoriteSpawnRange_max.z));
            }
        }
        else
        {
            if (inDetectRange)
            {
                return new Vector3(Random.Range(GameManager.Instance.ObjDetectRange_min.x, 

                    GameManager.Instance.ObjDetectRange_max.x),

                    Random.Range(GameManager.Instance.ObjDetectRange_min.y, GameManager.Instance.ObjDetectRange_max.y),
                    GameManager.Instance.MeteoriteSpawnRange_max.z);
            }
            else
            {
                return new Vector3(Random.Range(GameManager.Instance.MeteoriteSpawnRange_min.x, 
                    GameManager.Instance.MeteoriteSpawnRange_max.x),

                    Random.Range(GameManager.Instance.MeteoriteSpawnRange_min.y, 
                    GameManager.Instance.MeteoriteSpawnRange_max.y),

                    GameManager.Instance.MeteoriteSpawnRange_max.z);
            }
        }
    }
}
