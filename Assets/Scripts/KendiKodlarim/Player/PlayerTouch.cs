using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisFircasi;
using DisSulama;
using DisSuCekme;
using DisYapiskan;
using DisBraket;
using DisYayi;
using DisTelim;

namespace PlayerBehaviour
{
    public class PlayerTouch : MonoBehaviour
    {
        [Header("DokunmaIslemleri")]
        RaycastHit hit;

        [Header("MirasAlmaIslemleri")]
        public Asama1 asama1;
        public Asama2 asama2;
        public Asama3 asama3;
        public Asama4 asama4;
        public Asama5 asama5;
        public Asama6 asama6;
        public Asama7 asama7;

        [Header("Durumlar")]
        public int caseNumber;

        [Header("Controller")]
        private OnBoardingController onBoardingController;
        private CameraMovement _cameraMovement;

        int layerMask1;
        int layerMask2;



        public PlayerTouch()
        {
            asama1 = new Asama1();
            asama2 = new Asama2();
            asama3 = new Asama3();
            asama4 = new Asama4();
            asama5 = new Asama5();
            asama6 = new Asama6();
            asama7 = new Asama7();

            layerMask1 = 1 << 4 | 1 << 2;
            layerMask2 = 1 << 4 | 1 << 7 | 1 << 2 | 1 << 9;
            //  layerMask1 = ~layerMask1;
            layerMask2 = ~layerMask2;

            onBoardingController = GameObject.FindObjectOfType<OnBoardingController>();
            _cameraMovement = GameObject.FindObjectOfType<CameraMovement>();
        }

        public void Touch()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 50, layerMask1))
                {
                    caseNumber = hit.transform.gameObject.GetComponent<ObjectNumber>().caseNumber;

                    if (caseNumber == 2 || caseNumber == 4)
                    {

                        layerMask2 = 1 << 9;
                    }
                    else
                    {
                        layerMask2 = 1 << 4 | 1 << 7 | 1 << 2 | 1 << 9;
                        layerMask2 = ~layerMask2;
                    }

                    onBoardingController.DeactiveOnBoarding();
                }
            }


            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 50, layerMask2))
                {
                    if (hit.transform.CompareTag("Tooth"))
                    {
                        switch (caseNumber)
                        {
                            case 1:
                                asama1._hit = hit;
                                asama1.MoveToothBrush();
                                break;
                            /*  case 2:
                                  asama2._touchPosition = hit.point;
                                  asama2._hit = hit;
                                  asama2.MoveWater();

                                  asama3.IncreaseWaterAmount();
                                  break;*/
                            case 3:
                                asama3._hit = hit;
                                asama3.MoveWaterSender();
                                break;
                            /* case 4:
                                 asama4._hit = hit;
                                 asama4.BiggerSticky();
                                 asama4.MoveSticky();
                                 break;*/
                            case 5:
                                asama5._hit = hit;
                                asama5.CreateBraket();
                                break;
                            case 6:

                                break;
                            case 7:
                                asama7._hit = hit;
                                asama7.MoveDisYayi();
                                break;
                        }
                    }
                    else if (hit.transform.CompareTag("Raycast"))
                    {
                        switch (caseNumber)
                        {
                            case 1:
                                asama1._hit = hit;
                                asama1.OnlyMoveToothBrush();
                                break;
                            case 2:
                                asama2._touchPosition = hit.point;
                                asama2._hit = hit;
                                asama2.OnlyMoveWater();
                                break;
                            case 3:
                                asama3._hit = hit;
                                asama3.OnlyMoveWaterSender();
                                break;
                            case 4:
                                asama4._hit = hit;
                                asama4.OnlyMoveSticky();
                                break;
                            case 5:
                                asama5._hit = hit;
                                //asama5.MoveBraket();
                                break;
                            case 6:
                               
                            case 7:
                                asama7._hit = hit;
                                asama7.MoveDisYayi();
                                break;
                                break;
                        }
                    }
                    else if (hit.transform.CompareTag("UzakLayer"))
                    {
                        switch (caseNumber)
                        {
                            case 2:
                                asama2._touchPosition = hit.point;
                                asama2._hit = hit;
                                asama2.MoveWater();
                                asama3.IncreaseWaterAmount();
                                break;
                            case 4:
                                asama4._hit = hit;
                                asama4.MoveSticky();
                                break;
                        }
                    }

                    _cameraMovement.MoveCamera();
                }
                /*   else
                   {
                       Deactive();
                   }*/
            }


            if (Input.GetMouseButtonUp(0))
            {
                Deactive();

                _cameraMovement.BreakCamera();
            }
        }

        private void Deactive()
        {
            switch (caseNumber)
            {
                case 1:
                    asama1.DeactiveToothBrush();
                    caseNumber = 0;
                    break;
                case 2:
                    asama2.DeactiveWater();
                    caseNumber = 0;
                    break;
                case 3:
                    asama3.DeactiveWaterSenderer();
                    caseNumber = 0;
                    break;
                case 4:
                    asama4.DeactiveSticky();
                    caseNumber = 0;
                    break;
                case 5:
                    asama5.DeactiveBraket();
                    break;
                case 6:
                   
                    break;
                case 7:
                    asama7.DeactiveDisYayi();
                    break;
            }
        }
    }
}

