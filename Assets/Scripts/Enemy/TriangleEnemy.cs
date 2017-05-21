using UnityEngine;
using System.Collections;

public class TriangleEnemy : EnemyBase
{
    public GameObject explosion;
    public GameObject blackHole;
    private bool isBlackHoleExisted = false;

    #region MonoBehaviour
    public override void Awake()
    {
        base.Awake();
        scoreBase = 10;
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

        if (IsVisible == true && isBlackHoleExisted == false)
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
        base.Setup();
    }

    public override void RotateToTarget(Vector3 target)
    {
        base.RotateToTarget(target);
    }

    public override void ReturnPool()
    {
        base.ReturnPool();
        isBlackHoleExisted = false;
    }
    #endregion

    #region Event
    public override void OnEnemyHit(int damage)
    {
        base.OnEnemyHit(damage);
    }

    public override void OnEnemyDie()
    {
        base.OnEnemyDie();
        var effect = PoolManager.SpawnObject(explosion, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        effect.Play();
        PoolManager.Instance.StartCoroutine(ReleaseParticleExplosion(effect));
        OnEnemyKilled();
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
    }
    #endregion
}
