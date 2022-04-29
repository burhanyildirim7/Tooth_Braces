using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraMovement : MonoBehaviour
{

    // private GameObject Player;

    Vector3 aradakiFark;

    [Header("CameraPosition")]
    private Vector3 parentPos;
    private Transform transformParent;

    [Range(0, 2)]
    [SerializeField] private float kameraHassasiyet;


    private bool isMoving;

    void Start()
    {
        transformParent = transform.parent;
        parentPos = transformParent.position;
        isMoving = false;
    }

    IEnumerator SendingStartingPosition()
    {
        while (Vector3.Distance(transform.position, -Vector3.forward * 13 - Vector3.up * 1.25f) >= .01f)
        {
            transform.DOMove(-Vector3.forward * 13 - Vector3.up * 1.25f, 1.25f);
            yield return null;
        }
    }


    public void MoveCamera()
    {
        transformParent.DOMove(parentPos + Vector3.right * (Input.mousePosition.x - Screen.width / 2) * .008f * kameraHassasiyet + Vector3.forward * Mathf.Abs(Input.mousePosition.x - Screen.width / 2) * .006f * kameraHassasiyet, 1.25f);
        transformParent.DORotateQuaternion(Quaternion.Euler(Vector3.up * (Input.mousePosition.x - Screen.width / 2) * -.06f * kameraHassasiyet), 1.25f);
        isMoving = true;
    }

    public void BreakCamera()
    {
        isMoving = false;
        StartCoroutine(returnPos());
    }

    IEnumerator returnPos()
    {
        while (Vector3.Distance(parentPos, transformParent.position) >= .0001f && GameController.instance.isContinue && !isMoving)
        {
            transformParent.DOMove(-Vector3.forward * 13 - Vector3.up * 1.25f, 1.25f);
            transformParent.DORotateQuaternion(Quaternion.Euler(Vector3.zero), 1.25f);
            yield return null;
        }
    }


}
