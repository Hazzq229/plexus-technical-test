using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Slider _progressionSlider;

    void Start()
    {
        ExperienceManager.Instance.OnLevelUp.AddListener(value => _levelText.text = value.ToString());
        ExperienceManager.Instance.OnXPChanged.AddListener(value => _progressionSlider.value = value);
    }
}
