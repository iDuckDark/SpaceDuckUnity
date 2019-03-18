using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    public float speed, tilt, fireRate;
    private float nextFire;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;

    private Quaternion calibrateQuat;
    public SimpleTouchPad simpleTouchPad;
    public SimpleTouchAreaButton simpleTouchAreaButton;

    private void Update()
    {
        // Desktop
        //if (Input.GetButton("Fire1") && Time.time > nextFire)

        // Mobile
        if (simpleTouchAreaButton.CanFire() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //add audio for Unity 5
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }

    }

    private void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuat = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrateQuat = Quaternion.Inverse(rotateQuat);
    }

    private Vector3 FixAcceleration(Vector3 acc)
    {
        return calibrateQuat * acc;
    }

    private void FixedUpdate()
    {
        // Desktop
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Mobile Accelerometer
        //Vector3 accRaw = Input.acceleration;
        //Vector3 acc = FixAcceleration(accRaw);
        //Vector3 movement = new Vector3(acc.x, 0.0f, acc.y);

        //Mobile TouchPad
        Vector2 direction = simpleTouchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

        //RigidBody logic
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3(
             Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax));

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }
}
