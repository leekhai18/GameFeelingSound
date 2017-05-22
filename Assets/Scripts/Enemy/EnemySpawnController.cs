using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public Transform[] listSpawnerPos;
    public GameObject triangleEnemyPrefab;
    public GameObject polygonEnemyPrefab;
    public GameObject starEnemyPrefab;

    float timer;
    [SerializeField] int polygonPart = 1;
    [SerializeField] int trianglePart = 7;
    [SerializeField] int starPart = 1;

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
            int randomEnemySpawn = Random.Range(0, polygonPart + trianglePart + starPart - 1);

            if (randomEnemySpawn < polygonPart)
                Invoke("PolygonEnemySpawn", 0);
            else if (polygonPart <= randomEnemySpawn && randomEnemySpawn < (polygonPart + starPart))
                Invoke("StarEnemySpawn", 0);
            else
                Invoke("TriangleEnemySpawn", 0);

            timer = 0;
        }
    }

    void TriangleEnemySpawn()
    {
        var newTriangleEnemy = PoolManager.SpawnObject(triangleEnemyPrefab, listSpawnerPos[Random.Range(2, 3)].position, Quaternion.identity).GetComponent<TriangleEnemy>();
        newTriangleEnemy.Setup();
    }

    void PolygonEnemySpawn()
    {
        var newPolygonEnemy = PoolManager.SpawnObject(polygonEnemyPrefab, listSpawnerPos[0].position, Quaternion.identity).GetComponent<PolygonEnemy>();
        newPolygonEnemy.Setup();
    }

    void StarEnemySpawn()
    {
        var newStarEnemy = PoolManager.SpawnObject(starEnemyPrefab, listSpawnerPos[1].position, Quaternion.identity).GetComponent<StarEnemy>();
        newStarEnemy.RotateToTarget(new Vector3(14, 7, 0));
    }
}