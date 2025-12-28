using UnityEngine;

public class StickyMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 groundVelocity;

    // Karakterin ne kadar alt�n� kontrol edelim?
    public float rayLength = 1.2f;

    // Hangi Layer'lara yap��s�n? (Default ve Ground genelde yeterli)
    public LayerMask stickToLayer;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {
        // 1. Karakter yere de�iyor mu kontrol et
        // (Character Controller'�n kendi isGrounded'� bazen ge� kalabiliyor, Raycast en temizi)
        RaycastHit hit;

        // Karakterin merkezinden a�a�� do�ru ���n at�yoruz
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, rayLength, stickToLayer))
        {
            // 2. Bast���m�z �eyin bir Rigidbody'si var m�?
            if (hit.collider.attachedRigidbody != null)
            {
                // 3. O objenin h�z�n� al (Hem d��me h�z� hem yatay h�z)
                groundVelocity = hit.collider.attachedRigidbody.linearVelocity;

                // 4. E�er obje hareket ediyorsa bizi de o y�nde ta��
                // (Y eksenini bazen iptal etmek gerekebilir ama yuvarlanan ta�ta kals�n)
                if (groundVelocity.magnitude > 0.1f)
                {
                    controller.Move(groundVelocity * Time.deltaTime);
                }
            }
        }
    }

    // Gizmos ile ���n� sahnede g�r (Debug i�in)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + Vector3.down * rayLength);
    }
}