using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Preferences")]
public class Preferences : ScriptableObject
{
    [Tooltip("Шанс появления яблока в %")]
    public int AppleChance;

    [Tooltip("Скорость полета ножа")]
    public int KnifeSpeed;

    [Tooltip("Минимальное количество ножей в бревне")]
    public int MinKnivesWood;

    [Tooltip("Максимальное количество ножей  в бревне")]
    public int MaxKnivesWood;

    [Tooltip("Минимальное количество ножей")]
    public int MinKnives;

    [Tooltip("Максимальное количество ножей")]
    public int MaxKnives;

    [Tooltip("Количество уровней до босса")]
    public int StagesCount;

    [Tooltip("Задержка между бросками ножа")]
    public float LatencyThrow;

    [Tooltip("Задержка между уровнями")]
    public float LatencyLevel;


    public bool HasApple()
    {
        if(Random.Range(0,100) <= AppleChance) { return true; } else { return false; }
    }
}
