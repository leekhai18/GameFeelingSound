using UnityEngine;
using System.Collections;

public class AutoRotateSpawner : MonoBehaviour
{
    [SerializeField]
    float speed; //degree per second
    [SerializeField]
    bool isClockwise;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotate(speed);
    }

    public void Rotate(float speed)
    {
        if (isClockwise)
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        else
            transform.Rotate(Vector3.back * Time.deltaTime * speed);
    }
}
