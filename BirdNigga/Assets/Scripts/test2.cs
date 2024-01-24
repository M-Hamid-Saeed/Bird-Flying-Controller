using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    bool shouldRoll = false;
   Quaternion baseSpin; // set once at start
float yDegrees=0;
 
public float speed=4; // allow us to adjust as we run

    private void Awake()
    {
        Debug.Log("IN awkae");
    }
    void Start() {
  baseSpin=transform.rotation;
        Debug.Log("IN START");
}
 
void Update() {
        /*Quaternion ySpin=Quaternion.Euler(0, yDegrees,0);
        yDegrees+=speed;*/
        Debug.Log("In Update");

        //transform.rotation=baseSpin * ySpin;
        //transform.rotation =ySpin*baseSpin;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(transform.forward);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(transform.right);
        }
        
    }
    
    private void OnEnable()
    {

    
        Debug.Log("In Enable");
    }

}
