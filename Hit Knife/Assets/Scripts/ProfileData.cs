using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ProfileData")]
public class ProfileData : ScriptableObject
{
    public int Score, Apples, XP, Level, Stage, IdSkin;

    public int GetScore()
    {
        return Score;
    }
    public int GetApples()
    {
        return Apples;
    }
    public int GetXP()
    {
        return XP;
    }
    public int GetLevel()
    {
        return Level;
    }
    public int GetStage()
    {
        return Stage;
    }
    public int GetIDSkin()
    {
        return IdSkin;
    }
    public void SetScore(int score)
    {
        Score = score;
    }
    public void SetApples(int apples)
    {
        Apples = apples;
    }
    public void SetXP(int xp)
    {
        XP = xp;
    }
    public void SetLevel(int level)
    {
        Level = level;
    }
    public void SetStage(int stage)
    {
        Stage = stage;
    }
    public void SetIdSkin(int idSkin)
    {
        IdSkin = idSkin;
    }
    public void IncreaseApples(int count)
    {
        Apples += count;
    }
    public void ReduceApples(int count)
    {
        Apples -= count;
    }
}
