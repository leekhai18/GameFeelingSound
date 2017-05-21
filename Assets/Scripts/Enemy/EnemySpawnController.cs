using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public Transform[] listSpawnerPos;
    public GameObject triangleEnemyPrefab;
    public GameObject polygonEnemyProfab;

    float timer;
    [SerializeField] int polygonPart = 1;
    [SerializeField] int trianglePart = 7;

    [SerializeField] float cooldownSpawn;
    [SerializeField] float cooldownSpawnIncree = 4;
    [SerializeField] float maxCooldownSpawn = 3;

    private float CooldownSpawnFollowMusic
    {
        get
        {
            cooldownSpawn = maxCooldownSpawn - AudioMeasure.Instance.RmsValue * cooldownSpawnIncree;
            return cooldownSpawn;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > CooldownSpawnFollowMusic)
        {
            int randomEnemySpawn = Random.Range(0, polygonPart + trianglePart - 1);

            if (randomEnemySpawn < polygonPart)
                Invoke("PolygonEnemySpawn", 0);
            else
                Invoke("TriangleEnemySpawn", 0);

            timer = 0;
        }
    }

    void TriangleEnemySpawn()
    {
        var newTriangleEnemy = PoolManager.SpawnObject(triangleEnemyPrefab, listSpawnerPos[Random.Range(1, 4)].position, Quaternion.identity).GetComponent<TriangleEnemy>();
        newTriangleEnemy.Setup();
    }

    void PolygonEnemySpawn()
    {
        var newPolygonEnemy = PoolManager.SpawnObject(polygonEnemyProfab, listSpawnerPos[0].position, Quaternion.identity).GetComponent<PolygonEnemy>();
        newPolygonEnemy.Setup();
    }
}