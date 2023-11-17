using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TestNetworkPlayer : NetworkBehaviour
{
    private NetworkVariable<Vector2> moveInput = new NetworkVariable<Vector2>();
    private float _speed = 0.01f;

    void Start()
    {
        moveInput.OnValueChanged = (p, c) =>
        {

        };
    }

    void Update()
    {
        if (IsOwner)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            SetMoveInputServerRpc(inputX, inputY);
        }

        if (IsServer)
        {
            transform.position += new Vector3(moveInput.Value.x, 0, moveInput.Value.y) * _speed;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetMoveInputServerRpc(float x, float y)
    {
        this.moveInput.Value = new Vector2(x, y);
    }
}
