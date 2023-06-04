using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMovementToUseSpell : MonoBehaviour
{
    [SerializeField]
    private Vector3 _clickPoint;
    private ILookClickPos _lookClickPos;

    [SerializeField]
    private bool _lookToTarget;

    [SerializeField]
    private bool _isClick;

    public bool IsClick
    {
        get { return _isClick; }
        set { _isClick = value; }
    }

    private bool _canShoot;

    private void Awake()
    {
        _lookClickPos = GetComponent<ILookClickPos>();
    }

    private void Update()
    {
        if (_isClick)
        {
            _lookClickPos.CanselSettings(false);
            _clickPoint = GetVectorFromMouse.Take(transform.position.y);
            if (_clickPoint == Vector3.zero)
                return;
            _lookClickPos.ChangeMovePos(_clickPoint);
            
            _isClick = false;
        }

        _lookToTarget = _lookClickPos.FaceToTarget();
    }

    public bool CanShoot()
    {
        return _lookToTarget;
    }

    public void ClarMoveState(bool isNeedStop)
    {
        _lookClickPos.CanselSettings(isNeedStop);
    }
}
