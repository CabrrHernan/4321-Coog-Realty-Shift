using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseInputUI : MonoBehaviour
{
    public GameObject winText;
    //public Button resetButton;
    //public Button exitButton;

    void Start()
    {
        winText.SetActive(false); // Hide win message initially

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowWinMessage()
    {
        winText.SetActive(true);
    }
}