using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossesManager : MonoBehaviour
{
    public static BossesManager Instance;
    public List<Target> Bosses;
    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public Target GetRandomBoss()
    {
        return Bosses[Random.Range(0,Bosses.Count - 1)];
    }
}
