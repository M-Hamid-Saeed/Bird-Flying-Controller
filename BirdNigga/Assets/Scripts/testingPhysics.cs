using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingPhysics : MonoBehaviour
{
    private Rigidbody rb;
    float x;
    float y;
    [SerializeField] float minVelocity;
    [SerializeField] float maxVelocity;
    [SerializeField] float tiltAngle;
    [SerializeField]private float alpha;
    [SerializeField]private float returnFactor;
    [SerializeField]private float percentageIncrease;
    [SerializeField]private float percentageDecrease;
    [SerializeField]private float Y_RotationValue;
    [SerializeField]private Animator anim;
    private bool shouldRoll;
    private Quaternion startRotation;
    private bool previousVerticalInput;
    private Quaternion targetRollRotation;




    [SerializeField] float smooth;
    public float speed;
    private bool boostEnd;
    private float initialVel;

    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialVel = minVelocity;
        alpha = smooth;
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
      //  UsingRollPitch();
        // testingRotation();
        

    }


    private void FixedUpdate()
    {
        MoveForwardBoost();
        UpDownlift();
        Tilt_Rotate();
    }
    void testingRotation()
    {
        transform.rotation *= Quaternion.AngleAxis(tiltAngle, Vector3.up);
    }
    void TakeInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

       /* anim.SetBool("flight_right", HorizontalInput > 0);
        anim.SetBool("flight_left", HorizontalInput < 0);*/

        // Vector3 randomRot = transform.rotation.eulerAngles;
        /*
                Quaternion var = Quaternion.Euler(randomRot);
                Quaternion currentRotation = transform.rotation;

                // Convert the quaternion to Euler angles
                Vector3 currentEulerAngles = currentRotation.eulerAngles;

                // Clamp the pitch angle
                float clampedPitch = Mathf.Clamp(currentEulerAngles.x, -70, 70);

                // Apply clamping to pitch only
                currentEulerAngles.x = clampedPitch;

                // Convert the modified Euler angles back to a quaternion
                Quaternion modifiedRotation = Quaternion.Euler(currentEulerAngles);

                // Apply the modified rotation to the transform
                transform.rotation = modifiedRotation;
                Debug.Log("EULER END"+ var.eulerAngles);*/


        //randomRot = Quaternion.AngleAxis(tiltAngle, transform.right) * randomRot;



    }
    void MoveForwardBoost()
    {
        anim.SetBool("glide_right", HorizontalInput > 0);
        anim.SetBool("glide_left", HorizontalInput < 0);
        if (Input.GetKey(KeyCode.Space))
        {
            boostEnd = false;
            anim.SetBool("boost", true);
            
            

            minVelocity = Mathf.Lerp(minVelocity, maxVelocity, Time.fixedDeltaTime * 5);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            boostEnd = true;
            anim.SetBool("boost", false);
        }
        if (boostEnd)
        {
            
            minVelocity = Mathf.Lerp(minVelocity, initialVel, Time.fixedDeltaTime * 5);
        }
        
        /*else
            minVelocity = Mathf.Lerp(maxVelocity, minVelocity, Time.fixedDeltaTime);*/
        rb.velocity = transform.forward * minVelocity;

        speed = rb.velocity.magnitude;
    }
    void Tilt_Rotate()
    {


        /*Quaternion target = Quaternion.Euler(VerticalInput * tiltAngle, 0, -HorizontalInput * tiltAngle);
        Debug.Log(VerticalInput);
        if (Mathf.Abs(VerticalInput) > 0.5f )
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            
        }*/

        /* if (VerticalInput > 0.1f)

             x += Time.fixedDeltaTime * tiltAngle;

         else if (VerticalInput < -.1f)
             x -= Time.fixedDeltaTime * tiltAngle;
         if (HorizontalInput > .1f)
             y += Time.fixedDeltaTime * smooth;
         else if (HorizontalInput < -.1f)
             y -= Time.fixedDeltaTime * smooth;
         //transform.rotation = Quaternion.Euler(x, 0, 0);
         Debug.Log(transform.eulerAngles);
         Quaternion target = Quaternion.Euler(x, y, -HorizontalInput * tiltAngle);*/
        Quaternion wantedRotation = transform.rotation;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Quaternion currentRotation = transform.rotation;
            wantedRotation = Quaternion.AngleAxis(Y_RotationValue, -Vector3.up) *currentRotation ; //Quaternion.AngleAxis(Y_RotationValue, transform.forward);
            Debug.Log("WANTED ROTATION" + wantedRotation.eulerAngles);
            // wantedRotation.x = Mathf.Clamp(wantedRotation.x, -10f, 10f);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.fixedDeltaTime * 10 );
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Quaternion currentRotation = transform.rotation;
            Debug.Log("WANTED ROTATION" + wantedRotation.eulerAngles);
            wantedRotation = Quaternion.AngleAxis(Y_RotationValue, Vector3.up) * currentRotation;//* Quaternion.AngleAxis(Y_RotationValue, -transform.forward);
            // wantedRotation.x = Mathf.Clamp(wantedRotation.x, -10f, 10f);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.fixedDeltaTime * 10);
        }

      
  

    }
    void UpDownlift()
    {
        Quaternion localRot = transform.rotation;
       // Vector3 newRot = new Vector3((VerticalInput * tiltAngle), 0,0);
        Vector3 newRot = new Vector3((VerticalInput * tiltAngle),localRot.eulerAngles.y, -HorizontalInput * tiltAngle /2);
       
        Quaternion target = Quaternion.Euler(newRot);
        
        if ((VerticalInput == 0 && previousVerticalInput))
            alpha = returnFactor;
        else if (Mathf.Abs(VerticalInput) > 0  /*Mathf.Abs(HorizontalInput) > 0)*/ && !previousVerticalInput)
            alpha = smooth;
        else if (Mathf.Abs(VerticalInput) > 0 && previousVerticalInput)
            alpha = Mathf.Clamp(alpha * ((percentageIncrease / 100f) + 1), smooth, 1.3f);
        else if (VerticalInput == 0 && !previousVerticalInput)
            alpha = Mathf.Clamp(alpha * (1 - (percentageDecrease / 100f)), smooth, 1.3f);
        if (Mathf.Abs(HorizontalInput) > 0)
            alpha = returnFactor;
        

        // alpha = VerticalInput == 0 && previousVerticalInput ? returnFactor : Mathf.Clamp(alpha * 1.5f, .01f, 1.8f);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.fixedDeltaTime * alpha);
        previousVerticalInput = Mathf.Abs(VerticalInput) > 0;//|| Mathf.Abs(HorizontalInput) > 0;
    }
        static public float ModularClamp(float val, float min, float max, float rangemin = -180f, float rangemax = 180f)
        {
            var modulus = Mathf.Abs(rangemax - rangemin);
            if ((val %= modulus) < 0f) val += modulus;
            return Mathf.Clamp(val + Mathf.Min(rangemin, rangemax), min, max);

        }
 

 

}