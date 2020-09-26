using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    #region Singleton class: Level
    public static Level Instance;

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

    public int objectsInScene;
    public int totalObjects;

    [SerializeField] ParticleSystem winFX = default;

    [Space]
    [SerializeField] Transform objectsParent = default;

    [Header("Level Objects & Obstacles")]
    [SerializeField] Material groundMaterial = default;
    [SerializeField] Material objectMaterial = default;
    [SerializeField] Material obstacleMaterial = default;
    [SerializeField] SpriteRenderer groundBorderSprite = default;
    [SerializeField] SpriteRenderer groundSideSprite = default;
    [SerializeField] Image progressFillImage = default;

    [SerializeField] SpriteRenderer bgFadeSprite = default;

    [Space]
    [Header("Level Colors-----------")]
    [Header("Ground")]
    [SerializeField] Color groundColor = default;
    [SerializeField] Color borderColor = default;
    [SerializeField] Color sideColor = default;

    [Header("Objects & Obstacle")]
    [SerializeField] Color objectColor = default;
    [SerializeField] Color obstacleColor = default;

    [Header("UI (Progress)")]
    [SerializeField] Color progressFillColor = default;

    [Header("Background")]
    [SerializeField] Color cameraColor = default;
    [SerializeField] Color fadeColor = default;

    void Start()
    {
        CountObjects();
        UpdateLevelColors();
    }
    
    void CountObjects()
    {
        totalObjects = objectsParent.childCount;
        objectsInScene = totalObjects;
    }

    public void PlayWinFX()
    {
        winFX.Play();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void LoadRestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateLevelColors()
    {
        groundMaterial.color = groundColor;
        groundSideSprite.color = sideColor;
        groundBorderSprite.color = borderColor;

        obstacleMaterial.color = obstacleColor;
        objectMaterial.color = objectColor;

        progressFillImage.color = progressFillColor;

        Camera.main.backgroundColor = cameraColor;
        bgFadeSprite.color = fadeColor;
    }

    private void OnValidate()
    {
        UpdateLevelColors();
    }
}
