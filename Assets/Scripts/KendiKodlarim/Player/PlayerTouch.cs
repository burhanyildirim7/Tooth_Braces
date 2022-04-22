using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisFircasi;
using DisSulama;
using DisSuCekme;
using DisYapiskan;
using DisBraket;

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

        [Header("Durumlar")]
        public int caseNumber;

        int layerMask1;
        int layerMask2;


        public PlayerTouch()
        {
            asama1 = new Asama1();
            asama2 = new Asama2();
            asama3 = new Asama3();
            asama4 = new Asama4();
            asama5 = new Asama5();

            layerMask1 = 1 << 4 | 1 << 7 | 1 << 2;
            layerMask2 = 1 << 4 | 1 << 7 | 1 << 2;
            //  layerMask1 = ~layerMask1;
            layerMask2 = ~layerMask2;
        }

        public void Touch()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 50, layerMask1))
                {
                    caseNumber = hit.transform.gameObject.GetComponent<ObjectNumber>().caseNumber;
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
                                asama1._touchPosition = hit.point;
                                asama1._hit = hit;
                                asama1.MoveToothBrush();
                                break;
                            case 2:
                                asama2._touchPosition = hit.point;
                                asama2._hit = hit;
                                asama2.MoveWater();

                                asama3.IncreaseWaterAmount();
                                break;
                            case 3:
                                asama3._hit = hit;
                                asama3.MoveWaterSender();
                                break;
                            case 4:
                                asama4._hit = hit;
                                asama4.BiggerSticky();
                                break;
                            case 5:
                                asama5._hit = hit;
                                asama5.CreateBraket();
                                break;
                        }
                    }
                    else if (hit.transform.CompareTag("Raycast"))
                    {
                        switch (caseNumber)
                        {
                            case 1:
                                asama1._touchPosition = hit.point;
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
                                break;
                            case 5:

                                break;  
                        }
                    }
                }
                else
                {
                    Deactive();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Deactive();
            }
        }

        private void Deactive()
        {
            switch (caseNumber)
            {
                case 1:
                    asama1.DeactiveToothBrush();
                    break;
                case 2:
                    asama2.DeactiveWater();
                    break;
                case 3:
                    asama3.DeactiveWaterSenderer();
                    break;
            }
        }
    }
}

