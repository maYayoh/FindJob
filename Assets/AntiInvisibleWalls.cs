using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiInvisibleWalls : MonoBehaviour
{
    private IEnumerator LetsPapersGoThrough(Collider other)
    {
        other.enabled = false;
        yield return new WaitForSeconds(0.5f);
        other.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paper"))
        {
            StartCoroutine(LetsPapersGoThrough(other));
        }
    }
}
