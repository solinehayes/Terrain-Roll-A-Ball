using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float pan_sensitivity = 2.0f;
    public float look_sensitivity = 2.0f;
    public float scroll_sensitivity = 50.0f;

    private float rot_x;
    private float rot_y;

    public GameObject player;

    private Vector3 offset;

    void Start () {
        offset = transform.position - player.transform.position;
        Vector3 eulers = transform.localEulerAngles;
        rot_x = eulers.x;
        rot_y = eulers.y;
    }

    void Update () {

        Transform t = transform;
        float mx = Input.GetAxisRaw("Mouse X");
        float my = Input.GetAxisRaw("Mouse Y");
        
        float scrl = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1)) {

            rot_y = transform.localEulerAngles.y;


            rot_x -= my * look_sensitivity;
            rot_y += mx * look_sensitivity;

            t.localRotation = Quaternion.Euler(rot_x, rot_y, 0.0f);
        }
        else if(Input.GetMouseButton(0)){
            float rotationAngle = 3.14f/180.0f * mx * look_sensitivity;
            float[,] rotationMatrix = new float[2, 2];
            float angleToPlayer;

            rotationMatrix[0,0] = Mathf.Cos(rotationAngle);
            rotationMatrix[0,1] = - Mathf.Sin(rotationAngle);
            rotationMatrix[1,0] = Mathf.Sin(rotationAngle);
            rotationMatrix[1,1] = Mathf.Cos(rotationAngle);

            Vector2 newOffset = new Vector2(rotationMatrix[0,0]*offset.x + rotationMatrix[0,1]*offset.z ,
                                rotationMatrix[1,0]* offset.x + rotationMatrix[1,1]* offset.z);

            offset = new Vector3(newOffset.x, offset.y, newOffset.y);

            if (offset.z <0  && offset.x < 0) angleToPlayer = Mathf.Atan(offset.x/offset.z)*180.0f/3.14f;
            else if (offset.z <0  && offset.x > 0) angleToPlayer = -  Mathf.Atan(Mathf.Abs(offset.x/offset.z))*180.0f/3.14f;
            else if (offset.z >0  && offset.x > 0) angleToPlayer = 180.0f + Mathf.Atan(Mathf.Abs(offset.x/offset.z))*180.0f/3.14f;
            else angleToPlayer =  180.0f - Mathf.Atan(Mathf.Abs(offset.x/offset.z))*180.0f/3.14f;

            t.localRotation = Quaternion.Euler(rot_x, angleToPlayer, 0.0f);

        }
        t.position = player.transform.position + offset;
    
        
    }
}
