using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BonusBehaviour : MonoBehaviour
{
    float timeAlive = 10;

    public GameObject[] listBonus;

    //Check out Screen
    Plane[] planes;
    Collider2D objCollider;
    bool IsVisible
    {
        get
        {
            return GeometryUtility.TestPlanesAABB(planes, objCollider.bounds);
        }
    }

    private void Awake()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        objCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
    }

    public void Setup()
    {
        PoolManager.Instance.StartCoroutine(ReturnPool(this.gameObject, timeAlive));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (IsVisible == false)
                return;

            float ran = Random.Range(0.5f, listBonus.Length - 0.5f);

            GameObject bonus;
            
            bonus = PoolManager.SpawnObject(listBonus[(int)ran], transform.position, Quaternion.identity);
            bonus.GetComponent<ParticleSystem>().startSize = 3;
            bonus.transform.DOMove(KingBehaviour.Instance.transform.position, 2);

            PoolManager.Instance.StartCoroutine(ReturnPool(bonus.gameObject, 2.1f));
            PoolManager.Instance.StartCoroutine(ReturnPool(this.gameObject, 0));

            if ((int)ran == 0)
            {
                ScoreManager.Instance.currentNumLife++;
                PoolManager.SpawnObject(PoolManager.Instance.listPrefab[11].gameObject, Vector3.zero, Quaternion.identity);
            }
            if ((int)ran == 1)
            {
                PlayerManager.Instance.currentNumGun++;
                PlayerManager.Instance.isNumGunChange = true;
            }

        }
    }

    IEnumerator ReturnPool(GameObject gameObj, float time)
    {
        yield return new WaitForSeconds(time);

        PoolManager.ReleaseObject(gameObj);
    }
}
