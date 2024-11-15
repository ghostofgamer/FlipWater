using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    [SerializeField] private Text _coinValueText;
    [SerializeField] private int _coinReward = 100;

    private int _coinValue;
    private int _defaultCount = 0;

    private void Start()
    {
        _coinValue = PlayerPrefs.GetInt("Coin", _defaultCount);
        Show();
    }

    public void AddCoin()
    {
        _coinValue += _coinReward;
        PlayerPrefs.SetInt("Coin", _coinValue);
        Show();
    }

    private void Show()
    {
        if (_coinValueText != null)
            _coinValueText.text = _coinValue.ToString();
    }
}