using System.Collections;
using UnityEngine;

public class KingBehaviour : Singleton<KingBehaviour>
{
    public GameObject prefabEffectTriangleEnemy;
    public GameObject prefabEffectPolygonEnemy;
    [SerializeField] float scaleX = 0.6f;
    [SerializeField] float scaleY = 0.6f;
    [SerializeField] float timeScale = 0.5f;
    bool isOnTriggerEnter2D = false;
    float timer = 1;
    bool isGoToBlackHole = false;

    bool isGameOver = false;

    GameObject shield;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TriangleEnemy")
        {
            var prefabTriangle = PoolManager.SpawnObject(prefabEffectTriangleEnemy, collision.transform.position, Quaternion.identity);
            PoolManager.Instance.StartCoroutine(ReleasePrefabEffect(prefabTriangle));
            ZoomIn();
            PoolManager.Instance.StartCoroutine(ZoomOut());
        }
        if (collision.tag == "PolygonEnemy")
        {
            var prefabPolygon = PoolManager.SpawnObject(prefabEffectPolygonEnemy, collision.transform.position, Quaternion.identity);
            PoolManager.Instance.StartCoroutine(ReleasePrefabEffect(prefabPolygon));
            ZoomIn();
            PoolManager.Instance.StartCoroutine(ZoomOut());
        }
        if (collision.tag == "StarEnemy")
        {
            ZoomIn();
            PoolManager.Instance.StartCoroutine(ZoomOut());
            PoolManager.Instance.StartCoroutine(GoToBlackHole());
        }

        isOnTriggerEnter2D = true;
    }

    private void Update()
    {
        if (timer > 0 && isGoToBlackHole == true)
        {
            timer -= Time.deltaTime * 0.5f;
            Vector3 vec3 = new Vector3(timer, timer, timer);
            transform.localScale = vec3;
            shield.transform.localScale = vec3;
            PlayerBehaviour.Instance.transform.localScale = vec3;
        }
    }

    void OnGUI()
    {
        if (isOnTriggerEnter2D)
        {
            Vibration.Vibrate(500);
            isOnTriggerEnter2D = false;
        }

        if (isGameOver)
        {
            GUI.Button(new Rect(0, 10, 100, 32), "Game Over!");
        }
    }

    IEnumerator GoToBlackHole()
    {
        yield return new WaitForSeconds(3);

        isGoToBlackHole = true;
        var blackHole = GameObject.FindGameObjectWithTag("BlackHole");
        transform.Translate(blackHole.transform.position);

        shield = GameObject.FindGameObjectWithTag("Shield");
        shield.transform.Translate(blackHole.transform.position);

        PlayerBehaviour.Instance.transform.Translate(blackHole.transform.position);
        PlayerBehaviour.Instance.enabled = false;

        isGameOver = true;
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