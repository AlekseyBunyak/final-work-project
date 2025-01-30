using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(SurfaceChecker))]
public class PlayerController : MonoBehaviour
{
    [Header("Vars")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _verticalForceForJumpDown = -0.5f;
    [SerializeField] private float _jumpTimeWithoutGround;

    [SerializeField] private AnimationCurve _horizontalMovementCurve;

    private Shooter _shooter;
    private SurfaceChecker _surfaceChecker;

    private Rigidbody2D _rigidBody;

    private bool _isMoving;
    private bool _canJumpUp;
    private bool _downAnimated;
    private bool _isLadder;
    private bool _cantShoot = false;

    private float _velosityUp;
    private float _gravityScale;

    private int _clicksJumpButton = 0;

    public bool IsMoving => _isMoving;
    public bool DownAnimated => _downAnimated;
    public bool IsLadder { get { return _isLadder; } set { _isLadder = value; } }
    public float VelosityUp => _velosityUp;


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _shooter = GetComponent<Shooter>();
        _surfaceChecker = GetComponent<SurfaceChecker>();

        _gravityScale = _rigidBody.gravityScale;
    }

    private void Update()
    {
        _velosityUp = _rigidBody.velocity.y;

        if (_downAnimated)
        {
            _cantShoot = true;
        }

        if (!_downAnimated && !_isLadder)
        {
            _cantShoot = false;
        }

        JumpWithoutGround();
    }

    #region(public methods)

    /// <summary>
    /// Controls the player's movements, jumps and attacks
    /// </summary>
    /// <param name="direction">Horizontal axis X</param>
    /// <param name="vertical">Vertical axis Y</param>
    /// <param name="buttonPressed">Pressing jump button</param>
    public void Movement(float direction, float vertical, bool buttonPressed)
    {
        HorizontalMovement(direction);

        ActionsOnTheLadder(vertical);

        CheckJumpAnimatedDown(vertical);

        JumpDirection(vertical, buttonPressed);
    }

    /// <summary>
    /// Checking for pressing the shot button, calls the Shoot method.
    /// </summary>
    /// <param name="attackOne">data about pressing the shot button</param>
    public void Attack(bool attackOne) 
    {        
        if (attackOne) 
        {     
            if(!_cantShoot) 
            {
                _shooter.Shoot(transform.localScale.x);
            }           
        }
    }

    #endregion

    #region(private methods)

    private void SetOrientation(float direction)
    {
        Vector2 characterOrientation;

        if (direction > 0)
        {
            characterOrientation = new Vector2(1,1);
        }
        else
        {
            characterOrientation = new Vector2(-1,1);
        }

        transform.localScale = characterOrientation;
    }

    private void JumpDirection(float vertical, bool buttonPressed)
    {
        if (buttonPressed)
        {            

            if (vertical < _verticalForceForJumpDown && _surfaceChecker.CanJumpDown)
            {
                JumpDown();
            }
            else JumpUp();
        }
    }

    private void JumpUp() 
    {
        if(_canJumpUp && _clicksJumpButton < 1) 
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);

            _clicksJumpButton++;
        }       
    }

    private void JumpDown() 
    {
        if (_surfaceChecker.CanJumpDown) 
        {
            StartCoroutine(JumpOff());
        }       
    }

    private void CheckJumpAnimatedDown(float vertical)
    {
        if (vertical < _verticalForceForJumpDown && _surfaceChecker.CanJumpDown)
        {
            _downAnimated = true;
        }
        else 
        {
            _downAnimated = false;
        } 
    }

    private void JumpWithoutGround()
    {
        if (!_surfaceChecker.IsGrounded && _canJumpUp)
        {
            StartCoroutine(DelayBlockJump());
        }

        if (_surfaceChecker.IsGrounded)
        {
            _canJumpUp = true;
        }
    }

    private void ActionsOnTheLadder(float vertical)
    {
        if (_isLadder)
        {
            _rigidBody.gravityScale = 0;

            if (Mathf.Abs(vertical) > 0.2f)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, vertical * _speed);
                _cantShoot = true;
            }
            else if (!_isMoving)
            {
                _rigidBody.velocity = Vector2.zero;
            }
        }
        else
        {
            _rigidBody.gravityScale = _gravityScale;
        }
    }

    private void HorizontalMovement(float direction)
    {
        if (Mathf.Abs(direction) > 0.2f && !_downAnimated)
        {
            SetOrientation(direction);

            _rigidBody.velocity = new Vector2(_horizontalMovementCurve.Evaluate(direction) * _speed, _rigidBody.velocity.y);

            _isMoving = true;
        }
        else _isMoving = false;
    }   

    #endregion

    #region(coroutines)

    IEnumerator JumpOff() 
    {
        BoxCollider2D collider = _surfaceChecker.Platform.gameObject.GetComponent<BoxCollider2D>();

        collider.enabled = false;

        yield return new WaitForSeconds(0.4f);

        collider.enabled = true;
    }
    
    IEnumerator DelayBlockJump() 
    {
        yield return new WaitForSeconds(_jumpTimeWithoutGround);

        _canJumpUp = false;

        _clicksJumpButton = 0;
    }

    #endregion
}
