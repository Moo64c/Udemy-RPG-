using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D theRigidbody;
    public Animator playerAnimator;
    public float moveSpeed = 10.0f;
    public static PlayerController instance;
    public string areaTransitionName;
    public bool canMove = true;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
            var horizontalAxis = Input.GetAxisRaw("Horizontal");
            var verticalAxis = Input.GetAxisRaw("Vertical");

        if (canMove)
        {
            theRigidbody.velocity = new Vector2(horizontalAxis * moveSpeed, verticalAxis * moveSpeed);
        }
        else
        {
            theRigidbody.velocity = Vector2.zero;
        }

        playerAnimator.SetFloat("moveX", theRigidbody.velocity.x);
        playerAnimator.SetFloat("moveY", theRigidbody.velocity.y);

        if (canMove && (horizontalAxis == 1 || horizontalAxis == -1 || verticalAxis == 1 || verticalAxis == -1))
        {
            playerAnimator.SetFloat("lastMoveX", horizontalAxis);
            playerAnimator.SetFloat("lastMoveY", verticalAxis);
        }

        // Limit player position to the map.
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z
        );
    }

    public void SetBounds(Vector3 bottomLeftLimit, Vector3 topRightLimit)
    {
        this.bottomLeftLimit    = bottomLeftLimit   + new Vector3(0.5f, 1f, 0);
        this.topRightLimit      = topRightLimit     - new Vector3(0.5f, 1f, 0);
    }
}
