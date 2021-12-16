using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterHit : MonoBehaviour
{
    public Transform popUp;

    private IEnumerator ShowPopUp()
    {
        Transform tempPopUp = GameObject.Instantiate<Transform>(popUp);
        
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        tempPopUp.position = pos;
        
        tempPopUp.LookAt(Camera.main.transform.position);
        
        Quaternion rot = new Quaternion();
        rot.eulerAngles = tempPopUp.rotation.eulerAngles + new Vector3(0, 180, 0);
        tempPopUp.transform.rotation = rot;

        yield return new WaitForSeconds(3f);
        Destroy(tempPopUp.gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Paper"))
        {
            Destroy(other.gameObject);
            StartCoroutine(ShowPopUp());
        }
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
