using UnityEngine;
using UnityEngine.UI;
using System;

public class OfflineMoney : MonoBehaviour
{
    [Header("Money")]
    [SerializeField] private int _plusMoney;
    private int _money;
    [Space(height: 5f)]

    [SerializeField] private GameObject _moneyAnimation;
    [SerializeField] private GameObject _buttonTakeMoney;
    [Space(height: 5f)]

    [SerializeField] private Text _plusMoneyText;
    [Space (height: 5f)]

    [Header ("Time")]
    [Tooltip("1 500 - 1 second\n 28 800 000 - 8 hours \n 43 200 000 - 12 hours \n 86 400 000 - 1 day")]
    [SerializeField] private float _toWait;

    private bool _isActive;
    private bool _isReady;

    [SerializeField] private GameObject _window;
    private Player _player;

    private ulong _lastOpen;


    private void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        _money = PlayerPrefs.GetInt("money");
        _lastOpen = ulong.Parse(PlayerPrefs.GetString("lastOpen"));
        _plusMoneyText.text = _plusMoney.ToString();

        if (!isReady())
        {
            _isReady = false;
        }
    }


    private void Update()
    {
        if(_player.IsGameplay == false)
        {
            if (_isReady == false)
            {
                if (isReady())
                {
                    _isReady = true;
                    _buttonTakeMoney.SetActive(true);
                    _window.SetActive(true);
                    return;
                }

                ulong diff = ((ulong)DateTime.Now.Ticks - _lastOpen);
                ulong m = diff / TimeSpan.TicksPerMillisecond;
            }
        }
    }


    public void Click()
    {
        PlusMoney();

        _lastOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("lastOpen", _lastOpen.ToString());
        _isReady = false;
        _window.SetActive(false);
    }


    public void MultiplyAndTakeMoney()
    {
        //ADS

        _plusMoney = _plusMoney * 3;
        _money += _plusMoney;
        PlayerPrefs.SetInt("money", _money);

        _moneyAnimation.SetActive(true);
        _buttonTakeMoney.SetActive(false);

        _lastOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("lastOpen", _lastOpen.ToString());

        _isReady = false;

        _window.SetActive(false);
    }


    private bool isReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - _lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float seconleft = (float)(_toWait - m) / 1000.0f;

        if (seconleft < 0)
        {
            _isActive = true;

            Debug.Log("Work");

            return true;
        }
        return false;
    }


    public void PlusMoney()
    {
        _moneyAnimation.SetActive(true);
        _buttonTakeMoney.SetActive(false);

        _money += _plusMoney;
        PlayerPrefs.SetInt("money", _money);
    }
}