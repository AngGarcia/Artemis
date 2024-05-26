using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) { if (collision.gameObject.GetComponent<Character>() != null) { GameManager.Instance.ResetTransform(collision.gameObject); } }
}
