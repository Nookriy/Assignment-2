using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public bool Battlemode;
    private float EnemyHP;
    private float EnemyAtk;
    private EnemyMov enemyScript;
    private EnemyMov[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindObjectsOfType<EnemyMov>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startBattlemode(EnemyMov enemy)
    {
        EnemyHP = enemy.HP;
        EnemyAtk = enemy.Damage;
        enemyScript = enemy;
        foreach(EnemyMov enem in enemies)
        {
            enem.isBattlemode = true;
        }

    }
}
