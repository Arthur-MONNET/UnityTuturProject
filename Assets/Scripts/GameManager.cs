using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro highScoreText;
    [SerializeField] float timeBeforeRestart = 2;
    [SerializeField] float slowness = 10;

    private void Start()
    {
        highScoreText.SetText(PlayerPrefs.GetInt("HighScore").ToString("0"));
    }

    bool isGameEnded = false;
    float bonus = 0f;

    private void Update()
    {
        if (isGameEnded == false)
        {
            if(float.Parse(scoreText.text) > Time.timeSinceLevelLoad * 10f + bonus * 100f + 50f)
            {
                bonus = bonus + 1f;
            }
            float score = Time.timeSinceLevelLoad * 10f + bonus * 100f;
            scoreText.SetText(score.ToString("0"));
            if(score > PlayerPrefs.GetInt("HighScore"))
            {
                highScoreText.SetText(score.ToString("0"));
            }
        }
    }

    public void EndGame()
    {
        if (isGameEnded != true)
        {
            isGameEnded = true;
            StartCoroutine(RestartLevel());
        }
    }

    IEnumerator RestartLevel()
    {
        PlayerPrefs.SetInt("HighScore", Mathf.Max(PlayerPrefs.GetInt("HighScore"), int.Parse(scoreText.text)));
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

        yield return new WaitForSeconds(timeBeforeRestart / slowness);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;
    }
}
