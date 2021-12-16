using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCam : MonoBehaviour
{
    public Transform visor;
    public Transform cv;
    private Ray rayPickPos;
    
    // Update is called once per frame
    void Update()
    {
        visor.position = Input.mousePosition;
        if (Input.GetButtonDown("Fire1"))
        {
            rayPickPos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (Physics.Raycast(rayPickPos, out rh))
            {
                ThrowCV(rh.point);
            }
        }
        //if (Input.GetButton("Fire1"))
        //{
            //charge += 0.02f;
        //}
        //else if (Input.GetButtonUp("Fire1"))
        //{
            //StartCoroutine(ThrowDelay());
        //}
    }
    
    public void getParamsThrow(Vector3 targetPosition, Vector3 throwPosition, out Vector3 velocityProjectile)
    {
        Vector3 dirTarget = targetPosition - throwPosition;
        float distTarget = dirTarget.magnitude;
        dirTarget = dirTarget / distTarget; //Pour éviter un sqrt de plus
        
        float speedProj = Mathf.Sqrt(distTarget * Physics.gravity.magnitude);
        Vector3 dirShoot = Quaternion.AngleAxis(-25, Vector3.Cross(Vector3.up, dirTarget)) * dirTarget;

        velocityProjectile = dirShoot * speedProj;
    }

    public void ThrowCV(Vector3 targetPosition)
    {
        //Création du projectile
        Transform proj = GameObject.Instantiate<Transform>(cv);
        Vector3 projPos = transform.position;
        projPos.y -= 2f;
        proj.position = projPos + Vector3.up; //on le lance de devant nous + transform.forward

        //Si on tire sans mouvement de la cible, quels sont les paramètres
        Vector3 velocityProjectile;
        getParamsThrow(targetPosition, proj.position, out velocityProjectile);

        proj.GetComponent<Rigidbody>().velocity = velocityProjectile * 1.25f;
        proj.GetComponent<Rigidbody>().angularVelocity = UnityEngine.Random.onUnitSphere*3.0f; //On fait tourner le projectile en l'air, pour le fun
    }
}
