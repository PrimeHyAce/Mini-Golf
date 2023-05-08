using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Collider col;
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    // [SerializeField] LineRenderer aimLine;
    [SerializeField] Transform aimWorld;
    bool shoot;
    bool shootingMode;
    float forceFactor;

    public bool ShootingMode { get => shootingMode;}

    private void Update() {
        if (shootingMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // aimLine.gameObject.SetActive(true);
                aimWorld.gameObject.SetActive(true);
            }
            else if (Input.GetMouseButton(0))
            {
                var mouseViewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                var ballViewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
                var ballScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
                var pointerDirection = ballViewportPos - mouseViewportPosition;
                pointerDirection.z = 0;

                //draw aim line
                // var positions = new Vector3[]{ballScreenPos, Input.mousePosition};
                // aimLine.positionCount = positions.Length;
                // aimLine.SetPositions(positions);

                //draw aim world
                var aimDirection = new Vector3(pointerDirection.x, 0, pointerDirection.y);
                aimDirection = Camera.main.transform.localToWorldMatrix * aimDirection;
                aimWorld.transform.position = this.transform.position;
                aimWorld.transform.forward = aimDirection;

                //force factor
                forceFactor = Mathf.Clamp01(pointerDirection.magnitude * 2);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                shoot = true;
                shootingMode = false;
                // aimLine.gameObject.SetActive(false);
                aimWorld.gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate() {
        if (shoot)
        {
            shoot = false;
            var direction = Camera.main.transform.forward;
            direction.y = 0;
            rb.AddForce(direction * force * forceFactor, ForceMode.Impulse);
        }

        if(rb.velocity.sqrMagnitude < 0.01f && rb.velocity.sqrMagnitude > 0) 
        {
            rb.velocity = Vector3.zero;
        }
    }

    public bool IsMove()
    {
        return rb.velocity != Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        shootingMode = true;
    }
}
