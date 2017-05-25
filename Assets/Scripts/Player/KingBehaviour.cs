using System.Collections;
using UnityEngine;
using DG.Tweening;

public class KingBehaviour : Singleton<KingBehaviour>
{
    public GameObject prefabEffectTriangleEnemy;
    public GameObject prefabEffectPolygonEnemy;
    bool isOnTriggerEnter2D = false;

    [SerializeField] float minParticleSpeed = 1;
    [SerializeField] float particleIncreeVelocity = 10;

    public Collider2D coll;

    protected override void Awake()
    {
        base.Awake();
        coll = GetComponent<Collider2D>();
        coll.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TriangleEnemy")
        {
            var prefabTriangle = PoolManager.SpawnObject(prefabEffectTriangleEnemy, collision.transform.position, Quaternion.identity);
            PoolManager.Instance.StartCoroutine(ReleasePrefabEffect(prefabTriangle));

            ScoreManager.Instance.currentNumLife--;
        }
        if (collision.tag == "PolygonEnemy")
        {
            var prefabPolygon = PoolManager.SpawnObject(prefabEffectPolygonEnemy, collision.transform.position, Quaternion.identity);
            PoolManager.Instance.StartCoroutine(ReleasePrefabEffect(prefabPolygon));

            ScoreManager.Instance.currentNumLife--;
        }
        if (collision.tag == "StarEnemy")
        {
            GameManager.Instance.SuckInBlackHole();
        }
        if (collision.tag == "Bullet")
        {
            var prefabPolygon = PoolManager.SpawnObject(prefabEffectPolygonEnemy, collision.transform.position, Quaternion.identity);
            PoolManager.Instance.StartCoroutine(ReleasePrefabEffect(prefabPolygon));

            ScoreManager.Instance.currentNumLife--;
        }

        transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.5f);
        PoolManager.Instance.StartCoroutine(ZoomOut());

        isOnTriggerEnter2D = true;
    }

    private void Start()
    {
        PoolManager.SpawnObject(PoolManager.Instance.listPrefab[11].gameObject, Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        // StarParticle
        ParticleSystem stars = GameObject.Find("StarsParticle").GetComponent<ParticleSystem>();
        stars.playbackSpeed = minParticleSpeed + particleIncreeVelocity * AudioMeasure.Instance.RmsValue;
    }

    void OnGUI()
    {
        if (isOnTriggerEnter2D)
        {
            Vibration.Vibrate(500);
            Camera.main.DOShakePosition(0.5f, 2).OnComplete(()=>
            {
                Camera.main.transform.DOMove(new Vector3(0, 0, -10), 0.5f);
            });
            isOnTriggerEnter2D = false;
        }
    }

    IEnumerator ReleasePrefabEffect(GameObject prefab)
    {
        yield return new WaitForSeconds(1);
        PoolManager.ReleaseObject(prefab.gameObject);
    }

    IEnumerator ZoomOut()
    {
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.25f);
    }
}