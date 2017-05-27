using UnityEngine;
using System;

public enum PlayerDirection
{
    Left,
    Right,
    None
}

[Serializable]
public class PlayerData
{
    public float minSpeedRotate = 100f;
    public float increeVelocityRotate = 200f;
    public float bulletPerSec;
    public float minBulletPerSec = 1;
    public float bulletPerSecIncreeVelocity = 5;

    public float MaxSpeedRotateFollowMusic
    {
        get
        {
            return minSpeedRotate + AudioMeasure.Instance.RmsValue * increeVelocityRotate;
        }
    }

    public float BulletPerSecFollowMusic
    {
        get
        {
            bulletPerSec = minBulletPerSec + bulletPerSecIncreeVelocity * AudioMeasure.Instance.RmsValue;
            return bulletPerSec;
        }
    }
}

public class PlayerBehaviour : Singleton<PlayerBehaviour>
{
    private PlayerDirection direction;
    private bool startMoving;
    private float timer;

    public Transform[] gunPos;
    public PlayerData playerData;
    public GameObject bullet;

    [SerializeField] float currentSpeed;
    [SerializeField] float minSpeed = 720;
    [SerializeField] float increaseSpeedVelocity = 100;

    void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if (mousePos.x > 0)
            {
                direction = PlayerDirection.Right;
                startMoving = true;
            }
            else
            {
                direction = PlayerDirection.Left;
                startMoving = true;
            }
        }
        else
        {
            startMoving = false;
        }

        UpdateRotaion();

        if (bullet != null)
            UpdateFire();
    }

    void UpdateFire()
    {
        timer += Time.deltaTime;

        if (timer > 1.0f / playerData.BulletPerSecFollowMusic)
        {
            timer = 0;
            Fire();
        }
    }

    void Fire()
    {
        for (int i = 0; i < gunPos.Length; i++)
        {
            var bulletBehaviour = PoolManager.SpawnObject(bullet, gunPos[i].transform.position, gunPos[i].transform.rotation).GetComponent<BulletBehaviour>();
            bulletBehaviour.Fire();
        }
    }

    void UpdateRotaion()
    {
        if (startMoving == true)
        {
            if (currentSpeed < playerData.MaxSpeedRotateFollowMusic)
                currentSpeed += Time.deltaTime * SpeedVelocityFollowMusic;
            else
                currentSpeed -= Time.deltaTime * SpeedVelocityFollowMusic;
        }
        else
        {
            if (currentSpeed > 0)
                currentSpeed -= Time.deltaTime * SpeedVelocityFollowMusic;
            else
                currentSpeed = 0;
        }

        Rotate(currentSpeed);
    }

    private float SpeedVelocityFollowMusic
    {
        get
        {
            return minSpeed + AudioMeasure.Instance.RmsValue * increaseSpeedVelocity;
        }
    }

    public void Rotate(float speed)
    {
        switch (direction)
        {
            case PlayerDirection.Left:
                this.transform.Rotate(Vector3.forward * Time.deltaTime * speed);
                break;
            case PlayerDirection.Right:
                this.transform.Rotate(Vector3.back * Time.deltaTime * speed);
                break;
        }
    }
}