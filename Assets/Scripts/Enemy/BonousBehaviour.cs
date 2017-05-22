using UnityEngine;
using System.Collections;

public class BonousBehaviour : MonoBehaviour
{
    Renderer rend;
    Rigidbody2D body;

    public float speed = 20;
    bool isEatedBullet = false;
    float timeAlive = 10;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (isEatedBullet == true)
        {
            FlyToKing();
        }
    }

    public void Setup()
    {
        isEatedBullet = false;
        rend.material.mainTextureScale = new Vector2(1, 1);
        Invoke("ReturnPool", timeAlive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            rend.material.mainTextureScale = new Vector2(0, 1);
            RotateToTarget(KingBehaviour.Instance.transform.position);
            body.velocity = Vector2.zero;
            isEatedBullet = true;
        }
        if (collision.tag == "Earth")
        {
            ReturnPool();
        }
    }

    void FlyToKing()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    public virtual void RotateToTarget(Vector3 target)
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    void ReturnPool()
    {
        PoolManager.ReleaseObject(this.gameObject);
    }
}
