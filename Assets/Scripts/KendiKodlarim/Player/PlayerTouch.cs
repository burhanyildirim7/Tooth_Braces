using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisMacunu;
using DisFircasi;
using DisSulama;

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

        [Header("Durumlar")]
        public int caseNumber;


        public PlayerTouch()
        {
            asama1 = new Asama1();
            asama2 = new Asama2();
            asama3 = new Asama3();
        }

        public void Touch()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // asama1.DisMacunuEfektBaslat();
            }


            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Dis")
                    {
                        switch (caseNumber)
                        {
                            case 1:
                                asama1._touchPosition = hit.point;
                                asama1._hit = hit;
                                asama1.DisMacunuCikar();
                                break;
                            case 2:
                                asama2._touchPosition = hit.point;
                                asama2._hit = hit;
                                asama2.MoveToothBrush();
                                break;
                            case 3:
                                asama3._touchPosition = hit.point;
                                asama3._hit = hit;
                                asama3.MoveWater();
                                break;
                        }
                    }
                }
                else
                {
                    switch (caseNumber)
                    {
                        case 1:
                            asama1.DisMacunuEfektBitir();
                            break;
                        case 2:
                            asama2.DeactiveToothBrush();
                            break;
                        case 3:
                            asama3.DeactiveWater();
                            break;
                      
                    }

                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                switch (caseNumber)
                {
                    case 1:
                        asama1.DisMacunuEfektBitir();
                        break;
                    case 2:
                        asama2.DeactiveToothBrush();
                        break;
                    case 3:
                        asama3.DeactiveWater();
                        break;
                }

            }
        }
    }
}

