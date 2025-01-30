using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damageableLayer;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damageHit;

    private bool _hit = false;
    public bool Hit => _hit;

    private void Update()
    {
        if(_lifeTime <= 0) 
        {
            Destroy(gameObject);
        }

        _lifeTime -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _hit = true;

        if (collision.gameObject.layer == _damageableLayer) 
        {
            if (collision.gameObject.GetComponent<HealthManager>() != null) 
            {
                collision.gameObject.GetComponent<HealthManager>().HitDamage(_damageHit);
            }            
        }
    }

    /// <summary>
    /// Destroy bullet
    /// </summary>
    public void DestroyObject()
    {
       Destroy(gameObject);
    }
}
