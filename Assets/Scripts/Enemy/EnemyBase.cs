using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float minSpeed = 1;
    [SerializeField] protected float velocity = 10;
    [SerializeField] protected float speed = 3;
    [SerializeField] protected int health = 100;
    protected Rigidbody2D body;
    private Plane[] planes;
    protected Collider2D objCollider;
    protected int scoreBase;

    public bool IsVisible
    {
        get
        {
            return GeometryUtility.TestPlanesAABB(planes, objCollider.bounds);
        }
    }

    #region MonoBehaviour
    public virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        objCollider = GetComponent<Collider2D>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        UpdateMove();
    }

    public virtual void UpdateMove()
    {
        CalculateSpeed();
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
    #endregion

    #region Action
    public virtual void RotateToTarget(Vector3 target)
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    public virtual void Setup()
    {
        health = 100;
        RotateToTarget(PlayerBehaviour.Instance.transform.position);
    }

    public virtual void ReturnPool()
    {
        PoolManager.ReleaseObject(this.gameObject);
        body.velocity = Vector2.zero;
    }
    #endregion

    #region Event
    public virtual void OnEnemyHit(int damage)
    {
        if (IsVisible == false)
            return;

        health -= damage;
        if (health <= 0)
            OnEnemyDie();
    }

    public virtual void OnEnemyDie()
    {
        ReturnPool();
    }

    public virtual void OnEnemyKilled()
    {
        ScoreManager.Instance.currentScore += scoreBase;
    }

    public virtual void OnEnemyEscape()
    {
        ReturnPool();
    }
    #endregion

    #region Utils
    public virtual void CalculateSpeed()
    {
        speed = minSpeed + AudioMeasure.Instance.RmsValue * velocity;

        if (IsVisible == false)
            speed *= 10;
    }
    #endregion
}