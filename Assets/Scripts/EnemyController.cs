using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _EnemySpeed;
    [SerializeField] private float _radiusDemageZone;
    [SerializeField] private Transform[] _routePoints;
    [SerializeField] private LayerMask _layerPlayer;

    private Vector3 _nextTarget;
    private Vector3 _curentTarget;
    private Vector3 _defaultPosition;

    private EnemyStatus _status = EnemyStatus.Idle;

    private float _destenationOffset = 0.5f;
    private float _time = 3f;

    private void Start()
    {        
        _defaultPosition = transform.position;

        _curentTarget = _routePoints[0].position;
        _nextTarget = _routePoints[1].position;
    }

    private void Update()
    {
        bool playerInZone = Physics2D.OverlapCircle(transform.position, _radiusDemageZone, _layerPlayer);

        switch (_status) 
        {
            case EnemyStatus.Idle: 
                {
                    if(_time <= 0) 
                    {
                        _status = EnemyStatus.Patrol;
                        _time = 3f;
                    }
                    else _time -= Time.deltaTime;
                }
                return;

            case EnemyStatus.Patrol:
                {
                    if (Vector3.Distance(transform.position, _curentTarget) < _destenationOffset)
                    {
                        Vector3 target = _curentTarget;
                        _curentTarget = _nextTarget;
                        _nextTarget = target;

                        _status = EnemyStatus.Idle;
                    }
                    else MoveToTarget(_curentTarget);
                }
                return;

            case EnemyStatus.Follow:
                {
                    if (playerInZone)
                    {
                        _status = EnemyStatus.Attack;
                    }
                    else MoveToTarget(_curentTarget);
                }
                return;

            case EnemyStatus.Attack:
                {
                    if (playerInZone)
                    {
                        Debug.Log("Атакую");                       
                    }
                    else _status = EnemyStatus.Follow;
                }

                return;

            case EnemyStatus.MoveDefault:
                {
                    if (Vector3.Distance(transform.position, _defaultPosition) < _destenationOffset)
                    {
                        _status = EnemyStatus.Idle;

                        _curentTarget = _routePoints[0].position;
                        _nextTarget = _routePoints[1].position;
                    }
                    else MoveToTarget(_defaultPosition);
                }

                return;

        }
    }

    private void MoveToTarget(Vector2 target) 
    {
       transform.position = Vector2.MoveTowards(transform.position, target, _EnemySpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(_status != EnemyStatus.Attack) 
            {
                _status = EnemyStatus.Follow;

                _curentTarget = new Vector2(collision.transform.position.x, transform.position.y);
            }           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            _status = EnemyStatus.MoveDefault;
        }       
    }

    enum EnemyStatus 
    {
        Idle,
        Patrol,
        MoveDefault,
        Attack,
        Follow
    }
}
