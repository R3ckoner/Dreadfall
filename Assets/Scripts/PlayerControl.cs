using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float pickupRange = 2f;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private Vector3 playerVelocity;
    private bool isGrounded;

    // Define a dictionary to store the player's weapons by their names
    private Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
    private string currentWeaponName;

    private PersistentData persistentData;
    public WeaponManager weaponManager; // Declare the variable

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Access the PersistentData through its Instance
        persistentData = PersistentData.Instance;

        // Pass the WeaponManager to GetPlayerWeapons
        persistentData.SetWeaponManager(weaponManager);

        // Load the current weapon name from persistent data
        currentWeaponName = persistentData.GetCurrentWeaponName();
        if (!string.IsNullOrEmpty(currentWeaponName))
        {
            // Equip the current weapon if it exists in the inventory
            EquipWeapon(currentWeaponName);
        }
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }

        // Handle weapon firing
        if (Input.GetButtonDown("Fire1") && weapons.ContainsKey(currentWeaponName))
        {
            weapons[currentWeaponName].Fire();
        }
    }

    private void TryPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            WeaponPickup weaponPickup = hit.collider.GetComponent<WeaponPickup>();
            if (weaponPickup != null)
            {
                string weaponName = weaponPickup.PickUp();
                EquipWeapon(weaponName); // Call the method to equip weapons
            }
        }
    }

    public void AddToInventory(string weaponName, Weapon weapon)
    {
        if (!weapons.ContainsKey(weaponName))
        {
            weapons.Add(weaponName, weapon);
        }
        else
        {
            Debug.LogWarning("Weapon already exists in the inventory: " + weaponName);
        }
    }

    public void RemoveFromInventory(string weaponName)
    {
        if (weapons.ContainsKey(weaponName))
        {
            weapons.Remove(weaponName);
        }
        else
        {
            Debug.LogWarning("Weapon not found in the inventory: " + weaponName);
        }
    }

    private void EquipWeapon(string weaponName)
    {
        if (weapons.ContainsKey(weaponName))
        {
            // Deactivate the currently equipped weapon (if any)
            if (!string.IsNullOrEmpty(currentWeaponName) && weapons.ContainsKey(currentWeaponName))
            {
                weapons[currentWeaponName].gameObject.SetActive(false);
            }

            // Activate the newly equipped weapon
            weapons[weaponName].gameObject.SetActive(true);

            // Update the current weapon name
            currentWeaponName = weaponName;

            // Save the current weapon name to PersistentData or any other data storage
            // You can use the PersistentData class to save this data
            if (persistentData != null)
            {
                persistentData.SetCurrentWeaponName(currentWeaponName);
            }
            else
            {
                Debug.LogWarning("PersistentData script not found in the scene.");
            }
        }
        else
        {
            Debug.LogWarning("Weapon not found with name: " + weaponName);
        }
    }
}
