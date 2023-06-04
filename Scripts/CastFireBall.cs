using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastFireBall : MonoBehaviour
{
    [SerializeField]
    private CharacterController _player;
    private bool _isSkillActivated;
    private Rigidbody rb;
    
    [SerializeField]
    private float _power;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            CastSpeel();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
           _isSkillActivated = false;
        }
        if (_isSkillActivated) { 
            float x= _player.transform.position.x; 
            float y= _player.transform.position.y - _player.transform.position.y; 
            float z= _player.transform.position.z; 
            Vector3 _playerPos = new Vector3(x, y, z);
            rb.velocity = _playerPos*_power;
        }
    }

    private void Start()
    {
       
        
        rb = gameObject.AddComponent<Rigidbody>();
    }

    private void CastSpeel()
    {
        _isSkillActivated = true;
    }
}
