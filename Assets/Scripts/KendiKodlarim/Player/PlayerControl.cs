using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBehaviour;


public class PlayerControl : MonoBehaviour
{
    [Header("MirasIslemleri")]
    private PlayerTouch playerTouch;


    [Header("Asama1")]
    public GameObject disFircasi;
    public ParticleSystem bubbleEffect;

    [Header("Asama2")]
    public GameObject waterEffect;
    public GameObject waterObj;
    public GameObject waterCollier;

    [Header("Asama3")]
    public GameObject water;
    public GameObject waterSender;

    [Header("Asama4")]
    public GameObject sticky;


    [Header("Asama6")]
    public GameObject solUstYay;
    public GameObject sagUstYay;
    public GameObject solAltYay;
    public GameObject sagAltYay;



    void Start()
    {
        playerTouch = new PlayerTouch();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.instance.isContinue)
        {
            playerTouch.Touch();
        }
    }

    public void ChangeCase(int numberOfCase)
    {
        playerTouch.caseNumber = numberOfCase;
    }
}
