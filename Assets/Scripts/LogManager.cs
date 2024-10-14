using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    public static LogManager Instance => _instance;
    
    [SerializeField]
    private TMP_Text _logBase;
    [SerializeField]
    private Transform _logContainer;
    [SerializeField]
    private int _maxLogs;
    [SerializeField]
    private ScrollRect _scrollRect;
    
    private Queue<TMP_Text> _logPool;
    private List<TMP_Text> _activeLogs;
    
    private static LogManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        
        _logPool = new();
        _activeLogs = new();
    }

    private void Start()
    {
        SpawnLogPool();
    }

    public void LogShoot()
    {
        string text = $"You <color=#{ColorUtility.ToHtmlStringRGBA(Color.cyan)}> shoot </color> a bullet";
        Log(text);
    }

    public void LogHit()
    {
        string text = $"You <color=#{ColorUtility.ToHtmlStringRGBA(Color.yellow)}> hit </color> a block";
        Log(text);
    }

    public void LogDestroyed()
    {
        string text = $"You <color=#{ColorUtility.ToHtmlStringRGBA(Color.red)}> destroyed </color> a block";
        Log(text);
    }

    private void Log(string text)
    {
        var log = GetLog();
        log.transform.SetAsLastSibling();
        log.text = text;
        
        log.gameObject.SetActive(true);

        _scrollRect.verticalNormalizedPosition = 0;
        CheckMaxLog();
    }

    private void CheckMaxLog()
    {
        if(_activeLogs.Count < _maxLogs)
            return;

        while (_activeLogs.Count > _maxLogs)
            ReturnLog(_activeLogs[0]);
    }

    private void SpawnLogPool(int amount = 50)
    {
        for (int i = 0; i < amount; i++)
        {
            TMP_Text newLog = Instantiate(_logBase, _logContainer);
            _logBase.gameObject.SetActive(false);
            
            _logPool.Enqueue(newLog);
        }
    }
    
    private TMP_Text GetLog()
    {
        if (_logPool.TryDequeue(out TMP_Text log))
        {
            _activeLogs.Add(log);
            return log;
        }
            
        SpawnLogPool(10);
        
        log = _logPool.Dequeue();
        _activeLogs.Add(log);
        return log;
    }

    private void ReturnLog(TMP_Text log)
    {
        log.gameObject.SetActive(false);
        _activeLogs.Remove(log);
        _logPool.Enqueue(log);
    }
    
    public void ResetGame()
    {
        foreach (var log in _activeLogs.ToList())
            ReturnLog(log);
    }
}
