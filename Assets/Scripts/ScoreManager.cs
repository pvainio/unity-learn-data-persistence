using System;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public string Name;
    public Score HighScore;

    public static ScoreManager Instance { get; private set; }

    [SerializeField] MenuManager menuManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        LoadScore();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    internal void AddScore(int points)
    {
        if (HighScore != null && points <= HighScore.Points)
        {
            return;
        }

        Score score = new Score();
        score.Points = points;
        score.Name = Name;
        HighScore = score;
        string json = JsonUtility.ToJson(score);
        File.WriteAllText(GetSavePath(), json);
        Debug.Log("Saved highscore");
        menuManager.UpdateHighScore();
    }


    private static string GetSavePath()
    {
        return Application.persistentDataPath + "/highscore.json";
    }

    internal void LoadScore()
    {
        if (File.Exists(GetSavePath()))
        {
            Debug.Log("Loaded highscore");
            string json = File.ReadAllText(GetSavePath());
            HighScore = JsonUtility.FromJson<Score>(json);
            menuManager.UpdateHighScore();
        }
    }

    [Serializable]
    public class Score
    {
        public int Points;
        public string Name;
    }
}
