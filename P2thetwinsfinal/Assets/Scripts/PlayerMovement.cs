using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))] // Ensures that the AudioSource component is added to the player object
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public bool isSprinting = false;
    public float sprintingMultiplier;

    public float maxStamina = 100f;
    public float staminaRegenRate = 10f;
    public float sprintStaminaCost = 10f;
    private float currentStamina;

    public Slider staminaBar;

    private bool isGamePaused = false;

    private AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip sprintingSound; // Sound clip to play when sprinting

    private void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaBar();

        audioSource = GetComponent<AudioSource>(); // Get the reference to the AudioSource component
        audioSource.clip = sprintingSound; // Assign the sprinting sound clip to the AudioSource
        audioSource.loop = true; // Set the audio clip to loop
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePaused)
        {
            return; // Skip movement and input processing when the game is paused
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isSprintingInput = Input.GetKey(KeyCode.LeftShift);
        bool isStaminaDepleted = currentStamina <= 0f;

        if (isSprintingInput && !isStaminaDepleted && currentStamina > 0f)
        {
            isSprinting = true;
            currentStamina -= sprintStaminaCost * Time.deltaTime;
            if (!audioSource.isPlaying) // Check if the audio is not already playing
            {
                audioSource.Play(); // Play the sprinting sound
            }
        }
        else
        {
            isSprinting = false;
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (audioSource.isPlaying) // Check if the audio is currently playing
            {
                audioSource.Stop(); // Stop the sprinting sound
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaBar();

        Vector3 move = transform.right * x + transform.forward * z;

        if (isSprinting)
        {
            move *= sprintingMultiplier;
        }

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void UpdateStaminaBar()
    {
        staminaBar.value = currentStamina / maxStamina;
    }

    public void SetGamePaused(bool paused)
    {
        isGamePaused = paused;
    }
}
