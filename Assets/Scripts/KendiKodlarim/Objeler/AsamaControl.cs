using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsamaControl : MonoBehaviour
{
    [Header("Asamalar")]
    [SerializeField] private GameObject[] asamalar;

    [Header("DisSayisi")]
    public int disSayisi;
    public int disTeliSayisi;
    public int disYayiSayisi;

    [Header("DisTeliHaraketi")]
    public bool isMoveDentalBraces;

    [Header("DisHaraketi")]
    public bool isMoveTooth;


    [Header("Controllerler")]
    private OnBoardingController onBoardingController;
    private ToothController toothController;
    private UIController uIController;

    void Start()
    {
        onBoardingController = GameObject.FindObjectOfType<OnBoardingController>();
        toothController = GameObject.FindObjectOfType<ToothController>();
        uIController = GameObject.FindObjectOfType<UIController>();

        disSayisi = 28;
        disTeliSayisi = 28;
        isMoveDentalBraces = false;
        isMoveTooth = false;

    }

    public void Stage4Invoke()
    {
        for (int i = 0; i < asamalar.Length; i++)
        {
            asamalar[i].SetActive(false);
        }
        asamalar[1].SetActive(true);
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
    }


    public void Stage7Invoke()
    {
        disSayisi--;

        if (disSayisi <= 0)
        {
            GameObject[] obje = GameObject.FindGameObjectsWithTag("Tooth");
            for (int i = 0; i < obje.Length; i++)
            {
                Tooth tooth = obje[i].GetComponent<Tooth>();
                if (!tooth.willWearDisYayi)
                {
                    tooth.AnimasyonOynat(0);
                }
            }
            ActiveDentalBraces();
            disSayisi = 28;
            StartCoroutine(Beklet());
        }
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
    }

    IEnumerator Beklet()
    {
        yield return new WaitForSeconds(.25f);
        onBoardingController.PlayOnBoarding(5);
    }

    public void AddTel()
    {
        disTeliSayisi--;
        if (disTeliSayisi <= disYayiSayisi)
        {
            GameObject[] obje = GameObject.FindGameObjectsWithTag("Tooth");
            for (int i = 0; i < obje.Length; i++)
            {
                obje[i].GetComponent<Tooth>().AnimasyonOynat(1);
                onBoardingController.PlayOnBoarding(4);
            }
        }

        if (disTeliSayisi <= 0)
        {
            StartCoroutine(MoveDentalBraces());
            onBoardingController.DeactiveOnBoarding();
        }
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
    }

    public void ReduceTelSayisi(int sayi)
    {
        disTeliSayisi -= sayi;
        if (disTeliSayisi <= 0)
        {
            StartCoroutine(MoveDentalBraces());
        }
    }

    public void DeleteTell()
    {
        disTeliSayisi++;

        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
    }

    public void ActiveDentalBraces()
    {
        for (int i = 0; i < asamalar.Length; i++)
        {
            asamalar[i].SetActive(false);
        }
        asamalar[2].SetActive(true);

        isMoveDentalBraces = true;
    }

    public void ApplyTheMove()
    {
        StartCoroutine(MoveDentalBraces());
    }

    IEnumerator MoveDentalBraces()
    {
        yield return new WaitForSeconds(.75f);
        isMoveTooth = true;

        yield return new WaitForSeconds(1.5f);
        if (GameController.instance.isContinue)
        {
            GameController.instance.isContinue = false;
            uIController.ActivateWinScreen();
        }

    }


}
