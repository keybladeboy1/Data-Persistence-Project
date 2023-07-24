using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InformationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI bestScoreText;
    public static InformationManager Instance;
    public TMP_InputField playerName;
    public string highscoreName;
    public int highscoreNumber;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        LoadHighscore();

        bestScoreText.text = $"Best Score: {highscoreName} : {highscoreNumber}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }    
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    [System.Serializable]
    class SaveData
    {
        public string highscoreName;
        public int highscoreNumber;
    }

    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.highscoreName = highscoreName;
        data.highscoreNumber = highscoreNumber;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highscoreName = data.highscoreName;
            highscoreNumber = data.highscoreNumber;
        }
    }
}
