﻿using UnityEngine;
using System.Collections;

public class PolygonEnemy : EnemyBase
{
    [SerializeField] int level = 1;
    float timer;
    float timeAvailable = 100;

    public GameObject explosion;
    public GameObject blackHole;

    private bool isBlackHoleExisted = false;

    #region MonoBehaviour
    public override void Awake()
    {
        base.Awake();
        objCollider = GetComponentInChildren<Collider2D>();
        scoreBase = 5;
        isBlackHoleExisted = false;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            OnEnemyHit(100);
            collision.GetComponent<BulletBehaviour>().OnBulletHit();
        }
        else if (collision.tag == "Earth")
        {
            OnEnemyEscape();
            ReturnPool();
        }
        else if (collision.tag == "Player")
        {
        }
    }

    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if (timer > timeAvailable)
        {
            timer = 0;
            ReturnPool();
        }

        if (IsVisible == true && isBlackHoleExisted == false && level == 1)
        {
            var blackH = PoolManager.SpawnObject(blackHole, transform.position, Quaternion.identity);
            isBlackHoleExisted = true;
            PoolManager.Instance.StartCoroutine(ReleaseBlackHole(blackH));
        }
    }

    public override void UpdateMove()
    {
        base.UpdateMove();
    }
    #endregion

    #region Action
    public override void Setup()
    {
        health = 100;
        SetLevel(1);
        timer = 0;
        RotateToTarget();
    }

    public void RotateToTarget()
    {
        var randVec = Random.insideUnitSphere;
        randVec.z = 0;
        RotateToTarget(PlayerBehaviour.Instance.transform.position + randVec);
    }

    public override void ReturnPool()
    {
        base.ReturnPool();
        isBlackHoleExisted = false;
    }

    public void RotateRandom()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
    }

    public void SetLevel(int level)
    {
        this.level = level;
        transform.localScale = Vector3.one / level;
    }

    void ShowExplosionEffect()
    {
        var effect = PoolManager.SpawnObject(explosion, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        effect.Play();
        PoolManager.Instance.StartCoroutine(ReleaseParticleExplosion(effect));
    }
    #endregion

    #region Event
    public override void OnEnemyHit(int damage)
    {
        base.OnEnemyHit(damage);
    }

    public override void OnEnemyDie()
    {
        OnEnemyKilled();
        ShowExplosionEffect();
        int currentLevel = level + 1;
        if (currentLevel == 4)
        {
            ReturnPool();
            return;
        }

        SetLevel(currentLevel);
        var other = PoolManager.SpawnObject(this.gameObject, transform.position, Quaternion.identity).GetComponent<PolygonEnemy>();
        other.SetLevel(currentLevel);
        other.RotateRandom();
        other.velocity *= currentLevel;
        RotateRandom();
        timer = 0;
    }

    IEnumerator ReleaseParticleExplosion(ParticleSystem explosion)
    {
        yield return new WaitForSeconds(1);
        PoolManager.ReleaseObject(explosion.gameObject);
    }

    IEnumerator ReleaseBlackHole(GameObject blackH)
    {
        yield return new WaitForSeconds(2);
        PoolManager.ReleaseObject(blackH);
    }
    #endregion

    #region Utils
    public override void CalculateSpeed()
    {
        base.CalculateSpeed();
        speed *= 0.5f;
    }
    #endregion        
}