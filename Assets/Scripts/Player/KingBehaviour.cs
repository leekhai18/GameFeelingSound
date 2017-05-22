using System.Collections;
using UnityEngine;

public class KingBehaviour : MonoBehaviour
{
    public GameObject prefabEffectTriangleEnemy;
    public GameObject prefabEffectPolygonEnemy;
    [SerializeField] float scaleX = 0.6f;
    [SerializeField] float scaleY = 0.6f;
    [SerializeField] float timeScale = 0.5f;
    bool isOnTriggerEnter2D = false;

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

        isOnTriggerEnter2D = true;
    }

    void OnGUI()
    {
        if (isOnTriggerEnter2D)
        {
            Vibration.Vibrate(500);
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
}