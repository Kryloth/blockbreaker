using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    public static GameState GameState => _gameState;

    public int CurrentActiveBlocks
    {
        get => _currentActiveBlocks;
        set
        {
            _currentActiveBlocks = value;
            if(_currentActiveBlocks <= 0)
                SetGameOver();
        }
    }
    
    [SerializeField]
    private Projectile _bulletProjectile;
    [SerializeField]
    private Transform _bulletContainer;
    [SerializeField]
    private List<BlockRow> _blockRows;
    [SerializeField]
    private GameObject _gameOverUI;
    
    private static GameManager _instance;
    private static GameState _gameState;
    
    private Queue<Projectile> _bulletPool;
    private List<Projectile> _activeBullets;
    private int _currentActiveBlocks;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        
        _bulletPool = new();
        _activeBullets = new();
    }

    private void Start()
    {
        SpawnBullets();
        ResetGame();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            ResetGame();
    }

    private void SpawnBullets(int amount = 100)
    {
        for (int i = 0; i < amount; i++)
        {
            Projectile newBullet = Instantiate(_bulletProjectile, _bulletContainer);
            newBullet.gameObject.SetActive(false);
            newBullet.transform.position = Vector2.zero;
            
            _bulletPool.Enqueue(newBullet);
        }
    }

    public Projectile GetBullet()
    {
        if (_bulletPool.TryDequeue(out Projectile bullet))
        {
            _activeBullets.Add(bullet);
            return bullet;
        }
            
        SpawnBullets(10);
        
        bullet = _bulletPool.Dequeue();
        _activeBullets.Add(bullet);
        
        return bullet;
    }

    public void ReturnBullet(Projectile projectile)
    {
        _activeBullets.Remove(projectile);
        _bulletPool.Enqueue(projectile);
    }

    public void SetGameOver()
    {
        _gameState = GameState.GameOver;
        _gameOverUI.gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        _currentActiveBlocks = 0;
        _gameState = GameState.Playing;
        
        LogManager.Instance.ResetGame();
        _gameOverUI.gameObject.SetActive(false);
        
        foreach (var bullet in _activeBullets.ToList())
            bullet.Terminate();
        
        foreach (var blockRow in _blockRows)
            blockRow.SetupRow();
    }
}


public enum GameState
{
    Playing,
    GameOver
}