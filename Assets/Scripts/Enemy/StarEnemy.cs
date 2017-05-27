using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StarEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health;
    private int storeHealth;

    Rigidbody2D body;
    float timeAlive = 20;
    int score = 100;

    Plane[] planes;
    Collider2D objCollider;

    public GameObject blackHole;
    public GameObject explosionPrefab;
    public GameObject[] listBonousPrefab;
     

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
        score = GameManager.Instance.scoreStarEnemy;
        storeHealth = health;
    }

    void Start()
    {
        Invoke("ReturnPool", timeAlive);
    }

    void Update()
    {
        Fly();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Earth")
        {
            var blackH = PoolManager.SpawnObject(blackHole, transform.position, Quaternion.identity);

            blackH.tag = "BlackHoleKillKing";

            blackH.transform.DOScale(Vector3.zero, 0);
            blackH.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 3);
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.blackHoleAppearLarge);

            ReturnPool();
        }

        if (collision.tag == "Bullet")
        {
            OnEnemyHit(PlayerManager.Instance.damageGun);
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
        GameManager.Instance.ScoreAdd(score);

        var explosionEffect = PoolManager.SpawnObject(explosionPrefab, transform.position, Quaternion.identity);
        PoolManager.Instance.StartCoroutine(ReleaseExplosionPrefab(explosionEffect));
        ReturnPool();

        float ran = Random.Range(0.3f, listBonousPrefab.Length - 0.1f);

        var bonus = PoolManager.SpawnObject(listBonousPrefab[(int)ran], transform.position, Quaternion.identity).GetComponent<BonusBehaviour>();
        bonus.Setup((int)ran);
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
        health = storeHealth;
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
}
