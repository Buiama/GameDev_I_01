using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    Ship ship;
    int countEnemies = 0;
    bool startNextLevel;
    float nextLevelTimer = 5;
    string[] levels = { "Level_1", "Level_2", "Level_3" };
    int currentLevel = 1;
    int scoreValue = 0;
    Text scoreValueText;

    bool isTimeStopped;
    float timeStopDuration = 5f;
    float timeStopCounter = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreValueText = GameObject.Find("ScoreValue").GetComponent<Text>();
        }
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ship = FindObjectOfType<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;
                if (ship.GetIsAlive())
                {
                    if (currentLevel <= levels.Length)
                    {
                        string sceneName = levels[currentLevel - 1];
                        SceneManager.LoadSceneAsync(sceneName);
                    }
                    else SceneManager.LoadScene("Outro");
                }
                nextLevelTimer = 5;
                startNextLevel = false;
            }
            else nextLevelTimer -= Time.deltaTime;
        }
        if (isTimeStopped)
        {
            timeStopCounter -= Time.deltaTime;
            if (timeStopCounter <= 0)
            {
                isTimeStopped = false;
                ResumeTime();
            }
        }
    }

    public void ActivateTimeStop()
    {
        isTimeStopped = true;
        timeStopCounter = timeStopDuration;
        StopTime();
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    public void AddEnemy()
    {
        countEnemies++;
    }

    public void RemoveEnemy()
    {
        countEnemies--;
        if(countEnemies == 0)
        {
            startNextLevel = true;
        }
    }

    public void EditScoreValue(int points)
    {
        scoreValue += points;
        scoreValueText.text = scoreValue.ToString();
    }

    public int GetScoreValue()
    {
        return scoreValue;
    }
}
