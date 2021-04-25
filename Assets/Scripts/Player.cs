using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Vector2 striveSpeed = Vector2.zero;
    public float speed = 2f;
    public float jumpStrength = 0.1f;
    public float terminalVelocity = -10f;
    public float jumpCooldown = 1f;
    public Vector2 xBounds = Vector2.zero;
    public Vector3 maxAngularVelocity = Vector3.zero;

    public UnityEvent OnFlip;
    public UnityEvent OnHit;


    private Rigidbody rb;
    private Surroundings surr;
    private float h;
    private bool jumpReady = true;
    private bool extraJumpReady = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        surr = transform.GetChild(0).gameObject.GetComponent<Surroundings>();
        SetUpEvent(OnHit);
        SetUpEvent(OnFlip);
        Flip();
        StartCoroutine(ResetJump());
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) && (jumpReady || extraJumpReady) )
        {
            Jump();
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Hit();
        }
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y - h, rb.angularVelocity.z);
        // clamp position
        float clampedX = Mathf.Clamp(transform.position.x, xBounds.x, xBounds.y);
        if (clampedX != transform.position.x)
        {
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            Hit();
        }

        //clamp speed
        float clampedVelocity = Mathf.Clamp(rb.velocity.y, terminalVelocity, 100);
        rb.velocity = new Vector3(rb.velocity.x, clampedVelocity, rb.velocity.z);
    }

    private void Jump()
    {
        Flip();
        OnFlip.Invoke();
        if (extraJumpReady && !jumpReady) StartCoroutine(ResetExtraJump());
        if (jumpReady) StartCoroutine(ResetJump());
    }

    private void Hit()
    {
        OnHit.Invoke();
        UIManager._instance.Hit();
        Flip();
    }

    private void Flip()
    {
        rb.velocity = new Vector2(0, 0);
        Vector3 dir = surr.DirectionToNearestObject();

        if (dir.x > -1 && dir.x < 1)
        {
            if(dir.x < 0) dir = new Vector3(-1f * 3, dir.y, dir.z);
            if(dir.x >= 0) dir = new Vector3(1f * 3, dir.y, dir.z);
        }
        rb.AddForce(-dir.normalized * -jumpStrength);
        rb.angularVelocity = new Vector3(Random.Range(30, maxAngularVelocity.x),
                                                    rb.angularVelocity.y,
                                                    Random.Range(30, maxAngularVelocity.z)); //Jump spin
    }

    private IEnumerator ResetJump()
    {
        jumpReady = false;
        yield return new WaitForSeconds(jumpCooldown);
        jumpReady = true;
    }

    private IEnumerator ResetExtraJump()
    {
        extraJumpReady = false;
        yield return new WaitForSeconds(jumpCooldown);
        extraJumpReady = true;
    }

    public static void SetUpEvent(UnityEvent e)
    {
        if (e == null)
            e = new UnityEvent();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(xBounds.x, transform.position.y, transform.position.z), 0.3f);
        Gizmos.DrawSphere(new Vector3(xBounds.y, transform.position.y, transform.position.z), 0.3f);        
    }
}
