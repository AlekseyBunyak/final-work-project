using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBarManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private Image _manaImage;
    [SerializeField] private Shooter _shooter;

    private float _deductionAmount;
    private float _fillingTime;

    private int _fullammo;

    private void Start()
    {
        _fullammo = _shooter.Ammo;

        _ammoText.text = _fullammo.ToString();

        _manaImage.fillAmount = 1f;

        _deductionAmount = 1f / _fullammo;

        _fillingTime = 1f / _shooter.ReloadTimer;
    }

    private void Update()
    {
        if (_shooter.Ammo <= 0)
        {
            FillImage();
        }       
    }   

    private void OnEnable()
    {
        Shooter.OnShoot += SetAmmoCounter;
    }

    private void OnDisable()
    {
        Shooter.OnShoot -= SetAmmoCounter;
    }

    private void SetAmmoCounter(int ammo) 
    {
        if (_fullammo == ammo)
        {
            _manaImage.fillAmount = 1;

            _ammoText.text = ammo.ToString();
        }
        else 
        {
            _ammoText.text = ammo.ToString();

            _manaImage.fillAmount -= _deductionAmount;
        }       
    }

    private void FillImage()
    {
        if (_manaImage.fillAmount < 1f)
        {
            _manaImage.fillAmount += Time.deltaTime * _fillingTime;
        }        
    }
}
