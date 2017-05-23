﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumCircle : Singleton<SpectrumCircle>
{ 
    public GameObject melodyPrefab;

    [SerializeField] int numOfMelody = 30;
    [SerializeField] float radius = 4;
    [SerializeField] int valueScaleY = 200;
    [SerializeField] float minParticleSpeed = 1;
    [SerializeField] float particleIncreeVelocity = 2;

    public List<GameObject> listMelody;

    // Use this for initialization
    void Start()
    {
        listMelody = new List<GameObject>();

        for (int i = 0; i < numOfMelody; i++)
        {
            float angle = i * Mathf.PI * 2 / numOfMelody;
            var pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            var go = Instantiate(melodyPrefab, pos, Quaternion.Euler(0, 0, GameUtils.RadToDeg(angle) - 90), transform) as GameObject;

            listMelody.Add(go);
        }
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

        ParticleSystem stars = GameObject.Find("StarsParticle").GetComponent<ParticleSystem>();
        stars.playbackSpeed = minParticleSpeed + particleIncreeVelocity * AudioMeasure.Instance.RmsValue;
    }

}
