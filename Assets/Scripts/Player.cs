using System.Collections;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Configuration Parameters
    
    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField] 
    private GameObject laserPrefab;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private float projectileFiringPeriod = 0.1f;
    
    #endregion
    
    #region Private Members

    private float _viewportXMin;
    
    private float _viewportXMax;
    
    private float _viewportYMin;
    
    private float _viewportYMax;
    
    private Coroutine _firingCoroutine;
    
    #endregion

    #region Cached References

    private SpriteRenderer _mySpriteRenderer; 
    
    #endregion
    
    #region Unity Events
    
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
        Fire();
    }
    
    #endregion
    
    #region Private Methods

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
           StopCoroutine(_firingCoroutine); 
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
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
