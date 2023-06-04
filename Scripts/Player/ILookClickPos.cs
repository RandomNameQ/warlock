using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILookClickPos { 
    bool FaceToTarget();
    void ChangeMovePos(Vector3 newPos);

    void CanselSettings(bool isNeedStop);
}
