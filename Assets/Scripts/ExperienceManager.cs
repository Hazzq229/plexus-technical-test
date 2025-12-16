using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    [Header("Progression Settings")]
    public int CurrentLevel = 1;
    public float CurrentXP = 0;
    public float BaseXPRequirement = 50f;
    public float XPMultiplier = 1.5f;

    [Header("Events")]
    public UnityEvent<float> OnXPChanged;
    public UnityEvent<int> OnLevelUp;

    private float _xpToNextLevel;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        CalculateNextLevelXP();
    }

    void Start()
    {
        OnLevelUp?.Invoke(CurrentLevel);
    }

    public void AddXP(float amount)
    {
        CurrentXP += amount;

        if(CurrentXP >= _xpToNextLevel) LevelUp();

        float progressPercent = CurrentXP / _xpToNextLevel;
        OnXPChanged?.Invoke(progressPercent);

        Debug.Log($"XP bertambah: {amount}. Total XP: {CurrentXP}/{_xpToNextLevel}");
    }

    void LevelUp()
    {
        CurrentLevel++;
        CurrentXP -= _xpToNextLevel;

        CalculateNextLevelXP();

        OnLevelUp?.Invoke(CurrentLevel);
        Debug.Log($"Berhasil naik ke level {CurrentLevel}!");
    }
    void CalculateNextLevelXP()
    {
        _xpToNextLevel = BaseXPRequirement * Mathf.Pow(XPMultiplier, CurrentLevel - 1);
    }
}
