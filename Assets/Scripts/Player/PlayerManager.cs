using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject[] listTank;
    public PlayerBehaviour player;
    public int currentNumGun = 1;

    public bool isNumGunChange = false;
    public int damageGun = 100;

    // Use this for initialization
    void Start ()
    {
        currentNumGun = 1;
        player = PoolManager.SpawnObject(listTank[currentNumGun - 1], new Vector3(0.08f, -0.01f, 0), Quaternion.identity).GetComponent<PlayerBehaviour>();
        player.gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update ()
    {
        if (isNumGunChange)
        {
            var rotationTemp = player.transform.rotation;
      
            PoolManager.ReleaseObject(player.gameObject);

            int index = (currentNumGun - 1) < 5 ? (currentNumGun - 1) : 4;
            player = PoolManager.SpawnObject(listTank[index], new Vector3(0.08f, -0.01f, 0), rotationTemp).GetComponent<PlayerBehaviour>();
            player.gameObject.tag = "Player";

            isNumGunChange = false;
        }
    }
}
