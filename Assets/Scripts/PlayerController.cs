using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;
    private Vector3 _direction;

    void Update()
    {
        RotateTurret();
        
        if(Input.GetKeyDown(KeyCode.Space) && GameManager.GameState == GameState.Playing)
            Shoot();
    }

    private void Shoot()
    {
        Projectile bullet = GameManager.Instance.GetBullet();
        bullet.transform.position = _shootPoint.position;
        
        bullet.Initialize(_direction);
        LogManager.Instance.LogShoot();
    }

    private void RotateTurret()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (mousePos - transform.position).normalized;
        
        float angle = VectorToAngle(_direction, 0);
        angle = Mathf.Clamp(angle, 10f, 170f);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    
    private static float VectorToAngle(Vector2 direction, float adjustment = -90f)
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += adjustment;
        if (angle < 0) angle += 360;

        return angle;
    }
}
