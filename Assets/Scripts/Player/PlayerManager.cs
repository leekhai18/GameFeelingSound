using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject[] tank;
    PlayerBehaviour temp;
    public int currentNumGun = 1;

    public bool isNumGunChange = false;

    // Use this for initialization
    void Start ()
    {
        currentNumGun = 1;
        temp = PoolManager.SpawnObject(tank[currentNumGun - 1], new Vector3(0.08f, -0.01f, 0), Quaternion.identity).GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (isNumGunChange)
        {
            var rotationTemp = temp.transform.rotation;
      
            PoolManager.ReleaseObject(temp.gameObject);

            int index = (currentNumGun - 1) < 5 ? (currentNumGun - 1) : 4;
            temp = PoolManager.SpawnObject(tank[index], new Vector3(0.08f, -0.01f, 0), rotationTemp).GetComponent<PlayerBehaviour>();
            isNumGunChange = false;
        }
    }
}
