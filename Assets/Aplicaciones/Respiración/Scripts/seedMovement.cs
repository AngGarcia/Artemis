using System.Threading.Tasks;
using UnityEngine;

public class seedMovement : MonoBehaviour
{

    private Vector2 _direction;
    private Vector2 _mousePosDown;
    private Vector2 _mousePosUp;

    private Rigidbody2D _rigidbody;
    private ConstantForce2D _force;
    private Vector3 _originalPos;
    private Quaternion _originalRot;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _force = GetComponent<ConstantForce2D>();

        _originalPos = transform.position;
        _originalRot = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePosDown = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mousePosUp = Input.mousePosition;

            _direction = _mousePosUp - _mousePosDown;
            //Debug.Log("Direction: " + direction);

            Vector2 variation = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-50.0f, 50.0f));
            //Debug.Log("Variation: " + variation);

            Vector2 finalDirection = _direction + variation;

            _force.enabled = true;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.AddForce(finalDirection * 50 * Time.deltaTime);
        }
    }

    public void Reload()
    {
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.velocity = Vector3.zero;
        _force.enabled = false;

        transform.position = _originalPos;
        transform.rotation = _originalRot;
    }
}
