using System.Collections.Generic;
using UnityEngine;

public class WordNumberRewind : MonoBehaviour
{
    [Header("Rewind Settings")]
    [SerializeField] private float recordInterval = 0.02f; // Her 0.02 saniyede kaydet
    [SerializeField] private int maxRecordTime = 5; // 5 saniye kayýt

    [Header("Objects to Track")]
    [SerializeField] private GameObject[] objectsToTrack; // Ýzlenecek objeler

    private List<VisibilitySnapshot> snapshots;
    private bool isRewinding = false;
    private float recordTimer = 0f;
    private int maxSnapshots;

    private void Start()
    {
        snapshots = new List<VisibilitySnapshot>();
        maxSnapshots = Mathf.CeilToInt(maxRecordTime / recordInterval);

        if (objectsToTrack == null || objectsToTrack.Length == 0)
        {
            Debug.LogWarning("WordNumberRewind: Hiç obje atanmamýþ!");
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            RewindVisibility();
        }
        else
        {
            RecordVisibility();
        }
    }

    /// <summary>
    /// Tüm objelerin görünürlüðünü kaydet
    /// </summary>
    private void RecordVisibility()
    {
        recordTimer += Time.fixedDeltaTime;

        if (recordTimer >= recordInterval)
        {
            recordTimer = 0f;

            // Yeni snapshot oluþtur
            VisibilitySnapshot snapshot = new VisibilitySnapshot();
            snapshot.timestamp = Time.time;
            snapshot.objectStates = new bool[objectsToTrack.Length];

            // Her objenin durumunu kaydet
            for (int i = 0; i < objectsToTrack.Length; i++)
            {
                if (objectsToTrack[i] != null)
                {
                    snapshot.objectStates[i] = objectsToTrack[i].activeSelf;
                }
            }

            // Liste baþýna ekle (en yeni önde)
            snapshots.Insert(0, snapshot);

            // Maksimum limit aþýlýrsa eski kayýtlarý sil
            if (snapshots.Count > maxSnapshots)
            {
                snapshots.RemoveAt(snapshots.Count - 1);
            }
        }
    }

    /// <summary>
    /// Kayýtlý görünürlüðü geri sar
    /// </summary>
    private void RewindVisibility()
    {
        if (snapshots.Count > 0)
        {
            VisibilitySnapshot snapshot = snapshots[0];

            // Her objeyi kayýtlý duruma getir
            for (int i = 0; i < objectsToTrack.Length; i++)
            {
                if (objectsToTrack[i] != null && i < snapshot.objectStates.Length)
                {
                    objectsToTrack[i].SetActive(snapshot.objectStates[i]);
                }
            }

            // Kullanýlan snapshot'ý sil
            snapshots.RemoveAt(0);
        }
        else
        {
            // Kayýt kalmadýysa rewind'i durdur
            StopRewind();
        }
    }

    /// <summary>
    /// Rewind'i baþlat (InputManager'dan veya baþka yerden çaðrýlýr)
    /// </summary>
    public void StartRewind()
    {
        if (isRewinding) return;

        isRewinding = true;
        Debug.Log("WordNumberRewind baþladý");
    }

    /// <summary>
    /// Rewind'i durdur
    /// </summary>
    public void StopRewind()
    {
        if (!isRewinding) return;

        isRewinding = false;
        Debug.Log("WordNumberRewind durdu");
    }

    /// <summary>
    /// Rewind durumunu döndür
    /// </summary>
    public bool IsRewinding()
    {
        return isRewinding;
    }

    /// <summary>
    /// Kayýt sayýsýný döndür (UI için)
    /// </summary>
    public int GetSnapshotCount()
    {
        return snapshots.Count;
    }

    /// <summary>
    /// Rewind yüzdesini döndür (0-1)
    /// </summary>
    public float GetRewindPercentage()
    {
        return (float)snapshots.Count / maxSnapshots;
    }
}

/// <summary>
/// Bir anýn obje durumlarýný tutan yapý
/// </summary>
[System.Serializable]
public class VisibilitySnapshot
{
    public float timestamp;
    public bool[] objectStates; // Her objenin aktif/pasif durumu
}

