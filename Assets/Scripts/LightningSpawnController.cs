using UnityEngine;

public class LightningSpawnController : MonoBehaviour
{
    public Transform[] listSpawnerPos;
    public GameObject lightningPrefab;

    float timer = 0;
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
            Invoke("LightningSpawn", 0);
            timer = 0;
        }
    }

    void LightningSpawn()
    {
        int index = Random.Range(0, listSpawnerPos.Length - 1);

        var newLightning = PoolManager.SpawnObject(lightningPrefab, listSpawnerPos[index].position, Quaternion.identity).GetComponent<LightningBehaviour>();
        newLightning.Setup();
    }
}