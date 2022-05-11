using UnityEngine;
using UnityEngine.UI;
using Dreamteck.Splines;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    public event Action OnTouch;
    public event Action OnFinished;

    public bool isFinish;

    [SerializeField]
    private float speed;
    private bool isSlide;
    [SerializeField]
    private bool isTap;

    public float NowSpeed;
    private bool drive = false;
    private bool takeMoney;

    public float startSpeed;
    public float Gas = 100;
    public double road = 1;
    public float speedTime;
    public float gasMinus;
    public int money;

    public Image gasImg;
    public Image roadImg;
    public Text gasText;
    public Text moneyText;

    public ParticleSystem leftSmoke;
    public ParticleSystem rightSmoke;

    public GameObject wagon1;

    public GameObject panel;
    [SerializeField] private GameObject _moneyAnimation;
    public GameObject btnWin;
    public GameObject btnLose;
    public int getMoney;
    public Text getMoneyText;

    //bonuses
    public int getMoneyL = 100;
    public int getMoneyW = 1000;

    public GameObject finishSfx;
    public AudioSource carSound;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform endLineTransform;


    private Vector3 endLinePos;

    public float ProgressValue;
    private float fullDist;

    [HideInInspector] public bool IsLose;
    [HideInInspector] public bool IsGameplay;

    [SerializeField] private GameObject _infoWindow;

    public SplineFollower _splineFollower;
    public List<GameObject> _carModels = new List<GameObject>();
    public Rigidbody Rigidbody;
    private Shop _shop;
    private Rigidbody _wagonRb;
    private SplinePositioner _wagonSpline;
    private SplineFollower _camera;
    private WagonChanger _wagonChangerl;
 

    
    

    private void Start()
    { 
        _shop = FindObjectOfType<Shop>().GetComponent<Shop>();
        _shop.gameObject.SetActive(false);
        _wagonRb = wagon1.GetComponent<Rigidbody>();
        _wagonChangerl = wagon1.GetComponent<WagonChanger>();
        _wagonSpline = wagon1.GetComponent<SplinePositioner>();

        _camera = Camera.main.GetComponent<SplineFollower>();

        for (int i = 0; i < _carModels.Count; i++)
        {
            _carModels[i].SetActive(false);
        }

        for (int i = 0; i < _shop.Cars.Count; i++)
        {
            if (_shop.Cars[i].state == CarForSale.State.Chosen)
            {
                _carModels[i].SetActive(true);
            }
        }



        endLinePos = endLineTransform.position;
        fullDist = (float)_splineFollower.spline.triggerGroups[0].
            triggers[_splineFollower.spline.triggerGroups[0].triggers.Length - 1].
            position;

        if (getMoneyL == 0)
        {
            getMoneyL = 100;
            PlayerPrefs.SetInt("getMoneyL", getMoneyL);
        }
        if (getMoneyW == 0)
        {
            getMoneyW = 1000;
            PlayerPrefs.SetInt("getMoneyW", getMoneyW);
        }
        if (gasMinus == 0)
        {
            gasMinus = 0.1f;
            PlayerPrefs.SetFloat("gasMinus", gasMinus);
        }
        if (startSpeed == 0)
        {
            startSpeed = 15f;
            PlayerPrefs.SetFloat("startSpeed", startSpeed);
        }


        getMoneyL = PlayerPrefs.GetInt("getMoneyL");
        getMoneyW = PlayerPrefs.GetInt("getMoneyW");
        gasMinus = PlayerPrefs.GetFloat("gasMinus");
        startSpeed = PlayerPrefs.GetFloat("startSpeed");
        money = PlayerPrefs.GetInt("money");
        
    }


    private void Update()
    {
        
        gasImg.fillAmount = Gas/100;

        if (Gas <= 0 && NowSpeed <= 2)
        {
            Lose();
            Gas = 0;
        }

        ProgressValue = Mathf.InverseLerp(0f, fullDist, (float)_wagonSpline.GetPosition());

        UpdateProgressFill(ProgressValue);
        getMoneyText.text = getMoney.ToString();
        moneyText.text = money.ToString();
        money = PlayerPrefs.GetInt("money");
    }


    private void FixedUpdate()
    {
        _splineFollower.followSpeed = NowSpeed;
        if(Gas > 0 && isFinish == false)
        {
            NowSpeed = Mathf.Lerp(NowSpeed, speed, Time.deltaTime * speedTime);
        }
        else
        {
            NowSpeed = Mathf.Lerp(NowSpeed, 0, Time.deltaTime * speedTime);
        }
        if (drive && Gas > 0 && isFinish == false)
        {
            Gas -= gasMinus;
            leftSmoke.Play();
            rightSmoke.Play();
            carSound.pitch = Mathf.Lerp(carSound.pitch, 3f, Time.deltaTime * speedTime);
        }
        else
        {
            leftSmoke.Stop();
            rightSmoke.Stop();
            carSound.pitch = Mathf.Lerp(carSound.pitch, 1f, Time.deltaTime * speedTime);
        }

    }


    public void tap()
    {
        OnTouch?.Invoke();

        _wagonChangerl.ActivateWood();
        _shop.ShopButton.SetActive(false);
        IsGameplay = true;
        
        if (Gas > 0 && isFinish == false)
        {
            drive = true;
            isTap = true;

            if(isSlide == false)
            {
                speed = startSpeed;

                //sound.SetActive(true);
            }
            else
            {
                speed = startSpeed + 15;

                //sound.SetActive(true);
            }
        }
    }


    public void unTap()
    {

        //sound.SetActive(false);

        if (isSlide == false && isFinish == false)
        {
           
            speed = 0;
            drive = false;
        }
        else
        {
            speed = startSpeed + 15;
            drive = false;
        }

        isTap = false;
    }


    public void Win()
    {
        OnFinished?.Invoke();

        panel.SetActive(true);  
        btnWin.SetActive(true);

        getMoney = getMoneyW;

        if (takeMoney == false)
        {
            money += getMoney;
            PlayerPrefs.SetInt("money", money);
        }

        getMoneyText.text = getMoney.ToString();
        moneyText.text = money.ToString();

        takeMoney = true;

        finishSfx.SetActive(true);
    }


    public void Lose()
    {
        IsLose = true;

        panel.SetActive(true);
        btnLose.SetActive(true);

        getMoney = getMoneyL;

        if (takeMoney == false)
        {
            money += getMoney;
            PlayerPrefs.SetInt("money", money);
        }

        getMoneyText.text = getMoney.ToString();
        moneyText.text = money.ToString();

        takeMoney = true;
        _wagonSpline.enabled = false;
        _wagonRb.isKinematic = false;
        _camera.enabled = false;
    }


    public void TakeMoney()
    {
        _moneyAnimation.SetActive(true);

        money += getMoney;
        PlayerPrefs.SetInt("money", money);
        getMoneyText.text = getMoney.ToString();
        moneyText.text = money.ToString();
    }


    public void MultiplyAndTakeMoney()
    {
        //ADS

        _moneyAnimation.SetActive(true);

        getMoney = getMoney * 3;
        money += getMoney;
        PlayerPrefs.SetInt("money", money);
        getMoneyText.text = getMoney.ToString();
        moneyText.text = money.ToString();
    }


    public void collTrigerEnter()
    {
        speed += 15;
        isSlide = true;
    }


    public void collTrigerExit()
    {
        if (isTap)
        {
            speed = startSpeed;
        }
        else
        {
            speed = 0;
        }
        isSlide = false;
    }


    public void Stop()
    {
        Win();

        _infoWindow.SetActive(false);

        road = 1;
        NowSpeed = 0;
        speed = 0;

        isFinish = true;
        IsGameplay = false;
    }


  


    private void UpdateProgressFill(float value)
    {
        roadImg.fillAmount = value;
    }
}