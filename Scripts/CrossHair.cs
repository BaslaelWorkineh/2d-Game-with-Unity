using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    PlayerMovement playerMovement;
    CharacterController2D controller;

    [SerializeField] GameObject centerObject; // Reference to the player's center object
    [SerializeField] float distanceFromCenter = 2f; // Distance from the player's center

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        controller = FindObjectOfType<CharacterController2D>();
    }

    void Update()
    {
        // Get joystick input
        float horizontal = playerMovement.aimJoystick.Horizontal;
        float vertical = playerMovement.aimJoystick.Vertical;

        // Calculate the crosshair position based on the player's center and joystick input
        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized; // Normalize to prevent faster diagonal movement
        Vector3 newPosition = centerObject.transform.position + direction * distanceFromCenter;

        // Set the crosshair's position
        transform.position = newPosition;

        // Optionally, if you want to also update the effector target
        if (playerMovement.effectorTarget != null)
        {
            playerMovement.effectorTarget.transform.position = newPosition;
        }
    }
}

