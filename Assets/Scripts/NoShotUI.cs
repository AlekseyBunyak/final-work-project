using UnityEngine;
using UnityEngine.EventSystems;

public class NoShotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Shooter _shooter;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_shooter != null) 
        {
            _shooter.NoShot = true;
        }        
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        if (_shooter != null)
        {
            _shooter.NoShot = false;
        }
    }
}
