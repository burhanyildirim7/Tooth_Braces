using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        [Header("Durumlar")]
        public int caseNumber;

        public PlayerTouch()
        {
            asama1 = new Asama1();
            asama2 = new Asama2();
        }

        public void Touch()
        {
            int layerMask = 1 << 4 |1 << 7;
            layerMask = ~layerMask;

            if (Input.GetMouseButtonDown(0))
            {
                // asama1.DisMacunuEfektBaslat();
            }


            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 50,layerMask))
                {
                    if (hit.transform.tag == "Tooth")
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
            }
        }
    }
}

