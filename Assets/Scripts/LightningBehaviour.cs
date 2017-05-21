using UnityEngine;

public class LightningBehaviour : MonoBehaviour
{
    private Plane[] planes;
    private ParticleSystem particle;
    private Collider2D objCollider;
    float speed = 100;
    bool canMove;

    void Awake()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        objCollider = GetComponent<Collider2D>();
        particle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
    }

    public void Setup()
    {
        canMove = true;
    }

    void Update()
    {
        UpdateMove();
        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds) && canMove)
        {
            ShowEffect();
        }
    }

    void UpdateMove()
    {
        if (canMove)
            this.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void ShowEffect()
    {
        CancelInvoke();
        Invoke("ReturnPool", 2);
        particle.Play();
        canMove = false;
    }

    void ReturnPool()
    {
        PoolManager.ReleaseObject(this.gameObject);
    }
}