using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Kecepatan mobil bergerak maju konstan")]
    [SerializeField] private float forwardSpeed = 20f;

    [Tooltip("Kecepatan mobil bergerak ke samping")]
    [SerializeField] private float moveSpeed = 15f;

    [Tooltip("Batas pergerakan samping dari titik tengah (0)")]
    [SerializeField] private float laneWidth = 3.5f;

    // Hubungkan UIManager di Inspector
    public UIManager uiManager;

    private Rigidbody rb;
    private float moveInput;
    private bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Fungsi ini untuk menerima input dari New Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        if (isDead) return;
        moveInput = context.ReadValue<float>();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // Langsung atur kecepatan (velocity) Rigidbody.
        // Arah maju = -X, Arah samping = Z
        // Kecepatan vertikal (Y) diambil dari fisika saat ini agar gravitasi tetap bekerja.
        rb.velocity = new Vector3(-forwardSpeed, rb.velocity.y, moveInput * moveSpeed);

        // Batasi posisi mobil agar tidak keluar jalur.
        Vector3 clampedPosition = rb.position;
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -laneWidth, laneWidth);
        rb.position = clampedPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (uiManager != null)
            {
                uiManager.AddCoin();
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.velocity = Vector3.zero; // Hentikan semua gerakan

        if (uiManager != null)
        {
            uiManager.ShowGameOver();
        }
    }
}