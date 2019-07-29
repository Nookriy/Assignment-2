using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public bool Battlemode;
    private int EnemyHP;
    private int EnemyAtk;
    private EnemyMov enemyScript;
    private EnemyMov[] enemies;
    private PlayerMov player;
    private GameObject canvas;
    private bool isPlayerTurn;
    private bool EnemyisDefending;
    public Button bAttack;
    public Button bDefense;
    public Button bYMJ;
    private bool isButtonInteract;

    

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindObjectsOfType<EnemyMov>();
        canvas = GameObject.FindGameObjectWithTag("Canvastag");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMov>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Battlemode)
        {
            canvas.SetActive(true);
            actualBattlemode();
        }
        else
        {
            canvas.SetActive(false);
        }

        makeButtonsInteractable();
    }

    public void initializeBattlemode(EnemyMov enemy)
    {
        EnemyHP = enemy.HP;
        EnemyAtk = enemy.Damage;
        enemyScript = enemy;
        foreach(EnemyMov enem in enemies)
        {
            enem.isBattlemode = true;
        }
        player.isBattlemode = true;
        Battlemode = true;
        if (Random.Range(1, 6) > 3)
        {
            isPlayerTurn = false;
        }
        else
        {
            isPlayerTurn = true;
        }
        EnemyisDefending = false;
        player.PlayerisDefending = false;


    }

    public void Attack(string Attacker)
    {
        if (Attacker == "Player")
        {   // Enemy is affected //
            if (EnemyisDefending)
            {
                Debug.Log("Target defended");
                isButtonInteract = false;
                isPlayerTurn = false;
            }
            else
            {
                EnemyHP -= player.PlayerDamage;
                if (EnemyHP <= 0)
                {
                    //Enemy dead// 
                }
                isButtonInteract = false;
                isPlayerTurn = false;
            }
        }
        else
        {   // Player is affected //
            if (player.PlayerisDefending)
            {
                Debug.Log("Target defended");
            }
            else
            {
            player.PlayerHP -= EnemyAtk;
                if (player.PlayerHP <= 0)
                {
                    //Restart Game//
                }
            }
        }
    }

    public void Defense(string Defender)
    {  
        if (Defender == "Player")
        {
            player.PlayerisDefending = true;
            isButtonInteract = false;
            isPlayerTurn = false;
        }
        else
        {
            EnemyisDefending = true;
        }
    }

    public void YMJ(string Attacker)
    {
        if (Attacker == "Player")
        {   // Enemy is affected //
            if (EnemyisDefending)
            {
                Debug.Log("Target defended");
                isButtonInteract = false;
                isPlayerTurn = false;
            }
            else
            {
                EnemyHP -= (player.PlayerDamage * 2);
                isButtonInteract = false;
                isPlayerTurn = false;
                if (EnemyHP <= 0)
                {
                    //Enemy dead// 
                }
            }
        }
        else
        {   // Player is affected //
            if (player.PlayerisDefending)
            {
                Debug.Log("Target defended");
            }
            else
            {
                player.PlayerHP -= (EnemyAtk * 2);
                if (player.PlayerHP <= 0)
                {
                    //Restart Game//
                }
            }
        }
    }

    void actualBattlemode()
    {

        if (isPlayerTurn)
        {
            isButtonInteract = true;
        }
        else
        {
            int i = Random.Range(1, 3);
            if (i == 1)
            {
                Attack("Enemy");
            } 
            else if(i == 2)
            {
                Defense("Enemy");
            }
            else
            {
                YMJ("Enemy");
            }
            isPlayerTurn = true;
        }
        
        
        
        
        // Checking who's turn it is
        // if player's turn, player can choose Atck,YMJ, Defense
        // Enemy turn, random between Atck and Defend
        // Execute, then turn over
        // Check player and enemy status
        // If either dies, execute proper action

        
        //
    }

    void makeButtonsInteractable()
    {
        if (isButtonInteract)
        {
            bAttack.interactable = true;
            bDefense.interactable = true;
            bYMJ.interactable = true;
        }
        else
        {
            bAttack.interactable = false;
            bDefense.interactable = false;
            bYMJ.interactable = false;
        }
    }
 
}
