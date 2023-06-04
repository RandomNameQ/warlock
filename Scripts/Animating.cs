using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class Animating : NetworkBehaviour
{
    private Animator _animator;
    private void Awake() {
        _animator = GetComponent<Animator>();
    }

   
    public void SetMoving(bool value){
        _animator.SetBool("Moving", value);
    }

}
