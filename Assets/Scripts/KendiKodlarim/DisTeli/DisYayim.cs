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

    void Start()
    {
        mat = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
        fizik = GetComponent<Rigidbody>();
        KuvvetUygula();
    }

    // Update is called once per frame
    void Update()
    {
        // mat.color = new Color(0 + Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * 100, 255 - Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * 100, 0, 255);
        mat.color = new Color(0 + Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * .4f, 1 - Vector3.Distance(kemikler[0].transform.position, kemikler[1].transform.position) * .4f, 0);
    }

    public void KuvvetUygula()
    {
        fizik.useGravity = true;
        fizik.velocity = Vector3.forward * -3f + Vector3.up * 8f;
    }
}
