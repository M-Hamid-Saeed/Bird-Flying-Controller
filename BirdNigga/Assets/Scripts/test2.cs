using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    bool shouldRoll = false;
   Quaternion baseSpin; // set once at start
float yDegrees=0;
 
public float speed=4; // allow us to adjust as we run
 
void Start() {
  baseSpin=transform.rotation;
}
 
void Update() {
  Quaternion ySpin=Quaternion.Euler(0, yDegrees,0);
  yDegrees+=speed;
 
  transform.rotation=baseSpin * ySpin;
  //transform.rotation =ySpin*baseSpin;
}

}
