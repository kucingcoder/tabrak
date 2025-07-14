using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float forwardSpeed = 15f;
    [SerializeField] private float laneWidth = 4f; // Jarak maksimal dari tengah jalan

    private Rigidbody rb;
    private float moveInput;
    private Vector3 initialPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    // Dipanggil oleh Player Input component atau event dari Input Action asset
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    void FixedUpdate()
    {
        // Gerak maju konstan
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;

        // Gerak ke samping (kanan/kiri)
        Vector3 sideMove = transform.right * moveInput * moveSpeed * Time.fixedDeltaTime;

        // Gabungkan gerakan dan terapkan ke Rigidbody
        rb.MovePosition(rb.position + forwardMove + sideMove);

        // Batasi posisi mobil agar tidak keluar jalur
        Vector3 currentPos = rb.position;
        currentPos.x = Mathf.Clamp(currentPos.x, initialPosition.x - laneWidth, initialPosition.x + laneWidth);
        transform.position = currentPos;
    }
}