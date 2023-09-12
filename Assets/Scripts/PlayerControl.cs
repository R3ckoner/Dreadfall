using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private Vector3 playerVelocity;
    private bool isGrounded;

    // Define a list to store the player's weapons
    private List<Weapon> weapons = new List<Weapon>();
    public int currentWeaponIndex = 0; // Index of the currently equipped weapon

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle player movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        movement.y = 0f; // Disable vertical movement

        characterController.Move(movement * movementSpeed * Time.deltaTime);

        // Handle player rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Jumping
        if (isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f; // Ensures character is grounded after falling
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpForce;
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Inventory management (e.g., switch weapons)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count >= 2)
        {
            SwitchWeapon(1);
        }
        // Add more key bindings for switching to other weapons as needed
    }

    // Define a method to switch weapons
    private void SwitchWeapon(int newIndex)
    {
        if (newIndex >= 0 && newIndex < weapons.Count)
        {
            // Disable the current weapon
            weapons[currentWeaponIndex].gameObject.SetActive(false);

            // Update the current weapon index
            currentWeaponIndex = newIndex;

            // Enable the newly equipped weapon
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }
    }

    // Define a method to pick up weapons
    public void PickUpWeapon(Weapon weapon)
    {
        // Add the weapon to the player's inventory
        weapons.Add(weapon);

        // Optionally, switch to the picked up weapon
        SwitchWeapon(weapons.Count - 1);

        Debug.Log("Picked up weapon: " + weapon.weaponName);
    }

    // Define a method to load the player's inventory
    public void LoadInventory(List<Weapon> inventory)
    {
        weapons.Clear(); // Clear the current inventory
        weapons.AddRange(inventory); // Add weapons from the provided inventory

        // Optionally, equip a default weapon here if needed
        if (weapons.Count > 0)
        {
            SwitchWeapon(0);
        }
    }
}
