using UnityEngine;
using UnityEngine.UI;

public class InfoWindow_Gas : MonoBehaviour
{
    [Header ("Parameters")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _window;
    [SerializeField] private Text _textInfo;

    private float _gas;


    private void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
    }


    private void Update()
    {
        _gas = _player.Gas;

        Information();
    }


    private void Information()
    {
        if (_gas <= 5 && _gas > 0)
        {
            _window.SetActive(true);

            _textInfo.text = "LOW LEVEL OF GAS";
        }
        else if(_gas <= 0)
        {
            _window.SetActive(true);

            _textInfo.text = "OUT OF GAS";
        }
    }
}