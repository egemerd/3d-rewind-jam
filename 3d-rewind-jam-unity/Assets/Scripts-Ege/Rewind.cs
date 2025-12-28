using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    [Header("Rewind Settings")]
    [SerializeField] private float recordInterval = 0.02f;
    [SerializeField] private int maxRecordTime = 5;
    [SerializeField] private bool recordVelocity = true;

    [Header("Auto Rewind Settings")]
    [SerializeField] private float autoRewindDuration = 2f; // Kaç saniye geri saracak
    
    [Header("Visibility Rewind")]
    [SerializeField] private WordNumberRewind visibilityRewind;

    private List<PointInTime> pointsInTime;
    private Rigidbody rb;
    private bool isRewinding = false;
    private float recordTimer = 0f;
    private int maxPoints;
    private float rewindTimer = 0f; // Rewind süresi sayacý
    private int targetRewindFrames = 0; // Kaç frame geri gidilecek

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        maxPoints = Mathf.CeilToInt(maxRecordTime / recordInterval);
    }

    private void Update()
    {
        // InputManager'dan rewind input'unu kontrol et
        if (InputManager.Instance != null && InputManager.Instance.rewindAction != null)
        {
            // R tuþuna basýldýðýnda otomatik rewind baþlat
            if (InputManager.Instance.rewindAction.WasPressedThisFrame())
            {
                StartAutoRewind();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            // Geriye sar
            RewindTime();
            
            // Rewind süresini kontrol et
            rewindTimer += Time.fixedDeltaTime;
            
            // Belirlenen süre doldu mu veya frame sayýsýna ulaþýldý mý?
            if (rewindTimer >= autoRewindDuration || targetRewindFrames <= 0)
            {
                StopRewind();
            }
        }
        else
        {
            // Normal oyun - pozisyonlarý kaydet
            RecordTime();
        }
    }

    private void RewindTime()
    {
        if (pointsInTime.Count > 0 && targetRewindFrames > 0)
        {
            // Son kaydedilen noktayý al
            PointInTime pointInTime = pointsInTime[0];

            // Objeyi o pozisyona taþý
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;

            // Kullanýlan noktayý listeden çýkar
            pointsInTime.RemoveAt(0);
            
            // Frame sayacýný azalt
            targetRewindFrames--;
        }
        else
        {
            // Kayýt kalmadýysa veya hedef frame'e ulaþýldýysa durdur
            StopRewind();
        }
    }

    private void RecordTime()
    {
        recordTimer += Time.fixedDeltaTime;

        // Belirtilen aralýkta kaydet
        if (recordTimer >= recordInterval)
        {
            recordTimer = 0f;

            // Yeni nokta kaydet
            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));

            // Liste çok uzunsa eski kayýtlarý sil
            if (pointsInTime.Count > maxPoints)
            {
                pointsInTime.RemoveAt(pointsInTime.Count - 1);
            }
        }
    }

    /// <summary>
    /// Otomatik rewind baþlat - Belirli süre geri sar ve otomatik dur
    /// </summary>
    private void StartAutoRewind()
    {
        if (isRewinding)
        {
            Debug.Log("Rewind zaten aktif!");
            return;
        }
        
        // Kaç frame geri gideceðini hesapla
        targetRewindFrames = Mathf.CeilToInt(autoRewindDuration / recordInterval);
        
        // Mevcut kayýt sayýsýndan fazla geri gidemez
        targetRewindFrames = Mathf.Min(targetRewindFrames, pointsInTime.Count);
        
        if (targetRewindFrames <= 0)
        {
            Debug.Log("Rewind için yeterli kayýt yok!");
            return;
        }
        
        Debug.Log($"Otomatik Rewind baþladý - {autoRewindDuration} saniye ({targetRewindFrames} frame) geri sarýlacak");
        
        isRewinding = true;
        rewindTimer = 0f;
        rb.isKinematic = true;

        if (visibilityRewind != null)
        {
            visibilityRewind.StartRewind();
        }
    }

    private void StopRewind()
    {
        if (!isRewinding) return;
        
        Debug.Log($"Rewind durdu - {rewindTimer:F2} saniye geri sarýldý");
        
        isRewinding = false;
        rewindTimer = 0f;
        targetRewindFrames = 0;
        rb.isKinematic = false;

        if (visibilityRewind != null)
        {
            visibilityRewind.StopRewind();
        }
    }
}
