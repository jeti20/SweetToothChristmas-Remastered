using System.Collections;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    public bool _hasPowerup = false;
    public float _powerUpStrength = 1.5f;
    [SerializeField] private AudioSource collectionSound;

    [Header("PowerUpVisual")]
    [SerializeField] private MeshRenderer meshRenderer;

    private float remainingTime = 0f; // Track remaining time for the power-up
    private Coroutine powerUpCoroutine; // To store the current power-up countdown Coroutine

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            collectionSound.Play();
            meshRenderer.gameObject.SetActive(true);
            Destroy(other.gameObject);

            // If power-up is already active, extend the remaining time
            if (_hasPowerup)
            {
                remainingTime += 5f; // Add 5 seconds to the remaining time
            }
            else
            {
                _hasPowerup = true;
                remainingTime = 5f; // Start with 5 seconds if it's the first power-up
            }

            // If there is already an active power-up countdown, stop it
            if (powerUpCoroutine != null)
            {
                StopCoroutine(powerUpCoroutine);
            }

            // Start a new countdown or resume the current countdown
            powerUpCoroutine = StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        // Count down the remaining time until the power-up expires
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Decrease remaining time
            yield return null; // Wait for the next frame
        }

        // Once the time is up, deactivate the power-up
        _hasPowerup = false;
        meshRenderer.gameObject.SetActive(false);
    }
}
