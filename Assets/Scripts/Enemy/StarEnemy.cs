using UnityEngine;
using System.Collections;

public class StarEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health;
    [SerializeField]
    [Range(0.0f, 2.0f)]
    float timeShootDelay = 1;
    Rigidbody2D body;
    float timer;
    float timeAlive = 20;

    Plane[] planes;
    Collider2D objCollider;

    public GameObject blackHole;
    GameObject blackH;
    bool isBlackHoleSpawned = false;
    float timeBLAppear = 0;
    public GameObject explosionPrefab;
    public GameObject bonousPrefab;
    public GameObject bulletPrefab;
    public Transform[] gunPos;

    bool IsVisible
    {
        get
        {
            return GeometryUtility.TestPlanesAABB(planes, objCollider.bounds);
        }
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        objCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        Invoke("ReturnPool", timeAlive);
    }

    void Update()
    {
        Fly();

        timer += Time.deltaTime;
        if (timer >= timeShootDelay)
        {
            Fire();
            timer = 0;
        }

        if (timeBLAppear < 1.5 && isBlackHoleSpawned == true)
        {
            timeBLAppear += Time.deltaTime;
            blackH.transform.localScale = new Vector3(timeBLAppear, timeBLAppear, timeBLAppear);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Earth")
        {
            ReturnPool();
            blackH = PoolManager.SpawnObject(blackHole, transform.position, Quaternion.identity);
            blackH.transform.localScale = Vector3.zero;
            isBlackHoleSpawned = true;
            timeBLAppear = 0.5f;
        }
        if (collision.tag == "Bullet")
        {
            OnEnemyHit(100);
            collision.GetComponent<BulletBehaviour>().OnBulletHit();
        }
    }

    void OnEnemyHit(int damage)
    {
        if (IsVisible == false)
            return;

        health -= damage;
        if (health <= 0)
            OnEnemyDie();
    }

    void OnEnemyDie()
    {
        var explosionEffect = PoolManager.SpawnObject(explosionPrefab, transform.position, Quaternion.identity);
        PoolManager.Instance.StartCoroutine(ReleaseExplosionPrefab(explosionEffect));
        ReturnPool();

        var bonous = PoolManager.SpawnObject(bonousPrefab, transform.position, Quaternion.identity).GetComponent<BonousBehaviour>();
        bonous.Setup();
    }

    IEnumerator ReleaseExplosionPrefab(GameObject explosionPre)
    {
        yield return new WaitForSeconds(2);
        PoolManager.ReleaseObject(explosionPre);
    }

    void ReturnPool()
    {
        PoolManager.ReleaseObject(this.gameObject);
        body.velocity = Vector2.zero;
    }

    public void Fire()
    {
        for (int i = 0; i < gunPos.Length; i++)
        {
            var bulletScrip = PoolManager.SpawnObject(bulletPrefab, gunPos[i].transform.position, gunPos[i].transform.rotation).GetComponent<BulletBehaviour>();
            bulletScrip.Fire();
            PoolManager.Instance.StartCoroutine(ReleaseBullet(bulletScrip));
        }
    }

    IEnumerator ReleaseBullet(BulletBehaviour bullet)
    {
        yield return new WaitForSeconds(0.5f);
        PoolManager.ReleaseObject(bullet.gameObject);
    }

    public void Fly()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    public void RotateToTarget(Vector3 target)
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    public void Finish()
    {
        PoolManager.ReleaseObject(this.gameObject);
        body.velocity = Vector3.zero;
    }
}
