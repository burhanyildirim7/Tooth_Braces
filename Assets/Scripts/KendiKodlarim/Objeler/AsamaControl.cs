using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsamaControl : MonoBehaviour
{
    [Header("Asamalar")]
    [SerializeField] private GameObject[] asamalar;

    [Header("DisSayisi")]
    private int disSayisi;

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
        toothController= GameObject.FindObjectOfType<ToothController>();
        uIController = GameObject.FindObjectOfType<UIController>();

        disSayisi = GameObject.FindGameObjectsWithTag("Tooth").Length;
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
    }


    public void Stage7Invoke()
    {
        disSayisi--;

        if(disSayisi <= 0)
        {
            ActiveDentalBraces();
        }
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
        uIController.ActivateWinScreen();
    }


}
