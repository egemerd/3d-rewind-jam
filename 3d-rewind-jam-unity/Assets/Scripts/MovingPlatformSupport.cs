using UnityEngine;

public class MovingPlatformSupport : MonoBehaviour
{
    private CharacterController controller;
    private Transform activePlatform; // Þu an üstünde durduðumuz obje
    private Vector3 lastPlatformPosition; // Durduðumuz objenin bir önceki karedeki pozisyonu

    [Header("Ayarlar")]
    // Hangi layer'lara yapýþalým? (Genelde Default ve Ground seçili olmalý, Player seçili OLMAMALI)
    public LayerMask stickToLayer;
    public float rayLength = 0.5f; // Ayaktan aþaðý ne kadar ýþýn atalým?

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update'in en baþýnda çalýþýr. Platform hareket ettiyse biz de edelim.
    void Update()
    {
        // Eðer bir platformun üstündeysek
        if (activePlatform != null)
        {
            // Platformun þu anki pozisyonu ile önceki pozisyonu arasýndaki farký bul
            Vector3 positionDiff = activePlatform.position - lastPlatformPosition;

            // Eðer platform hareket etmiþse
            if (positionDiff != Vector3.zero)
            {
                // Bizi de o fark kadar taþý (controller.Move çarpýþmalarý da hesaplar)
                controller.Move(positionDiff);
            }

            // Bir sonraki kare için þu anki pozisyonu kaydet
            lastPlatformPosition = activePlatform.position;
        }
    }

    // Tüm hareketler bittikten sonra bir sonraki kare için zemin kontrolü yapalým
    void LateUpdate()
    {
        RaycastHit hit;
        // Karakterin hafif içinden baþlayýp aþaðý doðru kýsa bir ýþýn atýyoruz
        // Vector3.up * 0.1f -> Hafif yukarýdan baþla ki zemin içine gömülüyse de algýla
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, rayLength, stickToLayer))
        {
            // Eðer yeni bir platforma bastýysak veya ilk kez basýyorsak
            if (hit.transform != activePlatform)
            {
                activePlatform = hit.transform;
                lastPlatformPosition = activePlatform.position;
                // Debug.Log("Platforma yapýþtým: " + activePlatform.name); // Test için açabilirsin
            }
        }
        else
        {
            // Havadaysak platformu unut
            activePlatform = null;
        }
    }

    // Iþýný sahnede görmek için (Gizmos açýkken)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * rayLength);
    }
}