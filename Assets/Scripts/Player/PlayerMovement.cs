using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{


    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f; // Szybkoœæ obracania
    private Vector2 currentMovementInput;
    private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;

    [Header("DeadComponents")]
    private Rigidbody rig;   
    [SerializeField] private GameObject mesh;
    [SerializeField] private ParticleSystem particalSystem;
    [SerializeField] private AudioSource deadSound;

    PlayerPowerUp playerPowerUp;

    private void Awake()
    {
        playerPowerUp = GetComponent<PlayerPowerUp>();
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Ukrycie kursora
    }

    private void Move()
    {
        // Oblicz kierunek ruchu
        Vector3 moveDirection = new Vector3(currentMovementInput.x, 0, currentMovementInput.y); // Poprawne mapowanie na XZ
        moveDirection.Normalize(); // Normalizacja wektora ruchu

        if (moveDirection.magnitude > 0.1f)
        {
            // Obrót postaci w kierunku ruchu
            Vector3 targetDirection = moveDirection;
            targetDirection.y = 0; // Wy³¹cz obrót w osi Y, aby unikaæ problemów
            transform.forward = Vector3.Lerp(transform.forward, targetDirection, Time.deltaTime * rotateSpeed);
        }

        // Ustaw prêdkoœæ ruchu
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rig.velocity.y; // Utrzymaj prêdkoœæ w osi Y
        rig.velocity = velocity;

        // Ustawienie animacji biegu
        animator.SetBool("isRunning", moveDirection.magnitude > 0.1f);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            currentMovementInput = Vector2.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (playerPowerUp._hasPowerup)
            {
                //po podniesieniu powerup i zderzeniu z enemy  odrzucamy go dalej
                Rigidbody _enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 _awayFromPlayer = collision.gameObject.transform.position - transform.position;
                _enemyRigidbody.AddForce(_awayFromPlayer * playerPowerUp._powerUpStrength, ForceMode.Impulse);
            }
            else
            {
                StartCoroutine(BackToMenu());
            }
        }
        

    }

    IEnumerator BackToMenu()
    {
        
        mesh.SetActive(false);
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        particalSystem.Play();
        deadSound.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
    }

}
