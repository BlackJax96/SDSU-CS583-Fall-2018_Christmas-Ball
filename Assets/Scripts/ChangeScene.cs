using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static string CurrentScene;

    public void ChooseScene(string levelName)
    {
        CurrentScene = levelName;
        print(CurrentScene);
        SceneManager.LoadScene(levelName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        print(CurrentScene);
        switch (CurrentScene)
        {
            case "Shannon":
                ChooseScene("Veronica");
                break;
            case "Veronica":
                ChooseScene("Travis");
                break;
            case "Travis":
                ChooseScene("MenuScene");
                break;
        }
    }
    public void Retry()
    {
        ChooseScene(CurrentScene);
    }
}

