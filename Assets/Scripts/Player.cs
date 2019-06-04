﻿using UnityEngine;

public class Player : MonoBehaviour
{
    #region Configuration Parameters
    
    [SerializeField]
    private float movementSpeed = 10f;
    
    #endregion
    
    #region Private Members

    private float _viewportXMin;
    
    private float _viewportXMax;
    
    private float _viewportYMin;
    
    private float _viewportYMax;
    
    #endregion

    #region Cached References

    private SpriteRenderer _mySpriteRenderer; 
    
    #endregion
    
    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        _mySpriteRenderer = FindObjectOfType<SpriteRenderer>();
        InitializeMoveBoundaries();
    }

    // Update is called once per frame
    private void Update()
    {
        DetectMovement();
    }

    private void InitializeMoveBoundaries()
    {
        var shipSize = _mySpriteRenderer.size;
        var shipWidth = shipSize.x;
        var shipHeight = shipSize.y;
        
        var gameCamera = Camera.main;
        if (gameCamera != null)
        {
            _viewportXMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + shipWidth / 2;
            _viewportXMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - shipWidth / 2;

            _viewportYMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + shipHeight / 2;
            _viewportYMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - shipHeight / 2;
        }
    }

    private void DetectMovement()
    {
        var oldPosition = transform.position;
        
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        
        var newXPosition = Mathf.Clamp(oldPosition.x + deltaX, _viewportXMin, _viewportXMax);
        var newYPosition = Mathf.Clamp(oldPosition.y + deltaY, _viewportYMin, _viewportYMax);
        
        var newPosition = new Vector2(newXPosition, newYPosition);
        
        transform.position = newPosition;
    }
    
    #endregion
}