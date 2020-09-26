using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singleton class: UIManager
    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [Header("Level Progress UI")]
    [SerializeField] int sceneOffset = default;
    [SerializeField] TMP_Text nextLevelText = default;
    [SerializeField] TMP_Text currentLevelText = default;
    [SerializeField] Image progressFillImage = default;

    [Space]
    [SerializeField] TMP_Text levelCompletedText = default;

    [Space]
    [SerializeField] Image fadePanel = default;

    void Start()
    {
        fadeAtStart(); 

        progressFillImage.fillAmount = 0f;
        SetLevelProgressText();
    }

    void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }

    public void UpdateLevelProgress()
    {
        float val = 1f - ((float)Level.Instance.objectsInScene / Level.Instance.totalObjects);
        //progressFillImage.fillAmount = val;
        progressFillImage.DOFillAmount(val, .4f);
    }

    //----------------------------------------------------
    public void ShowLevelCompletedUI()
    {
        levelCompletedText.DOFade(1f, .6f).From(0f);
    }

    public void fadeAtStart()
    {
        fadePanel.DOFade(0f, 1.3f).From(1f);
    }
}
