using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTestPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player's movement
    public Transform TowerCenter; // The empty GameObject in the center of the TOWER 
    // public  float PlayerDistanceFromTowerCenter = 10f; // at the beginning it should be atleast the Radius of the cylinder
    // we will change this value later to make the player toward or away from  the cylinder center - UP DOWN ARROW KEYS

    private Vector3 moveDirection;
    private Vector3 TowerCenterVectorWithPlayerY = new Vector3(0, 0, 0);
    void Update()
    {
        // Get input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Calculate the direction from the player to the  tower center
        TowerCenterVectorWithPlayerY = new Vector3(TowerCenter.position.x, transform.position.y, TowerCenter.position.z);
        Vector3 PlayerToTowerVector = (TowerCenterVectorWithPlayerY - transform.position).normalized;
        
        // Calculate movement direction relative to the camera pivot
        //CROSS PRODUCT GIVES THE VECTOR PERPENDICULAR BETWEEN TWO VECTORS - Between 
        Vector3 rightDirection = Vector3.Cross(PlayerToTowerVector, Vector3.up); // Right direction based on the pivot
        // moveDirection = rightDirection * -horizontalInput;
        moveDirection = rightDirection * -horizontalInput + PlayerToTowerVector * verticalInput;

        // Apply movement
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the player to align with the cylinder surface
        Vector3 upDirection = (transform.position - TowerCenter.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(rightDirection, upDirection), upDirection);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        // Ensure the camera pivot is always facing the player
        TowerCenter.LookAt(transform);
    }
}
