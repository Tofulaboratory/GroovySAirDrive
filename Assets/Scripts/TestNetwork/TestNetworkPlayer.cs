using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TestNetworkPlayer : NetworkBehaviour
{
    private Vector2 moveInput;

    private float _speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            transform.position += new Vector3(moveInput.x, 0, moveInput.y)*_speed;
        }
    }

    [ServerRpc]
    private void SetMoveInputServerRpc(float x, float y)
    {
        this.moveInput = new Vector2(x, y);
    }
}
