using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour {
    public float smoothRot;
    public float smoothPos;
    public Transform target;
    public Vector3 oldPos;
    //public Vector3 dPos;
    // Use this for initialization
    void Start () {
        //dPos = target.position - transform.position;
        oldPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 temp = transform.position - oldPos;
        Vector3 temp = Vector3.zero;
        oldPos = transform.position;
        transform.localPosition = Vector3.SmoothDamp(transform.position, target.position, ref temp, smoothPos, 100);
        
        //transform.position = Vector3.LerpUnclamped(transform.position, target.position - dPos, smoothPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.localRotation, smoothRot * Time.deltaTime);
    }
}
