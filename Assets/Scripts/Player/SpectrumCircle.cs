using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumCircle : Singleton<SpectrumCircle>
{
    public GameObject[] listMelodyPrefab;

    [SerializeField] int numOfMelody = 30;
    [SerializeField] float radius = 4;
    [SerializeField] int valueScaleY = 200;

    public List<GameObject> listMelody;

    //
    private void Setup()
    {
        for (int i = 0; i < numOfMelody; i++)
        {
            float angle = i * Mathf.PI * 2 / numOfMelody;
            var pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            int index = (ScoreManager.Instance.currentNumLife - 1) < 3 ? (ScoreManager.Instance.currentNumLife - 1) : 2;
            var go = Instantiate(listMelodyPrefab[index], pos, Quaternion.Euler(0, 0, GameUtils.RadToDeg(angle) - 90), transform) as GameObject;

            listMelody.Add(go);
        }
    }

    // Use this for initialization
    public void Start()
    {
        listMelody = new List<GameObject>();
        Setup();
    }

    // Update is called once per frame
    void Update ()
    {
        float[] spectrum = AudioMeasure.Instance.Spectrum;

        for (int i = 0; i < numOfMelody; i++)
        {
            var previousScale = listMelody[i].transform.localScale;
            previousScale.y = spectrum[i] * valueScaleY;
            listMelody[i].transform.localScale = previousScale;
        }
    }

    public void Refresh()
    {
        listMelody.Clear();
        PoolManager.ReleaseObject(this.gameObject);
    }
}
