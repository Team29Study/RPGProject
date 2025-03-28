using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    // smoothDamp 시간
    [SerializeField] private float drag = 0.3f;

    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    private Vector3 dampingVelocity;
    private Vector3 impact;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    // 이전의 값들이 남아있어서 영향을 받지 않게 Reset
    public void Reset()
    {
        verticalVelocity = 0;
        impact = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
