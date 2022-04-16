using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBehaviour;


public class PlayerControl : MonoBehaviour
{
    [Header("MirasIslemleri")]
    private PlayerTouch playerTouch;

    [Header("Asama1")]
    public ParticleSystem disMacunu;

    [Header("Asama2")]
    public ParticleSystem disFircasi;


    void Start()
    {
        playerTouch = new PlayerTouch();

        playerTouch.caseNumber = 1;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isContinue)
        {
            playerTouch.Touch();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            playerTouch.caseNumber = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerTouch.caseNumber = 1;
        }
    }
}
