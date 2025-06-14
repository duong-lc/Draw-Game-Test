using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.Assertions;

public class Drawer : MonoBehaviour
{
    private const int PRIMARY_TOUCH_INDEX = 0;
    
    [Header("Reference")]
    [SerializeField] private GameObject linePrefab;
    
    [Header("Parameters")]
    [SerializeField] private float segmentSpacing;
    [SerializeField] private float circleColRadius = 0.08f;
    [SerializeField] private float segmentThickness = 0.1f;

    private Camera _cam;
    private int _segmentCount;
    private bool _isDragging;
    private LineRenderer _line;
    
    private GameObject _currentObject;
    private EdgeCollider2D _collider;
    private Rigidbody2D _rb;
    private LineToPolygon2D _colliderMaker = new();

    private Touch GetInput() => Input.GetTouch(PRIMARY_TOUCH_INDEX);
    
    private void Start()
    {
        _cam = Camera.main;
        _segmentCount = 0;
    }

    private void Update()
    {
        if (!IsTouching()) return;
        CheckDrawStart();
        CheckDrawDrag();
        CheckDrawEnd();
    }
    
    private void CheckDrawStart()
    {
        if (GetInput().phase != TouchPhase.Began) return;
        Assert.IsTrue(_segmentCount == 0);
        _isDragging = true;
            
        var pos3d     = GetTouchWorldPosition(GetInput().position);
        _currentObject = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        _line          = _currentObject.GetComponent<LineRenderer>();
        _collider      = _currentObject.GetComponent<EdgeCollider2D>();
        _rb            = _currentObject.GetComponent<Rigidbody2D>();
        
        _colliderMaker.Initialize(circleColRadius, segmentThickness, _line, _collider);
        _rb.simulated = false;
        _line.positionCount = 0; // reset size
    }

    private void CheckDrawDrag()
    {
        if (!_isDragging) return;
        var pos3d = GetTouchWorldPosition(GetInput().position);

        if (!CanNewSegment(pos3d) && _segmentCount != 0) return;
        _segmentCount       += 1;
        _line.positionCount =  _segmentCount;
        _line.SetPosition(_segmentCount - 1, pos3d);
    }
    
    private void CheckDrawEnd()
    {
        if (GetInput().phase != TouchPhase.Ended) return;
        if(TryBakeCollider()) 
            _rb.simulated = true;
        else
            Destroy(_currentObject);
        
        _segmentCount  = 0;
        _isDragging    = false;
        _currentObject = null;
        _collider      = null;
        _line          = null;
        _colliderMaker.DeleteRef();
    }

    private bool TryBakeCollider()
    {
        // allow to bake
        if (_segmentCount != 0)
        {
            _colliderMaker.Bake();
            return true;
        }

        return false;
    }

    private bool CanNewSegment(Vector3 pos3d) => _line.positionCount == 0 ||
                                                 Vector3.Distance(_line.GetPosition(_segmentCount - 1), pos3d) > segmentSpacing;

    private bool IsTouching() => Input.touchCount > 0;
    
    private Vector3 GetTouchWorldPosition(Vector2 screenPos)
    {
        var vect = _cam.ScreenToWorldPoint(screenPos);
        vect.z = 0;
        return vect;
    }
}
