using UnityEngine;

[System.Serializable]
public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale; // <-- YENÝ EKLENEN: Boyut verisi

    // Yapýcý metoda scale parametresi de ekledik
    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _scale)
    {
        position = _position;
        rotation = _rotation;
        scale = _scale; // <-- YENÝ EKLENEN
    }
}