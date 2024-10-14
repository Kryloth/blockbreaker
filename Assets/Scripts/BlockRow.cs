using System.Collections.Generic;
using UnityEngine;

public class BlockRow : MonoBehaviour
{
    [SerializeField]
    private List<Block> _blocks;
    
    public void SetupRow()
    {
        int amount = Random.Range(1, _blocks.Count + 1);
        int health = Random.Range(1, 11);
        GameManager.Instance.CurrentActiveBlocks += amount;
        
        foreach (var block in _blocks)
            block.gameObject.SetActive(false);
        
        for (int i = 0; i < amount; i++)
            _blocks[i].SetupBlock(health);
    }
}
