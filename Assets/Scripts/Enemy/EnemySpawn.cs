using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Setup Spawn Enemy")]
    [SerializeField] private Vector3Int _sizeMap;
    [SerializeField] private int _countEnemyPerChunk;
    [SerializeField] private int _sizeChunk;

    [Space]
    [SerializeField] private GameObject[] _enemyType;
    [SerializeField] private float heightSpawnEnemy = 2f;
    private void Awake()
    {
        GenerateRandomPositionInChunk();
    }
    private void GenerateRandomPositionInChunk()
    {
        for (int i = -_sizeMap.x; i < _sizeMap.x; i += _sizeChunk)
        {
            for (int j = -_sizeMap.z; j < _sizeMap.z; j += _sizeChunk)
            {
                int minX = i;
                int maxX = i + _sizeChunk;
                int minZ = j;
                int maxZ = j + _sizeChunk;
                int x = Random.Range(minX, maxX);
                int z = Random.Range(minZ, maxZ);

                GenerationRandomEnemy(x, z);
            }
        }
    }

    private void GenerationRandomEnemy(int x, int z)
    {
        int randomID = Random.Range(0, _enemyType.Length - 1);

        float randomRotationAngle = Random.Range(0, 360);
        Quaternion randomRotation = Quaternion.Euler(0f, randomRotationAngle, 0f);

        Instantiate(_enemyType[randomID], new Vector3(transform.position.x + x, heightSpawnEnemy, transform.position.z + z), randomRotation);
    }
    //area spawn
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x + -_sizeMap.x, heightSpawnEnemy, transform.position.z + _sizeMap.z), new Vector3(transform.position.x + _sizeMap.x, heightSpawnEnemy, transform.position.z + _sizeMap.z));
        Gizmos.DrawLine(new Vector3(transform.position.x + _sizeMap.x, heightSpawnEnemy, transform.position.z + _sizeMap.z), new Vector3(transform.position.x + _sizeMap.x, heightSpawnEnemy, transform.position.z + -_sizeMap.z / 2));
        Gizmos.DrawLine(new Vector3(transform.position.x + _sizeMap.x, heightSpawnEnemy, transform.position.z + -_sizeMap.z), new Vector3(transform.position.x + -_sizeMap.x, heightSpawnEnemy, transform.position.z + -_sizeMap.z));
        Gizmos.DrawLine(new Vector3(transform.position.x + -_sizeMap.x, heightSpawnEnemy, transform.position.z + -_sizeMap.z), new Vector3(transform.position.x + -_sizeMap.x, heightSpawnEnemy, transform.position.z + _sizeMap.z));
    }
}
