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

    void Start()
    {
        stage1ToothNumber = GameObject.FindGameObjectsWithTag("Tooth").Length;
        stage2ToothNumber = stage1ToothNumber;

        uIController = GameObject.FindObjectOfType<UIController>();
    }

    public void Stage1FinishedTooth()
    {
        stage1ToothNumber--;

        if(stage1ToothNumber <= 0)
        {
            uIController.Stage2();
        }
    }

    public void Stage2FinishedTooth()
    {
        stage2ToothNumber--;

        if (stage2ToothNumber <= 0)
        {
            uIController.Stage3();
        }
    }
}
