using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _projectileSpeed;
    [SerializeField]
    private float _lifetime = 5f;

    public void Initialize(Vector2 direction)
    {
        gameObject.SetActive(true);
        _rigidBody.AddForce(_projectileSpeed * direction, ForceMode2D.Impulse);
        StartCoroutine(LifetimeRoutine());
    }

    public void Terminate()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        transform.position = Vector2.zero;
        _rigidBody.velocity = Vector2.zero;
        GameManager.Instance.ReturnBullet(this);
    }

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        Terminate();
    }
}
