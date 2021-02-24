using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Transform Patrons;
    public Image PatronPref, SkinImage;
    public Text AppleCountMM, ScoreCountMM, StageCountMM, AppleCount, ScoreCount, StageCount;
    public GameObject MainMenu, GameMenu, ResultMenu, MenuPanel, SkinPanel, DefeatWindow, VictoryWindow;
    public int SkinPointer = 0;
    void Awake()
    {
        if (!Instance)
            Instance = this;
    }
    public void StartGame()
    {
        MainMenu.SetActive(false);
        GameMenu.SetActive(true);
        ResultMenu.SetActive(false);

        AppleCount.text = "0";
        ScoreCount.text = "0";
        StageCount.text = "0";
    }
    public void StartMainMenu()
    {
        MainMenu.SetActive(true);
        GameMenu.SetActive(false);
        ResultMenu.SetActive(false);

        OpenMenuPanel();

        AppleCountMM.text = PlayerPrefs.GetInt("Apples").ToString();
        ScoreCountMM.text = PlayerPrefs.GetInt("Score").ToString();
        StageCountMM.text = PlayerPrefs.GetInt("Stage").ToString();
    }
    public void StartResultMenu()
    {
        MainMenu.SetActive(false);
        GameMenu.SetActive(false);
        ResultMenu.SetActive(true);
    }
    public void OpenMenuPanel()
    {
        MenuPanel.SetActive(true);
        SkinPanel.SetActive(false);
    }
    public void OpenSkinPanel()
    {
        MenuPanel.SetActive(false);
        SkinPanel.SetActive(true);

        SkinPointer = PlayerPrefs.GetInt("SkinID");
        UpdateSkin();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OkBtnSkinPanel()
    {
        MenuPanel.SetActive(true);
        SkinPanel.SetActive(false);

        PlayerPrefs.SetInt("SkinID",SkinPointer);
    }
    public void LeftBtnSkinPanel()
    {
        if (SkinPointer > 0) { SkinPointer--; }
        UpdateSkin();
    }
    public void RightBtnSkinPanel()
    {
        if (SkinPointer < GameController.Instance.KSM.Skins.Count - 1) { SkinPointer++; }
        UpdateSkin();
    }
    public void UpdateSkin()
    {
        SkinImage.sprite = GameController.Instance.KSM.GetSkinImage(SkinPointer);
    }
    public void InitPatrons()
    {
        for (int i = 0; i < GameController.Instance.KnivesCount; i++)
        {
            Instantiate(PatronPref, Patrons);
        }
    }
    public void ClearPatrons()
    {
        int count = Patrons.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = Patrons.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
            Debug.Log("Count After: " + Patrons.childCount);
        }
    }
    public void IncreaseAppleCount(int count)
    {
        AppleCount.text = count.ToString();
    }
    public void IncreaseScoreCount(int count)
    {
        ScoreCount.text = count.ToString();
    }
    public void IncreaseStageCount(int count)
    {
        StageCount.text = count.ToString();
    }
    public void ReducePatron(int knives)
    {
        Patrons.GetChild(Patrons.childCount - knives - 1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/patron_null");
    }
}
