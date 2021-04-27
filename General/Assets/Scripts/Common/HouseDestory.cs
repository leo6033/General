using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDestory : MonoBehaviour
{
    private Health health;
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Awake()
    {
        health = GetComponent<Health>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.Dead() && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }
}
