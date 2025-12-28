using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [Header("Düþecek Olan Taþ")]
    public Rigidbody rockRb; // Taþýn Rigidbody'sini buraya sürükle

    [Header("Efektler (Opsiyonel)")]
    public float downForce = 0f; // Taþý ekstra hýzlý fýrlatmak istersen arttýr

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Sadece Player girdiðinde ve daha önce tetiklenmediyse çalýþsýn
        if (other.CompareTag("Player") && !hasTriggered)
        {
            ActivateTrap();
        }
    }

    void ActivateTrap()
    {
        hasTriggered = true;

        // Taþý serbest býrak
        rockRb.isKinematic = false; // Fizik motorunu devreye sok
        rockRb.useGravity = true;   // Yerçekimini aç

        // Ýstersen taþý aþaðý doðru fiþekle (Daha sert düþsün diye)
        if (downForce > 0)
        {
            rockRb.AddForce(Vector3.down * downForce, ForceMode.Impulse);
        }
    }
}