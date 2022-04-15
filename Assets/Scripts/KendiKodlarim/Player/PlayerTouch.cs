using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisMacunu;

namespace PlayerBehaviour
{
    public class PlayerTouch : MonoBehaviour
    {
        [Header("DokunmaIslemleri")]
        RaycastHit hit;

        [Header("MirasAlmaIslemleri")]
        public Asama1 asama1;


        public PlayerTouch()
        {
            asama1 = new Asama1();
        }

        public void Touch()
        {
            if(Input.GetMouseButtonDown(0))
            {
                asama1.DisMacunuEfektBaslat();
            }


            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Dis")
                    {
                        asama1._touchPosition = hit.point;
                        asama1._hit = hit;
                        asama1.DisMacunuCikar();
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                asama1.DisMacunuEfektBitir();
            }
        }
    }
}

