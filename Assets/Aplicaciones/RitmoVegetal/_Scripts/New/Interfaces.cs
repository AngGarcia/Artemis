using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable { }

public interface IRotational : IInteractable {
    public Quaternion Rotate(Quaternion actualRotation) {
        return actualRotation;
    }
}

public interface ITranslational : IInteractable {
    public Vector3 Translate(Vector3 actualPosition) {
        return actualPosition;
    }
}