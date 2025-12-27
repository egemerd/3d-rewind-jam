using UnityEngine;
using UnityEngine.InputSystem;

public class KripteksDurdurucu : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float donmeHizi = 40f; // Görüntüdeki hýzýn

    [Header("Hedef Ayarlarý")]
    public float hedefAci = 0f;   // Küreler hizalandýðýnda açý kaç olmalýysa onu gir (Örn: 0)
    [Range(1f, 45f)]
    public float sapmaPayi = 15f;

    [HideInInspector] // Inspector'da kalabalýk etmesin
    public bool dogruYerdeMi = false;

    private bool durduMu = false;

    void Update()
    {
        // --- 1. DÖNDÜRME KISMI (DÜZELTÝLDÝ: Y EKSENÝ) ---
        if (!durduMu)
        {
            // DÝKKAT: Artýk ortadaki parametreyi (Y) kullanýyoruz
            transform.Rotate(0, donmeHizi * Time.deltaTime, 0);

            dogruYerdeMi = false;
        }

        // --- 2. TUÞ KONTROLÜ (E TUÞU) ---
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            durduMu = !durduMu;

            if (durduMu)
            {
                KontrolEt();
            }
        }
    }

    void KontrolEt()
    {
        // DÝKKAT: Kontrolü de artýk Y açýsýna göre yapýyoruz
        float anlikAci = transform.localEulerAngles.y; // .z yerine .y oldu
        float fark = Mathf.DeltaAngle(anlikAci, hedefAci);

        if (Mathf.Abs(fark) <= sapmaPayi)
        {
            Debug.Log(gameObject.name + " BAÞARILI! Kilitlendi.");
            dogruYerdeMi = true;
        }
        else
        {
            // Görüntüdeki hatayý (OLMADI) burasý veriyor
            Debug.Log(gameObject.name + " OLMADI! Hatalý konum.");
            dogruYerdeMi = false;
        }
    }
}