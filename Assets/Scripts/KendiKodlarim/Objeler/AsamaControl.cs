using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsamaControl : MonoBehaviour
{
    [Header("Asamalar")]
    [SerializeField] private GameObject[] asamalar;

    [Header("DisSayisi")]
    private int disSayisi;
    [SerializeField] private int disTeliSayisi;
    [SerializeField] private int disYayiSayisi;

    [Header("DisTeliHaraketi")]
    public bool isMoveDentalBraces;

    [Header("DisHaraketi")]
    public bool isMoveTooth;

    [Header("DisYayi")]
    public int neededDisYayi;

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
            ActiveDentalBraces();
            disSayisi = 28;
        }
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
    }

    public void AddTel()
    {
        disTeliSayisi--;

        if (disTeliSayisi <= 0)
        {
            if (disTeliSayisi <= -2)
            {
                StartCoroutine(MoveDentalBraces());
                onBoardingController.DeactiveOnBoarding();
            }
            GameObject[] obje = GameObject.FindGameObjectsWithTag("Tooth");
            for (int i = 0; i < obje.Length; i++)
            {
                obje[i].GetComponent<Tooth>().AnimasyonOynat();

            }
            onBoardingController.PlayOnBoarding(4);
        }
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
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
        /*   yield return new WaitForSeconds(.75f);
           isMoveDentalBraces = true;*/

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
