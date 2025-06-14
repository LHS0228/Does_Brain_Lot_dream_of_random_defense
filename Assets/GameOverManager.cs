using TMPro;
using Unity.VisualScripting;
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
    public GameObject countText_Obj;

    public bool isCountCheck;
    public bool isGameOver;

    public float timeCount;

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
        scoreText.text = ScoreManager.Instance.Score + " Score";
    }

    public void DeadCounting()
    {
        timeCount += Time.deltaTime;

        if (!countText_Obj.activeSelf)
        {
            countText_Obj.SetActive(true);
        }

        if (10 - timeCount < -0.1f)
        {
            isGameOver = true;
            Debug.Log("게임오버");
            GameOver();
        }

        else
        {
            if (10 - timeCount < 0) countText_Obj.GetComponent<TextMeshProUGUI>().text = "0";
            else countText_Obj.GetComponent<TextMeshProUGUI>().text = (10 - timeCount).ToString("F1");
        }
    }
}
