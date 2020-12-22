using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 1.0f;

	public TextMeshProUGUI countText;
	public GameObject winTextObject;
    public GameObject camera;


    private Rigidbody player;
    Vector3 planMovement;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();

        // Set the count to zero 
		count = 0;

		SetCountText ();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue){

        Vector2 movement = movementValue.Get<Vector2>();
        float movementX= movement.x * speed;
        float movementZ = movement.y * speed;

        Vector3 ZCameraBase = player.transform.position - camera.transform.position;
        ZCameraBase = Vector3.Normalize(ZCameraBase);
        Vector3 XCameraBase = - Vector3.Cross(ZCameraBase,Vector3.up);
        planMovement = movementX * XCameraBase + movementZ * ZCameraBase;

    }

    void FixedUpdate(){
        player.AddForce(planMovement);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("PositiveCollectible")) 
        {
            other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText ();
        }
        if (other.gameObject.CompareTag("NegativeCollectible")) 
        {
            other.gameObject.SetActive(false);
			count = count - 1;
			SetCountText ();
        }
    }
    void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12) 
		{
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
		}
	}
}