using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BonusBehaviour : MonoBehaviour
{
    float timeAlive = 10;

    public GameObject[] listBonus;

    private void Awake()
    {
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
            float ran = Random.Range(0, listBonus.Length - 0.5f);

            Debug.Log(ran.ToString());

            GameObject bonus;
            
            bonus = PoolManager.SpawnObject(listBonus[(int)ran], transform.position, Quaternion.identity);
            bonus.GetComponent<ParticleSystem>().startSize = 3;
            bonus.transform.DOMove(KingBehaviour.Instance.transform.position, 2);

            PoolManager.Instance.StartCoroutine(ReturnPool(bonus.gameObject, 2.1f));
            PoolManager.Instance.StartCoroutine(ReturnPool(this.gameObject, 0));
        }
    }

    IEnumerator ReturnPool(GameObject gameObj, float time)
    {
        yield return new WaitForSeconds(time);

        PoolManager.ReleaseObject(gameObj);
    }
}
