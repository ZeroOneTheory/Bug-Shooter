using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {

    Rigidbody rb;

    private float zInputRaw;
    private bool isLeft;
    private bool isRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        soundFlying = gameObject.transform.Find("sound_flying").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {

        zInputRaw = Mathf.Abs(Input.GetAxisRaw("ZAxis"));
        if (zInputRaw < 0) { isLeft = true; } else if (zInputRaw > 0) { isRight = true; }
        if (zInputRaw == 0) { isLeft = false; isRight = false; }

        MovementUpDown();
        MovementForward();
        MovementRotation();
        MovementClampSpeed();
        MovementSwerve();

        FxSoundMove();


        rb.AddRelativeForce(Vector3.up * upForce);
        rb.rotation = Quaternion.Euler(new Vector3(tiltAmountForward, currentYRotation, tiltAmountSide));
    }



    public float hoverPower = 450.0f;
    public float upForce;
    void MovementUpDown()
    {
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K))
            {
                rb.velocity = rb.velocity;
            }
            if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L))
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Lerp(rb.velocity.y,0.0f,Time.deltaTime*5), rb.velocity.z);
                upForce = 281f;
            }
            if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Lerp(rb.velocity.y, 0, Time.deltaTime * 5), rb.velocity.z);
                upForce = 110f;
            }
            {
                rb.velocity = rb.velocity;
            }
            if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
            {
                upForce = 410f;
            }
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135f;
        }

        if (Input.GetKey(KeyCode.I))
        {
            upForce = 450f;
            if(Mathf.Abs(Input.GetAxis("Vertical")) >0.2f)
            {
                upForce = 500f;
            }

        } else if (Input.GetKey(KeyCode.K))
        {
            upForce -= 200f;
        } else if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Mathf.Abs(Input.GetAxis("Vertical"))<0.2f) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f))
        {
            upForce = 98.1f;
        }
    }

    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0.0f;
    private float tiltVelocityForward; 
    void MovementForward()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            rb.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }
    }

    private float desiredYRotation;
    private float rotateAmountByKey = 2.5f;
    private float rotationYVelocity;
    public float currentYRotation;
    void MovementRotation()
    {
        if (Input.GetKey(KeyCode.J))
        {
            desiredYRotation -= rotateAmountByKey;
        }
        if (Input.GetKey(KeyCode.L))
        {
            desiredYRotation += rotateAmountByKey;
        }
        currentYRotation = Mathf.SmoothDamp(currentYRotation, desiredYRotation, ref rotationYVelocity, 0.25f);
    }

    private Vector3 smoothVelocity;
    void MovementClampSpeed()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(rb.velocity.magnitude,10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(rb.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(rb.velocity.magnitude, 5.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.zero, ref smoothVelocity, 0.95f);
        }
    }

    private float sideMovementAmount = 300.0f;
    private float tiltAmountSide;
    private float tiltAmountVelocity;
    void MovementSwerve()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            rb.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, -20.0f * Input.GetAxis("Horizontal"), ref tiltAmountVelocity, 0.1f);
        } else
        {
            tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, 0, ref tiltAmountVelocity, 0.1f);
        }
    }

    private AudioSource soundFlying;
    void FxSoundMove()
    {
        soundFlying.pitch = 1 + (rb.velocity.magnitude / 100);
    }
}
