using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText; // Added for displaying the number of pick-ups collected
    public Transform respawnPoint; // Added for setting the respawn point
    public MenuController menuController;  // Added for accessing the game menu controller

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText(); // Added to initialize the count text
        // winTextObject.SetActive(false);
    }

    private void Update()
    {
        if(transform.position.y < -10) // Added to detect if the player falls off the map
        {
            Respawn(); // Respawn the player at the set respawn point
        }
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()  // Added to update the count text
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            //winTextObject.SetActive(true);
            menuController.WinGame();  // Added to call the win game method in the menu controller
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();  // Update the count text
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Respawn();
            EndGame(); // Added to end the game if the player collides with an enemy
        }
    }

    void Respawn()  // Added to respawn the player
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        transform.position = respawnPoint.position;
    }

    void EndGame()  // Added to end the game
    {
        menuController.LoseGame();  // Call the lose game method in the menu controller
        gameObject.SetActive(false); // Deactivate the player game object
    }
     
}
