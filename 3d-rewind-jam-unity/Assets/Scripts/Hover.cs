using UnityEngine;
using UnityEngine.EventSystems; // Mouse olaylarý için þart

// IPointerEnter (Mouse girdi) ve IPointerExit (Mouse çýktý) özelliklerini ekliyoruz
public class VolumeHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject sliderObj; // Açýlýp kapanacak slider objesi

    // Mouse kutunun sýnýrlarýna girdiði an çalýþýr
    public void OnPointerEnter(PointerEventData eventData)
    {
        sliderObj.SetActive(true);
    }

    // Mouse kutudan çýktýðý an çalýþýr
    public void OnPointerExit(PointerEventData eventData)
    {
        sliderObj.SetActive(false);
    }
}