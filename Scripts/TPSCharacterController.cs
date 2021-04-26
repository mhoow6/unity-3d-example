using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform cameraArm;

    [SerializeField]
    private Player characterBody;

    private void Update()
    {
        LookAround();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        // Debug.Log($"x: {x}");

        if (x < 180f)
            x = Mathf.Clamp(x, -1f, 70f);
        if (x > 180f)
            x = Mathf.Clamp(x, 335f, 361f);

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            Vector3 lookForawrd = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForawrd * moveInput.y + lookRight * moveInput.x;

            // �ٶ󺸴� �������� �̵�
            characterBody.rigid.AddForce(moveDir, ForceMode.Impulse);
            Debug.Log($"MoveDir : {moveDir}");
        }
        
        // Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.green);
    }

    // ī�޶� ���� �����Ÿ��� �ΰ� ���� ��ġ �̵�

}
