using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    bool CanThrow;
    public int KnivesCount, ApplesCount, ScoreCount, StageCount, StagePoolCount;
    public float Force, Latency, LevelLatency;
    public bool IsBoss, IsLevelFinished;
    public GameObject KnifePrefab, TargetPrefab;
    public Target Target, CurrentTarget;
    public Knife CurrentKnife;
    public Transform KnifeSpawn, TargetSpawn;
    public KnifeSkinsManager KSM;
    public Preferences Prefs;
    public ProfileData Data;
    UIController UI;

    GameObject KnifeGO, TargetGO;
    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    void Start()
    {
        UI = UIController.Instance;
        Latency = Prefs.LatencyThrow;
        LevelLatency = Prefs.LatencyLevel;
        StagePoolCount = Prefs.StagesCount;
        Force = Prefs.KnifeSpeed;

        Vibration.Init();

        if (!PlayerPrefs.HasKey("Score")) { PlayerPrefs.SetInt("Score", 0); }
        if (!PlayerPrefs.HasKey("Apples")) { PlayerPrefs.SetInt("Apples", 0); }
        if (!PlayerPrefs.HasKey("Stage")) { PlayerPrefs.SetInt("Stage", 0); }
        if (!PlayerPrefs.HasKey("SkinID")) { PlayerPrefs.SetInt("SkinID", 0); }

        UI.StartMainMenu();
    }
    public void StartGame()
    {
        ApplesCount = ScoreCount = StageCount = 0;
        IsBoss = false;

        KnifePrefab = KSM.GetSkin(PlayerPrefs.GetInt("SkinID", 0));
        UI.StartGame();

        StartLevel();
    }
    public void StartLevel()
    {
        Time.timeScale = 1;

        KnivesCount = Random.Range(Prefs.MinKnives,Prefs.MaxKnives);
        UI.InitPatrons();
        
        KnifeGO = Instantiate(KnifePrefab, KnifeSpawn);
        CurrentKnife = KnifeGO.GetComponent<Knife>();

        if (StageCount != 0 && StageCount % StagePoolCount == 0) {
            IsBoss = true;
            CurrentTarget = BossesManager.Instance.GetRandomBoss();
        }
        else
        {
            CurrentTarget = Target;
        }
        TargetGO = Instantiate(CurrentTarget.gameObject, TargetSpawn);

        IsLevelFinished = false;
        CanThrow = true;
    }
    public void StopLevel()
    {
        Time.timeScale = 0;

        CanThrow = false;

        Destroy(KnifeGO);
        Destroy(TargetGO);
        UI.ClearPatrons();

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && KnivesCount > 0 && CanThrow)
        {
            if (KnifeGO.transform.parent) { KnifeGO.transform.parent = null; }
            KnifeGO.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Force, ForceMode2D.Impulse);
            KnivesCount--;
            UI.ReducePatron(KnivesCount);

            if(KnivesCount == 0)
            {
                return;
            }

            KnifeGO = Instantiate(KnifePrefab, KnifeSpawn);
            CanThrow = false;
            StartCoroutine(LatencyCoroutine());
        }
    }
    public void IncreaseAppleCount()
    {
        ApplesCount++;
        UI.IncreaseAppleCount(ApplesCount);
    }
    public void IncreaseScoreCount()
    {
        ScoreCount++;
        UI.IncreaseScoreCount(ScoreCount);
    }
    public void Victory()
    {
        Vibration.Vibrate();

        CanThrow = false;

        StageCount++;
        UI.IncreaseStageCount(StageCount);

        IsLevelFinished = true;

        Crush();

        StartCoroutine(LatencyLevel());
    }
    public void Crush()
    {
        TargetGO.GetComponent<TargetCrush>().Crush();
    }
    public void Defeat()
    {
        StopLevel();
        CheckResult();
        UI.StartResultMenu();
    }
    public void CheckResult()
    {
        if(ScoreCount > PlayerPrefs.GetInt("Score")) { PlayerPrefs.SetInt("Score", ScoreCount); }
        if(StageCount > PlayerPrefs.GetInt("Stage")) { PlayerPrefs.SetInt("Stage", StageCount); }
        int CurrentApples = PlayerPrefs.GetInt("Apples");
        PlayerPrefs.SetInt("Apples", CurrentApples + ApplesCount);
    }

    IEnumerator LatencyCoroutine()
    {
        yield return new WaitForSeconds(Latency);
        CanThrow = true;
    }
    IEnumerator LatencyLevel()
    {
        yield return new WaitForSeconds(LevelLatency);
        StopLevel();
        StartLevel();
    }
}
