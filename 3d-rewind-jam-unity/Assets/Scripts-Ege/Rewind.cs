using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    [Header("Rewind Settings")]
    [SerializeField] private float recordInterval = 0.02f;
    [SerializeField] private int maxRecordTime = 5;
    [SerializeField] private bool recordVelocity = true;


    private List<PointInTime> pointsInTime;
    private Rigidbody rb;
    private bool isRewinding = false;
    private float recordTimer = 0f;
    private int maxPoints;

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
            // R tuþuna basýldýðýnda rewind baþlat
            if (InputManager.Instance.rewindAction.WasPressedThisFrame())
            {
                StartRewind();
            }

            // R tuþu býrakýldýðýnda rewind durdur
            if (InputManager.Instance.rewindAction.WasReleasedThisFrame())
            {
                StopRewind();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            // Geriye sar
            RewindTime();
        }
        else
        {
            // Normal oyun - pozisyonlarý kaydet
            RecordTime();
        }
    }

    private void RewindTime()
    {
        if (pointsInTime.Count > 0)
        {
            // Son kaydedilen noktayý al
            PointInTime pointInTime = pointsInTime[0];

            // Objeyi o pozisyona taþý
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;

            // Kullanýlan noktayý listeden çýkar
            pointsInTime.RemoveAt(0);
        }
        else
        {
            // Kayýt kalmadýysa rewind'i durdur
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

    private void StartRewind()
    {
        Debug.Log("Rewinding");
        isRewinding = true;
        rb.isKinematic = true;
    }

    private void StopRewind()
    {
        Debug.Log("Stopped Rewinding");
        isRewinding = false;
        rb.isKinematic = false;
    }
}
