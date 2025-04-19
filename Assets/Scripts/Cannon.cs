using UnityEngine;

public class Cannon : MonoBehaviour
{
    Camera cam;
    Rigidbody2D rb;

    [SerializeField] HingeJoint2D[] wheels;
    JointMotor2D motor;

    [SerializeField] float CannonSpeed;
    bool isGameOver;
    bool isMoving;
    Vector2 pos;
    float screenBounds;
    float velocityX;

    void Start()
    {
        cam = Camera.main;

        rb = GetComponent<Rigidbody2D>();
        pos = rb.position;

        motor = wheels[0].motor;

        screenBounds = GameManager.instance.screenWidth - 0.56f;
        
    }

    void Update()
    {
        //Check player input ( hand or mouse drag)
        isMoving = GameManager.instance.isMoving;
        isGameOver = GameManager.instance.isGameOver;
        if (!isGameOver && isMoving)
        {
            pos.x = cam.ScreenToWorldPoint(Input.mousePosition).x;
        }
    }

    void FixedUpdate()
    {
        if (isMoving && !isGameOver)
        {
            rb.MovePosition(Vector2.Lerp(rb.position, pos, CannonSpeed * Time.fixedDeltaTime));

            velocityX = pos.x - rb.position.x;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            velocityX = 0f;
        }
        if (Mathf.Abs(velocityX) > 0.0f && Mathf.Abs(rb.position.x) < screenBounds)
        {
            motor.motorSpeed = velocityX * 150f;
            MotorActivate(true);
        }
        else
        {
            motor.motorSpeed = 0f;
            MotorActivate(false);
        }
    }

    void MotorActivate(bool isActive)
    {
        wheels[0].useMotor = isActive;
        wheels[1].useMotor = isActive;

        wheels[0].motor = motor;
        wheels[1].motor = motor;
    }
}