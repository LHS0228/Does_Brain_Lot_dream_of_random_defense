using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject chooseUI;
    public TextMeshProUGUI nameText;

    public void ScoreRegistration()
    {
        if (nameText.text != null || nameText.text != "")
        {
            Debug.Log(nameText.text);
            //ScoreManager.Instance.UploadScore(nameText.text);
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
}
