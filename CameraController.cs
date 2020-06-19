using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float movespeed = 10;
    public float rotatespeed = 2;
    private bool movelock = false;
    RingGen rg;
    UIcontroller uic;

    private void Start()
    {
        rg = GameObject.Find("RingGen").GetComponent<RingGen>();
        uic = GameObject.Find("UIController").GetComponent<UIcontroller>();
    }

    void Update()
    {
        if (rg.cameraRelease)
        {
            transform.position = new Vector3(this.transform.position.x, this.transform.position.y + rg.calculatedlength * rg.cylinderlength, this.transform.position.z);
            rg.cameraRelease = false;

        }
        if (Input.GetKey(KeyCode.W) && !movelock)
        {
            MoveObjectTo(this.transform, new Vector3(this.transform.position.x, this.transform.position.y - 8, this.transform.position.z), movespeed);
        }
        if (Input.GetKey(KeyCode.S) && !movelock)
        {
            MoveObjectTo(this.transform, new Vector3(this.transform.position.x, this.transform.position.y + 8, this.transform.position.z), movespeed);
        }
        if (Input.GetKeyDown(KeyCode.A) && !movelock)
        {
            RotateAroundTo(this.transform, Vector3.up, rotatespeed);
            //Vector3 gravzero = new Vector3(0, this.gameObject.transform.position.y, 0);
            //this.gameObject.transform.RotateAround(gravzero, Vector3.up, (movespeed * Time.deltaTime) * 3);
        }
        if (Input.GetKeyDown(KeyCode.D) && !movelock)
        {
            RotateAroundTo(this.transform, -Vector3.up, rotatespeed);
            //Vector3 gravzero = new Vector3(0, this.gameObject.transform.position.y, 0);
            //this.gameObject.transform.RotateAround(gravzero, -Vector3.up, movespeed * Time.deltaTime);
        }
        Vector3 relativePos = Vector3.zero - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    private void MoveObjectTo(Transform objectToMove, Vector3 targetPosition, float moveSpeed)
    {
        uic.UIactive = false;
        foreach (var item in uic.selectables)
        {
            item.GetComponent<Interactable>().unselect();
        }
        StopCoroutine(MoveObject(objectToMove, targetPosition, moveSpeed));
        StartCoroutine(MoveObject(objectToMove, targetPosition, moveSpeed));
    }

    private void RotateAroundTo(Transform objectToMove, Vector3 targetPosition, float moveSpeed)
    {
        uic.UIactive = false;
        foreach (var item in uic.selectables)
        {
            item.GetComponent<Interactable>().unselect();
        }
        StopCoroutine(RotateAround(objectToMove, targetPosition, moveSpeed));
        StartCoroutine(RotateAround(objectToMove, targetPosition, moveSpeed));
    }

    public IEnumerator MoveObject(Transform objectToMove, Vector3 targetPosition, float moveSpeed)
    {
        float currentProgress = 0;
        Vector3 cashedObjectPosition = objectToMove.transform.position;        
        while (currentProgress <= 1)
        {
            movelock = true;
            currentProgress += moveSpeed * Time.deltaTime;
            objectToMove.position = Vector3.Lerp(cashedObjectPosition, targetPosition, currentProgress);
            yield return movelock = false;
        }
    }

    public IEnumerator RotateAround(Transform objectToMove, Vector3 targetPosition, float moveSpeed)
    {
        float currentProgress = 0;
        Vector3 cashedObjectPosition = objectToMove.transform.position;

        while (currentProgress <= 1)
        {
            movelock = true;
            currentProgress += moveSpeed * Time.deltaTime;
            Vector3 gravzero = new Vector3(0, this.gameObject.transform.position.y, 0);
            this.gameObject.transform.RotateAround(gravzero, targetPosition, currentProgress);
            yield return movelock = false;
        }
    }
}
