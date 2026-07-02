using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePage : Page
{
    [SerializeField] private Slider bossProgressSlider;
    [SerializeField] private Slider hpSlider;

    [SerializeField] private TMP_Text wavesText;

    protected override void Start()
    {
        base.Start();
        GameController.Instance.OnWavesStarted += Instance_OnWavesStarted;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnWavesStarted -= Instance_OnWavesStarted;
    }

    private void Instance_OnWavesStarted(int waves)
    {
        wavesText.text = $"Waves {waves}/10";
        bossProgressSlider.maxValue = 10;
        bossProgressSlider.value = waves;
    }
}