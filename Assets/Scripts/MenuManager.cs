using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] Text HighScoreText;
    [SerializeField] InputField NameInput;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScore();
        NameInput.text = ScoreManager.Instance.Name;    
    }

    public void StartGame()
    {
        if (ScoreManager.Instance.Name == null || ScoreManager.Instance.Name.Length == 0)
        {
            Debug.Log("No name");
            return;
        }
        SceneManager.LoadScene(1);
    }
    public void UpdateHighScore()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Score hs = ScoreManager.Instance.HighScore;
            HighScoreText.text = $"Best Score : {hs.Name} : {hs.Points}";
        }
    }

    public void SetName(string name)
    {
        Debug.Log("Name set " + name);
        ScoreManager.Instance.Name = name;
    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Exit();
#endif

    }

}
