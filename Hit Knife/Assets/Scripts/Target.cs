using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject TargetPref;
    public List<GameObject> Objects;
    public List<Transform> Points;
    public List<Moving> Movings;
    int MovingPointer;
    float CurrrentSpeed;
    public bool isBoss;
    bool isSlow, isPause, isAccelerate;

    public GameObject ApplePrefab, KnifePrefab;

    int apples, knives;
    private void Start()
    {
        if (GameController.Instance.Prefs.HasApple()) { InitObjects(ApplePrefab, 1, false); }
        InitObjects(KnifePrefab, Random.Range(GameController.Instance.Prefs.MinKnivesWood, GameController.Instance.Prefs.MaxKnivesWood), true);

        if (Movings.Count == 0)
        {
            RandomizeMoving();
        }

        MovingPointer = 0;
        isSlow = isPause = isAccelerate = false;
        CurrrentSpeed = Movings[0].Speed;
        StartCoroutine(MovingInterval());

    }
    void RandomizeMoving()
    {
        int iterations = Random.Range(1,5);
        for(int i = 0; i < iterations; i++)
        {
            Moving moving = ScriptableObject.CreateInstance("Moving") as Moving;
            moving.Speed = Random.Range(1.5f, 4);
            int dir = Random.Range(0, 2);
            if (dir == 0) { moving.Direction = -1; } else { moving.Direction = 1; }
            moving.Interval = Random.Range(4, 20);
            moving.SlowFactor = Random.Range(0.01f, 0.2f);
            moving.Pause = Random.Range(0.5f, 2.5f);

            Movings.Add(moving);
        }
    }
    private void FixedUpdate()
    {
        if (isAccelerate) {
            CurrrentSpeed += Movings[MovingPointer].SlowFactor;
            if (CurrrentSpeed >= Movings[MovingPointer].Speed)
            {
                CurrrentSpeed = Movings[MovingPointer].Speed;
                isAccelerate = false;
                StartCoroutine(MovingInterval());
            }
        }
        
        if (isSlow) { CurrrentSpeed -= Movings[MovingPointer].SlowFactor; }
        if (CurrrentSpeed <= 0 && !isPause) {
            CurrrentSpeed = 0;
            isSlow = false;
            isPause = true;
            StartCoroutine(PauseInterval());
        }
        transform.Rotate(0, 0, CurrrentSpeed * Movings[MovingPointer].Direction);
    }
    
    IEnumerator MovingInterval()
    {
        yield return new WaitForSeconds(Movings[MovingPointer].Interval);
        isSlow = true;
    }
    IEnumerator PauseInterval()
    {
        yield return new WaitForSeconds(Movings[MovingPointer].Pause);
        isPause = false;
        isAccelerate = true;
        ChangeMovingPointer();
    }
    void ChangeMovingPointer()
    {
        if(MovingPointer + 1 >= Movings.Count) { MovingPointer = 0; } else { MovingPointer++; }
    }
    int CalculateCount()
    {
        return Random.Range(1,4);
    }
    void InitObjects(GameObject pref, int count, bool isInverted)
    {
        for(int i = 0; i < count; i++)
        {
            int Pointer = Random.Range(0, Points.Count);
            GameObject GO = Instantiate(pref, Points[Pointer]);
            if (isInverted) { GO.transform.localEulerAngles = new Vector3(0, 0, 180); }
            Points.RemoveAt(Pointer);

            Objects.Add(GO);
        }
    }


}
