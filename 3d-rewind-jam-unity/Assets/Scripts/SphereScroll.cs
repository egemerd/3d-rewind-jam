using UnityEngine;

public class GrowingRock : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float pushForce = 10f;

    [Header("Büyüme Ayarlarý")]
    public float growthRate = 0.5f;
    public float maxSize = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Baþlangýç hareketi
        rb.AddTorque(Random.insideUnitSphere * pushForce, ForceMode.Impulse);
    }

    void Update()
    {
        // ÖNEMLÝ: Taþ Rewind modunda deðilse (isKinematic false ise) büyüt.
        // Rewind modundaysa büyütme, býrak Rewind scripti küçültsün.
        if (!rb.isKinematic)
        {
            if (transform.localScale.x < maxSize)
            {
                transform.localScale += Vector3.one * growthRate * Time.deltaTime;
            }
        }
    }
}