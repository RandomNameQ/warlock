using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayZoneController : NetworkBehaviour
{
    [SerializeField]
    private float _tickBetweenDecrease = 10f;
    
    [SerializeField]
    private float _step=0.01f;

    private float _timer;

    private void Update()
    {
        if (transform.localScale.x<=0)
            return;
        
        _timer += Time.deltaTime;

        if (_timer >= _tickBetweenDecrease)
        {
            DecreaseRockPosition();
            _timer = 0f;
        }
    }

    private void DecreaseRockPosition()
    {
        Vector3 currentScale = transform.localScale;
        Vector3 stepToChange = new Vector3(_step,0,_step);
        Vector3 nextScale = currentScale - stepToChange;
        nextScale.y =1;

        transform.localScale = nextScale;
    }

}
