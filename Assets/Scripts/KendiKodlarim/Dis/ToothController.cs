using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothController : MonoBehaviour
{
    [Header("DisSayilari")]
    public int stage1ToothNumber;
    private int stage2ToothNumber;

    GameObject[] asd;

    [Header("Controllers")]
    private UIController uIController;
    private OnBoardingController onBoardingController;



    void Start()
    {
        stage1ToothNumber = GameObject.FindGameObjectsWithTag("Tooth").Length;
        stage2ToothNumber = stage1ToothNumber;

        onBoardingController = GameObject.FindObjectOfType<OnBoardingController>();
        uIController = GameObject.FindObjectOfType<UIController>();
    }

    public void Stage1FinishedTooth()
    {
        stage1ToothNumber--;

        if (stage1ToothNumber <= 0)
        {
            SendMessage(2);
        }
    }

    public void Stage2FinishedTooth()
    {
        stage2ToothNumber--;

        if (stage2ToothNumber <= 0)
        {
            SendMessage(3);
            GameObject.FindWithTag("Kopuk").transform.gameObject.SetActive(false);
        }
    }




    private void SendMessage(int sayi)
    {
        onBoardingController.PlayOnBoarding(sayi);
    }
}
