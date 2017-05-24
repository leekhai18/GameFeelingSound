using System.Collections;
using UnityEngine;
using DG.Tweening;

public class KingBehaviour : Singleton<KingBehaviour>
{
    public GameObject prefabEffectTriangleEnemy;
    public GameObject prefabEffectPolygonEnemy;
    [SerializeField] float scaleX = 0.6f;
    [SerializeField] float scaleY = 0.6f;
    [SerializeField] float timeScale = 0.5f;
    bool isOnTriggerEnter2D = false;

    [SerializeField] float minParticleSpeed = 1;
    [SerializeField] float particleIncreeVelocity = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TriangleEnemy")
        {
            var prefabTriangle = PoolManager.SpawnObject(prefabEffectTriangleEnemy, collision.transform.position, Quaternion.identity);
            PoolManager.Instance.StartCoroutine(ReleasePrefabEffect(prefabTriangle));
            ZoomIn();
            PoolManager.Instance.StartCoroutine(ZoomOut());

            isOnTriggerEnter2D = true;
        }
        if (collision.tag == "PolygonEnemy")
        {
            var prefabPolygon = PoolManager.SpawnObject(prefabEffectPolygonEnemy, collision.transform.position, Quaternion.identity);
            PoolManager.Instance.StartCoroutine(ReleasePrefabEffect(prefabPolygon));
            ZoomIn();
            PoolManager.Instance.StartCoroutine(ZoomOut());

            isOnTriggerEnter2D = true;
        }
        if (collision.tag == "StarEnemy")
        {
            ZoomIn();
            PoolManager.Instance.StartCoroutine(ZoomOut());
            GameManager.Instance.SuckInBlackHole();

            isOnTriggerEnter2D = true;
        }
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

    void ZoomIn()
    {
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    IEnumerator ZoomOut()
    {
        yield return new WaitForSeconds(timeScale);
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void RotateToTarget(Vector3 target)
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }
}