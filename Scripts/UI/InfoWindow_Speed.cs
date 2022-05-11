using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow_Speed : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _window;
    [SerializeField] private Text _textInfo;

    private float _speed;

    private float timer, durationOfLerp = 10f;
    private Vector3 _endVelocity;

    private bool NeedToCheck = false;

    private void Start()
    {
        _player.OnTouch += StartCheckInformation;
        _player.OnFinished += FinishCheckInformation;

        _player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void StartCheckInformation()
    {
        NeedToCheck = true;
        _player.OnTouch -= StartCheckInformation;
    }
    private void FinishCheckInformation()
    {
        NeedToCheck = false;
        _window.SetActive(false);
        _player.OnFinished -= FinishCheckInformation;
    }

    private void Update()
    {
        if (NeedToCheck)
            Information();
        _speed = _player.NowSpeed;
    }


    private void Information()
    {
        if (_speed <= 10)
        {
            _window.SetActive(true);

            _textInfo.text = "TOO SLOW";
        }
        else if (_speed >= 30 && _speed <= 35)
        {
            _window.SetActive(true);

            _textInfo.text = "TOO FAST";
        }
        else if (_speed >= 35.9f)
        {
            _player._splineFollower.enabled = false;
            _player.Lose();
            
        }
        else if (_speed > 10 && _speed < 30)
        {
            _window.SetActive(false);
        }
    }
}