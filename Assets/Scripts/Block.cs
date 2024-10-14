using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Collider2D _collider;
    
    private int _health;

    public void SetupBlock(int health)
    {
        _health = health;
        _collider.enabled = true;
        SetColor();
        
        gameObject.SetActive(true);
    }

    private void ReduceHealth()
    {
        LogManager.Instance.LogHit();
        _health--;

        SetColor();
        if (_health <= 0)
            SetDestroyed();
    }

    private void SetDestroyed()
    {
        GameManager.Instance.CurrentActiveBlocks -= 1;
        LogManager.Instance.LogDestroyed();
        _collider.enabled = false;
    }
    
    private void SetColor()
    {
        float value = _health / 10f;
        value = Mathf.Clamp01(value);
        Color color = new(0.5f, value, value, _health > 0 ? 1 : 0);
        
        _image.color = color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ReduceHealth();
        }
    }
}
