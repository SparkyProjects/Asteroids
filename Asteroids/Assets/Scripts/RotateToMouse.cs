﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera cam;
    public float maximumLength;


    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;

    // Update is called once per frame
    void Update()
    {
        if (cam != null){
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = cam.ScreenPointToRay (mousePos);

            //Raycast determins if the projectile has hit something
            //max length I believe is used if there is no collision.
            if(Physics.Raycast (rayMouse.origin, rayMouse.direction, out hit, maximumLength)){
                RotateToMouseDirection (gameObject, hit.point);
            } else {
                var pos= rayMouse.GetPoint(maximumLength);
                RotateToMouseDirection(gameObject, pos);
            }

        }else{
            Debug.Log("MouseRotationScript - No camera selected");
        }
    }

    void RotateToMouseDirection (GameObject obj, Vector3 destination){
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation (direction);
        obj.transform.localRotation = Quaternion.Lerp (obj.transform.rotation,rotation,1);
    }

    public Quaternion GetRotation(){
        return rotation;
    }
}
