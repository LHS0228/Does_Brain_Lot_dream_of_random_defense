using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject gameOverUI;
    public GameObject chooseUI;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void ScoreRegistration()
    {
        if (nameText.text != null || nameText.text != "")
        {
            Debug.Log(nameText.text);
            ScoreManager.Instance.UploadScore(nameText.text);
            gameOverUI.SetActive(false);
            chooseUI.SetActive(true);
        }
    }

    public void ReGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GameExit()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        scoreText.text = ScoreManager.Instance.Score + " Á¡";
    }
}
