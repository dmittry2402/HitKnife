using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public static ParticleController Instance;
    public ParticleSystem PS;

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }
    public void StartEffect(Vector3 pos)
    {
        ParticleSystem Effect;

        Effect = Instantiate(PS, null, true);
        Effect.transform.localPosition = pos;

        Effect.Stop();
        Effect.Clear();
        Effect.Play();
    }
}
