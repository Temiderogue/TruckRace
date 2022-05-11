using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public int money;

    public Animator menuObj;
    public Player player;

    public int gasMoney = 500;
    public int energyMoney = 500;
    public int moneyMoney = 500;

    public Text gasMoneyText;
    public Text energyMoneyText;
    public Text moneyMoneyText;

    public int level1 = 0;
    public int level2 = 0;
    public int level3 = 0;

    public Text level1Text;
    public Text level2Text;
    public Text level3Text;

    public GameObject canvas1;
    public GameObject canvas2;
    [SerializeField] private GameObject _infoText;


    private void Update()
    {
        money = PlayerPrefs.GetInt("money");

        gasMoney = PlayerPrefs.GetInt("gasMoney");
        energyMoney = PlayerPrefs.GetInt("energyMoney");
        moneyMoney = PlayerPrefs.GetInt("moneyMoney");

        level1 = PlayerPrefs.GetInt("level1");
        level2 = PlayerPrefs.GetInt("level2");
        level3 = PlayerPrefs.GetInt("level3");

        level1Text.text = "Level " + level1.ToString();
        level2Text.text = "Level " + level2.ToString();
        level3Text.text = "Level " + level3.ToString();

        if (gasMoney == 0)
        {
            gasMoney = 100;
            PlayerPrefs.SetInt("gasMoney", gasMoney);
        }
        if (energyMoney == 0)
        {
            energyMoney = 100;
            PlayerPrefs.SetInt("energyMoney", energyMoney);
        }
        if (moneyMoney == 0)
        {
            moneyMoney = 100;
            PlayerPrefs.SetInt("moneyMoney", moneyMoney);
        }

        if (level1 == 0)
        {
            level1 = 1;
            PlayerPrefs.SetInt("level1", level1);
        }
        if (level2 == 0)
        {
            level2 = 1;
            PlayerPrefs.SetInt("level2", level2);
        }
        if (level3 == 0)
        {
            level3 = 1;
            PlayerPrefs.SetInt("level3", level3);
        }


        gasMoneyText.text = gasMoney.ToString();
        energyMoneyText.text = energyMoney.ToString();
        moneyMoneyText.text = moneyMoney.ToString();
    }

    public void startGame()
    {
        menuObj.SetTrigger("click");
        canvas2.SetActive(true);
        _infoText.SetActive(true);
    }
    public void gasBtn()
    {
        if (money >= gasMoney)
        {
            player.gasMinus /= 1.1f;
            money -= gasMoney;
            PlayerPrefs.SetFloat("gasMinus", player.gasMinus);
            PlayerPrefs.SetInt("money", money);
            gasMoney += 250;
            PlayerPrefs.SetInt("gasMoney", gasMoney);
            level1 += 1;
            PlayerPrefs.SetInt("level1", level1);
            gasMoneyText.text = gasMoney.ToString();
            level1Text.text = "Level " + level1.ToString();
        }
    }
    public void energyBtn()
    {
        if (money >= energyMoney)
        {
            player.startSpeed += 2;
            money -= energyMoney;
            PlayerPrefs.SetFloat("startSpeed", player.startSpeed);
            PlayerPrefs.SetInt("money", money);
            energyMoney += 250;
            PlayerPrefs.SetInt("energyMoney", energyMoney);
            level2 += 1;
            PlayerPrefs.SetInt("level2", level2);
            energyMoneyText.text = energyMoney.ToString();
            level2Text.text = "Level " + level2.ToString();
        }
    }
    public void moneyBtn()
    {
        if(money >= moneyMoney)
        {
            player.getMoneyL += 100;
            player.getMoneyW += 100;
            PlayerPrefs.SetInt("getMoneyL", player.getMoneyL);
            PlayerPrefs.SetInt("getMoneyW", player.getMoneyW);

            money -= moneyMoney;
            PlayerPrefs.SetInt("money", money);
            moneyMoney += 250;
            PlayerPrefs.SetInt("moneyMoney", moneyMoney);

            level3 += 1;
            PlayerPrefs.SetInt("level3", level3);

            moneyMoneyText.text = moneyMoney.ToString();
            level3Text.text = "Level " + level3.ToString();
        }
    }
}
