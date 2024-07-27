using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public void LoadScene(int index)
    {
        ResumeTime();
        SceneManager.LoadScene(index);
    }

    public void LoadNextScene()
    {
        ResumeTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        ResumeTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResumeTime()
    {
        Time.timeScale = 1;
    }
}