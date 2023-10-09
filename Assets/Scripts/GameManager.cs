using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    public static GameManager Instance;
    public static float Score;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void AddScore(int value)
    {
        Score += value;
        _scoreText.SetText($"Score: {Score}");
    }
}
