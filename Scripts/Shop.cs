using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<CarForSale> Cars = new List<CarForSale>();
    public GameObject ShopButton;
    public Player Player;
    private bool _isShopOpen = false;
    
    
    private void Start()
    {
        Player = FindObjectOfType<Player>().GetComponent<Player>();
        for (int i = 0; i < Cars.Count; i++)
        {
            switch (Cars[i].state)
            {
                case CarForSale.State.Bouhgt:
                    Cars[i].ButtonText.text = "Choose";
                    break;
                case CarForSale.State.OnSale:
                    Cars[i].ButtonText.text = "Buy";
                    break;
                case CarForSale.State.Chosen:
                    Cars[i].ButtonText.text = "Go!";
                    break;
                default:
                    break;
            }
            
        }
    }

    public void OpenShop()
    {
        if (_isShopOpen == true)
        {
            gameObject.SetActive(false);
            _isShopOpen = false;
        }
        else
        {
            gameObject.SetActive(true);
            _isShopOpen = true;
        }
        
    }
    
}
