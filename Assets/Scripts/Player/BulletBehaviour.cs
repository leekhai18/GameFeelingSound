using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 1000;
    private Rigidbody2D body;
    [Range(0.1f, 5.0f)]
    [SerializeField]
    float timeReturn = 1.5f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BlackHole")
        {
            this.OnBulletHit();
        }
    }

    public void Fire()
    {
        body.AddRelativeForce(Vector2.up * speed);
        Invoke("Finish", timeReturn);
    }

    public void Finish()
    {
        CancelInvoke();
        PoolManager.ReleaseObject(this.gameObject);
        body.velocity = Vector3.zero;
    }

    public void OnBulletHit()
    {
        Finish();
    }
}
