using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Bullet", menuName ="Bullet")]
public class Bullet : ScriptableObject
{
    public Sprite UnloadedSprite;
    public Sprite LoadedSprite;
    public Sprite FiredSprite;

    public int Value;
    public UnityEvent OnFired;

    public void AddScore()
    {
        GameManager.Instance.AddScore(Value);
    }
}
