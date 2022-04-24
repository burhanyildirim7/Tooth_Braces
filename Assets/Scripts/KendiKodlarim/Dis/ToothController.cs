using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothController : MonoBehaviour
{
    [Header("DisSayilari")]
    public int stage1ToothNumber;
    private int stage2ToothNumber;
    private int stage4ToothNumber;
    private int stage5ToothNumber;

    GameObject[] asd;

    [Header("Controllers")]
    private UIController uIController;
    private OnBoardingController onBoardingController;



    void Start()
    {
        stage1ToothNumber = GameObject.FindGameObjectsWithTag("Tooth").Length;
        stage2ToothNumber = stage1ToothNumber;
        stage4ToothNumber = stage1ToothNumber;
        stage5ToothNumber = stage1ToothNumber;

        onBoardingController = GameObject.FindObjectOfType<OnBoardingController>();
        uIController = GameObject.FindObjectOfType<UIController>();
    }

    public void Stage1FinishedTooth()
    {
        stage1ToothNumber--;

        if(stage1ToothNumber <= 0)
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

    public void Stage3FinishedTooth()
    {
        SendMessage(4);
    }

    public void Stage4FinishedTooth()
    {
        stage4ToothNumber--;

        if(stage4ToothNumber <= 0)
        {
            SendMessage(5);
        }
    }

    public void Stage5FinishedTooth()
    {
        stage5ToothNumber--;

        if (stage5ToothNumber <= 0)
        {
            SendMessage(6);
        }
    }

    private void SendMessage(int sayi)
    {
        onBoardingController.PlayOnBoarding(sayi);
    }
}
