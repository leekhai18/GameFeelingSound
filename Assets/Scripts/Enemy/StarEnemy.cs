using UnityEngine;
using System.Collections;

public class StarEnemy : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    [Range(0.0f, 2.0f)]
    float timeShootDelay = 1;
    Rigidbody2D body;
    float timer;

    public GameObject bulletPrefab;
    public Transform[] gunPos;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Fly();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeShootDelay)
        {
            Fire();
            timer = 0;
        }
    }

    public void Fire()
    {
        for (int i = 0; i < gunPos.Length; i++)
        {
            var bulletScrip = PoolManager.SpawnObject(bulletPrefab, gunPos[i].transform.position, gunPos[i].transform.rotation).GetComponent<BulletBehaviour>();
            bulletScrip.Fire();
        }
    }

    public void Fly()
    {
        body.AddRelativeForce(Vector2.up * speed);
    }

    public void Finish()
    {
        PoolManager.ReleaseObject(this.gameObject);
        body.velocity = Vector3.zero;
    }
}
