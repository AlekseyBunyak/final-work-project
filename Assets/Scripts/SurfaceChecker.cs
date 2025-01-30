using UnityEngine;

public class SurfaceChecker : MonoBehaviour
{
    [Header("Vars")]
    [SerializeField] private float _jumpOffset;

    [Header("Settings")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundChecker;        

    private bool _isGrounded;
    private bool _CanJumpDown;

    private GameObject _platform;

    public bool IsGrounded => _isGrounded;
    public bool CanJumpDown => _CanJumpDown;
    public GameObject Platform { get { return _platform; } set { _platform = value; } }

    private void Update()
    {
        Vector2 overlapCirclePosition = _groundChecker.position;
        _isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, _jumpOffset, _groundMask);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && _isGrounded) 
        {
            _CanJumpDown = true;

            _platform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _CanJumpDown = false;
    }
}
