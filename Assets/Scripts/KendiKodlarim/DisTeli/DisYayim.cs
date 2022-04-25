using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisYayim : MonoBehaviour
{
    [Header("Kemikler")]
    [SerializeField] private GameObject[] kemikler;

    [Header("Material")]
    private Material mat;

    [Header("KuuvetUygulamaIcin")]
    private Rigidbody fizik;

    public bool kopar;

    void Start()
    {
        mat = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
        fizik = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // mat.color = new Color(0 + Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * 100, 255 - Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * 100, 0, 255);
        mat.color = new Color(0 + Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * .6f, 1 - Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * .6f, 0);

        if(Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) >= 1f)
        {
            kopar = true;
          //  Debug.Log("A");
        }
        else
        {
            kopar = false;
        }
    }

    public void KuvvetUygula()
    {
        kemikler[0].transform.parent = transform;
        kemikler[1].transform.parent = transform;


        fizik.useGravity = true;
        fizik.velocity = Vector3.forward * -3f + Vector3.up * 5f + Vector3.right * 7;
    }
}
