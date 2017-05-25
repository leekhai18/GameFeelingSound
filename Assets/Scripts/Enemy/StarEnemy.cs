﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StarEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health;

    Rigidbody2D body;
    float timeAlive = 20;
    int score = 100;

    Plane[] planes;
    Collider2D objCollider;

    public GameObject blackHole;
    public GameObject explosionPrefab;
    public GameObject bonousPrefab;

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

        var bonus = PoolManager.SpawnObject(bonousPrefab, transform.position, Quaternion.identity).GetComponent<BonusBehaviour>();
        bonus.Setup();
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
