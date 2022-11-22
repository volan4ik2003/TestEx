using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private float playerSpeed = 5.0f;
   [SerializeField] private FloatingJoystick moveJoystick;
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.slopeLimit = 180f;
    }

    void Update()
    {  
        float x = moveJoystick.Horizontal; 
        float z = moveJoystick.Vertical;
        Vector3 move = new Vector3(x, 0, z);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }
}
