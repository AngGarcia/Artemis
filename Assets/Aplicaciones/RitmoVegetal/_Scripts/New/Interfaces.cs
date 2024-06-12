using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable { }

public interface IRotational : IInteractable {
    public void Rotate();
}

public interface ITranslational : IInteractable {
    public void Translate();
}