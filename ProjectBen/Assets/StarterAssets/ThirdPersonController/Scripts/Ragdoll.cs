using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private enum PlayerState
    {
        Normal,
        Ragdoll
    }

    private Rigidbody[] _ragdollRigidbodies;
    private PlayerState _currentState = PlayerState.Normal;
    private Animator _animator;
    private CharacterController _characterController;
    void Awake()
    {
        Physics.IgnoreLayerCollision(3, 6, true);
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        switch (_currentState)
        {
            case PlayerState.Normal:
                NormalBehaviour();
                break;
            case PlayerState.Ragdoll:
                RagdollBehaviour();
                break;
        }
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        _animator.enabled = true;
        _characterController.enabled = true;
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        _animator.enabled = false;
        _characterController.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            EnableRagdoll();
            _currentState = PlayerState.Ragdoll;
        }
    }

    private void NormalBehaviour()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EnableRagdoll();
            _currentState = PlayerState.Ragdoll;
        }
    }
    private void RagdollBehaviour()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            DisableRagdoll();
            _currentState = PlayerState.Normal;
        }
    }
}