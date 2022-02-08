using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int achivements;
    private Dictionary<string, int> highScores = new Dictionary<string, int>();
    private string userName;
    private int stars;
    private List<int> unlockedSkills = new List<int>();
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is Null!");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}