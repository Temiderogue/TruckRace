using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarForSale : MonoBehaviour
{
    public int Number;
    public int Price;
    public float Energy;
    public float Balance;
    public State state = State.OnSale;

    public Text ButtonText;
    public Image EnergyBar;
    public Image BalanceBar;

    [SerializeField] private Shop _shop;
    public enum State
    {
        OnSale,
        Bouhgt,
        Chosen
    }

    public void ButtonClick()
    {
        switch (state)
        {
            case CarForSale.State.Bouhgt:
                //choose
                for (int i = 0; i < _shop.Cars.Count; i++)
                {
                    if (_shop.Cars[i].state == State.Chosen)
                    {
                        _shop.Cars[i].state = State.Bouhgt;
                        _shop.Cars[i].ButtonText.text = "Choose";
                    }
                    _shop.Player._carModels[i].SetActive(false);
                }

                state = State.Chosen;
                ButtonText.text = "Go";
                _shop.Player._carModels[Number].SetActive(true);
                break;
            case CarForSale.State.OnSale:
                if (_shop.Player.money >= Price)
                {
                    state = State.Bouhgt;
                    ButtonText.text = "Choose";
                }
                break;
            case CarForSale.State.Chosen:
                //nothing
                break;
            default:
                break;
        }
    }
}
