using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BonusBehaviour : MonoBehaviour
{
    float timeAlive = 10;

    int kindOfBonus;

    public GameObject[] listBonus;

    bool isAvailable = false;

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

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void Setup(int kindBonus)
    {
        PoolManager.Instance.StartCoroutine(ReturnPool(this.gameObject, timeAlive));
        isAvailable = false;
        kindOfBonus = kindBonus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && isAvailable == false)
        {
            isAvailable = true;

            if (IsVisible == false)
                return;

            GameObject bonus;
            
            bonus = PoolManager.SpawnObject(listBonus[kindOfBonus], transform.position, Quaternion.identity);
            bonus.GetComponent<ParticleSystem>().startSize = 3;
            bonus.transform.DOMove(KingBehaviour.Instance.transform.position, 2);

            PoolManager.Instance.StartCoroutine(ReturnPool(bonus.gameObject, 2.1f));
            PoolManager.Instance.StartCoroutine(ReturnPool(this.gameObject, 0));

            if (kindOfBonus == 0)
            {
                PoolManager.Instance.StartCoroutine(IncreaseLife());
            }
            if (kindOfBonus == 1)
            {
                PoolManager.Instance.StartCoroutine(IncreaseNumGun());
            }

        }
    }

    IEnumerator IncreaseNumGun()
    {
        yield return new WaitForSeconds(1);
        PlayerManager.Instance.currentNumGun++;
        PlayerManager.Instance.isNumGunChange = true;
    }

    IEnumerator IncreaseLife()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.LifeAdd();
    }

    IEnumerator ReturnPool(GameObject gameObj, float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.ReleaseObject(gameObj);
    }
}
