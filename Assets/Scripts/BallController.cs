using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class BallController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    [SerializeField] LineRenderer aimLine;
    [SerializeField] Transform aimWorld;
    [SerializeField] Slider powerSlider;
    [SerializeField] GameObject fillSlider;
    [SerializeField] GameObject readyIndicator;
    bool shoot;
    bool shootingMode;
    float forceFactor;
    Vector3 forceDirection;
    Ray ray;
    Plane plane;

    int shootCount;

    public bool ShootingMode { get => shootingMode;}
    public int ShootCount { get => shootCount; }

    public UnityEvent<int> onBallShooted = new UnityEvent<int>();
    public UnityEvent OnShootSound;

    private void Start() {
        readyIndicator.SetActive(false);
    }

    private void Update() {
        if (shootingMode)
        {
            if (Input.GetMouseButtonDown(0))
            { 
                aimLine.gameObject.SetActive(true);
                aimWorld.gameObject.SetActive(true);
                fillSlider.SetActive(true);
                plane = new Plane(Vector3.up, this.transform.position);
                readyIndicator.SetActive(false);
            }
            else if (Input.GetMouseButton(0))
            {
                // force direction
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                plane.Raycast(ray, out var distance);
                forceDirection = this.transform.position - ray.GetPoint(distance);
                forceDirection.Normalize();

                // force factor
                var mouseViewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                var ballViewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
                var pointerDirection = ballViewportPos - mouseViewportPosition;
                pointerDirection.z = 0;
                pointerDirection *= Camera.main.aspect;
                pointerDirection.z = Mathf.Clamp(pointerDirection.z, -0.5f, 0.5f);
                forceFactor = pointerDirection.magnitude * 2;
                
                //aim visual
                aimWorld.transform.position = this.transform.position;
                aimWorld.forward = forceDirection;         
                aimWorld.localScale = new Vector3(1, 1, 0.5f + forceFactor);      
                
                var ballScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
                var mouseScreenPos = Input.mousePosition;
                ballScreenPos.z = 1f;
                mouseScreenPos.z = 1f;
                var positions = new Vector3[]
                {
                    Camera.main.ScreenToWorldPoint(ballScreenPos),
                    Camera.main.ScreenToWorldPoint(mouseScreenPos)
                };
                aimLine.SetPositions(positions);
                aimLine.endColor = Color.Lerp(Color.green, Color.red, forceFactor);

                // power slider
                powerSlider.value = forceFactor;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                shoot = true;
                shootingMode = false;
                aimLine.gameObject.SetActive(false);
                aimWorld.gameObject.SetActive(false);
                powerSlider.value = 0;
                fillSlider.SetActive(false);
                OnShootSound.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                shootingMode = false;
                aimLine.gameObject.SetActive(false);
                aimWorld.gameObject.SetActive(false);
                powerSlider.value = 0;
                fillSlider.SetActive(false);
            }
        }
    }

    private void FixedUpdate() 
    {
        if (shoot)
        {
            shoot = false;
            AddForce(forceDirection * force * forceFactor, ForceMode.Impulse);
            shootCount += 1;
            onBallShooted.Invoke(shootCount);
            readyIndicator.SetActive(false);
        }

        else 
        {
            if (rb.velocity.sqrMagnitude > 0.01f)
            {
                rb.velocity *= 0.99f;
            }
            else if (!IsMove())
            {
                readyIndicator.SetActive(true);
                readyIndicator.transform.position = transform.position + new Vector3(0, -0.45f, 0);
                readyIndicator.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }

        if (rb.velocity.sqrMagnitude < 0.01f && rb.velocity.sqrMagnitude > 0) 
        {
            rb.velocity = Vector3.zero;
            //rb.useGravity = false;
            rb.angularVelocity = Vector3.zero;
        }
        
        else if (rb.velocity.sqrMagnitude > 0.01f)
        {
            rb.useGravity = true;
        }
    }

    public bool IsMove()
    {
        return rb.velocity.sqrMagnitude > 0.01f;
    }

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Impulse)
    {
        rb.useGravity = true;
        rb.AddForce(force, forceMode);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(this.IsMove())
        {
            return;
        }
        
        shootingMode = true;
    }
}
