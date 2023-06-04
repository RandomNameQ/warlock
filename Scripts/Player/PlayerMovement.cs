using FishNet;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour, ILookClickPos,IGetPlayerNetwork
{
    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private float angleToRotate = 30f;

    [SerializeField]
    private float randomForce = 10f;

    [SerializeField]
    private Vector3 pointToMove;
    public Vector3 PointToMove { get; set; }

    private Vector3 _lastPointToMove;
    private Rigidbody rb;
    private Camera playerCamera;
    private Animating animating;

    public bool _canPlayerMove;
    public bool _characterTurned;
    public bool _isGetHit;
    public bool _alwaysWalk;

    [SerializeField]
    public bool CanShootInPoint { get; set; }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!IsOwner)
            return;
            var camera = playerCamera.GetComponent<CameraController>();
            camera.UpdateCameraOwner(gameObject);
            
            test();
    }
[ServerRpc]
void test(){
    gameObject.name = "Player"+base.OwnerId;
Debug.Log(gameObject.name);
}
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Screen.SetResolution(800, 600, false);
        playerCamera = Camera.main;
        animating = GetComponent<Animating>();
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        if (_canPlayerMove)
            Move();

        LazyMovement();
        GetPositionToMove();

        if (_isGetHit)
        {
            CancelMovePoint();
            _isGetHit = false;
        }
    }

    private void Move()
    {
       
        Vector3 movementDirection = pointToMove - transform.position;
        movementDirection.y = 0f;
        movementDirection.Normalize();

        if (pointToMove != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            rb.MoveRotation(
                Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime)
            );
            float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
            _characterTurned = angleDifference <= angleToRotate;
            CanShootInPoint = angleDifference <= 20f;
        }

        if (_characterTurned)
        {
            float distanceToTarget = Vector3.Distance(transform.position, pointToMove);
            if (distanceToTarget > 0.1f)
            {
                rb.velocity = movementDirection * movementSpeed;
            }
            else
            {
                rb.velocity = Vector3.zero;
                _canPlayerMove = false;
                _lastPointToMove = Vector3.zero;
            }
        }

        animating.SetMoving(_canPlayerMove);
    }

    private void LazyMovement()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            _alwaysWalk = !_alwaysWalk;
        }
    }

    private void GetPositionToMove()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1) || _alwaysWalk)
        {
            pointToMove = GetVectorFromMouse.Take(transform.position.y);
            if (pointToMove != Vector3.zero)
            {
                _characterTurned = false;
                _canPlayerMove = true;
            }
        }
    }

    private void CancelMovePoint()
    {
        rb.velocity = Vector3.zero;
        pointToMove = transform.position;
    }

    public bool FaceToTarget()
    {
        return CanShootInPoint;
    }

    public void ChangeMovePos(Vector3 newPos)
    {
        pointToMove = newPos;
    }

    public void CanselSettings(bool isNeedNewPos)
    {
        _canPlayerMove = true;
        _characterTurned = false;
        CanShootInPoint = false;
        if (isNeedNewPos)
        {
            StopMovement();
        }
    }

    public void StopMovement()
    {
        _canPlayerMove = false;
        _characterTurned = false;
        _isGetHit = false;
        _alwaysWalk = false;
        pointToMove = transform.position;
        rb.velocity = Vector3.zero;
        animating.SetMoving(_canPlayerMove);
    }

    public NetworkConnection GetCliendId()
    {
        var conn = base.Owner;
        
        return conn;
    }
}
