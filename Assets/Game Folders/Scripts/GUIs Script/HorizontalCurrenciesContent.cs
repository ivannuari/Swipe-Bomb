using TMPro;
using UnityEngine;

public class HorizontalCurrenciesContent : MonoBehaviour
{
    [SerializeField] private TMP_Text labelCoin;
    [SerializeField] private TMP_Text labelGem;

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private void UpdateCurrencies(int coin, int gem)
    {
        labelCoin.text = coin.ToString();
        labelGem.text = gem.ToString();
    }
}
