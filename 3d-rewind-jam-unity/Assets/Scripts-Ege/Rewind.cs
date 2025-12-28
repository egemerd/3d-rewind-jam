using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    [Header("Rewind Settings")]
    [SerializeField] private float recordInterval = 0.02f;
    [SerializeField] private int maxRecordTime = 5;

    [Header("Auto Rewind Settings")]
    [SerializeField] private float autoRewindDuration = 2f;

    [Header("Visibility Rewind")]
    [SerializeField] private WordNumberRewind visibilityRewind;

    private List<PointInTime> pointsInTime;
    private Rigidbody rb;
    public bool isRewinding = false;
    private float recordTimer = 0f;
    private int maxPoints;
    private float rewindTimer = 0f;
    private int targetRewindFrames = 0;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        maxPoints = Mathf.CeilToInt(maxRecordTime / recordInterval);
    }

    private void Update()
    {
        if (InputManager.Instance != null && InputManager.Instance.rewindAction != null)
        {
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
            RewindTime();

            rewindTimer += Time.fixedDeltaTime;

            if (rewindTimer >= autoRewindDuration || targetRewindFrames <= 0)
            {
                StopRewind();
            }
        }
        else
        {
            RecordTime();
        }
    }

    private void RewindTime()
    {
        if (pointsInTime.Count > 0 && targetRewindFrames > 0)
        {
            PointInTime pointInTime = pointsInTime[0];

            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            transform.localScale = pointInTime.scale; // <-- BOYUTU GERÝ YÜKLÜYORUZ

            pointsInTime.RemoveAt(0);

            targetRewindFrames--;
        }
        else
        {
            StopRewind();
        }
    }

    private void RecordTime()
    {
        recordTimer += Time.fixedDeltaTime;

        if (recordTimer >= recordInterval)
        {
            recordTimer = 0f;

            // <-- BOYUTU KAYDEDÝYORUZ (transform.localScale)
            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, transform.localScale));

            if (pointsInTime.Count > maxPoints)
            {
                pointsInTime.RemoveAt(pointsInTime.Count - 1);
            }
        }
    }

    private void StartAutoRewind()
    {
        if (isRewinding) return;

        targetRewindFrames = Mathf.CeilToInt(autoRewindDuration / recordInterval);
        targetRewindFrames = Mathf.Min(targetRewindFrames, pointsInTime.Count);

        if (targetRewindFrames <= 0) return;

        isRewinding = true;
        rewindTimer = 0f;
        rb.isKinematic = true; // Fizik kapanýr, Rewind baþlar

        if (visibilityRewind != null) visibilityRewind.StartRewind();
    }

    private void StopRewind()
    {
        if (!isRewinding) return;

        isRewinding = false;
        rewindTimer = 0f;
        targetRewindFrames = 0;
        rb.isKinematic = false; // Fizik açýlýr, GrowingRock tekrar çalýþmaya baþlar

        if (visibilityRewind != null) visibilityRewind.StopRewind();
    }
}