using UnityEngine;
public class BossAI1 : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]						
    public Transform player;                               
    public GameObject prefab;								
    public GameObject boss;                              
    public Transform bossLocation;                           
    public bool bossActive = false;							    
    [Header("Base Stats Settings")]						        
    public float bulletSpeed = 5f;							       
    public float shootDelay = 0.5f;							     
    public float bulletLifetime = 10.0f;			
    float shootTimer = 0f;                                        
    [Header("Wave Time Settings")] 								 
    public int firstWave = 0;									
    public int secondWave = 5;									
    public int thirdWave = 15;									 
    public int fourthWave = 25;                                  
    public int reset = 35;                                    
    public int bossPowerUp = 25;						
    float waveTimer = 0f;                                   
    [Header("Shoot Pattern Settings")]                         
    public Vector3 startPoint;                                 
    public int numberOfBullets = 15;                          
    public float radius = 5f;                             
    float shootPatternTimer1Var1 = 0f;                         
    float shootPatternTimer1Var2 = 0f;
    #endregion
    //UNITY FUNCTIONS
    #region UPDATE FUNCTION
    void Update()
    {
        if (bossActive == true)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer > firstWave && waveTimer < secondWave)
                Shoot();
            else if (waveTimer > secondWave && waveTimer < thirdWave)
            {
                ShootPatternOneVar1(numberOfBullets);
                ShootPatternOneVar2(numberOfBullets);
            }
            else if (waveTimer > thirdWave && waveTimer < fourthWave)
                Shoot();
            else if (waveTimer > fourthWave && waveTimer < reset)
            {
                Shoot();
                ShootPatternOneVar1(numberOfBullets);
                ShootPatternOneVar2(numberOfBullets);
            }
            else
                waveTimer = 0;
        }
    }
    #endregion
    //BOSS FUNCTIONS
    #region SHOOT FUNCTION
    void Shoot()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootDelay)
        {
            shootTimer = 0;
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            Vector3 playerPosition = player.position;
            Vector2 shootDir = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
            shootDir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
    #endregion
    #region SHOOT PATTERN ONE VAR ONE FUNCTION
    void ShootPatternOneVar1(int numberOfBullets)
    {
        shootPatternTimer1Var1 += Time.deltaTime;
        if (shootPatternTimer1Var1 > 0.5f)
        {
            startPoint = boss.GetComponent<Transform>().position;
            float angleStep = 360f / numberOfBullets;
            float angle = 0f;
            for (int i = 0; i <= numberOfBullets - 1; i++)
            {
                shootPatternTimer1Var1 = 0;
                float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
                Vector3 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * bulletSpeed;
                var bullet = Instantiate(prefab, startPoint, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
                Destroy(bullet, bulletLifetime);
                angle += angleStep;
            }
        }
    }
    #endregion
    #region SHOOT PATTERN ONE VAR TWO FUNCTION
    void ShootPatternOneVar2(int numberOfBullets)
    {
        shootPatternTimer1Var2 += Time.deltaTime;
        if (shootPatternTimer1Var2 > 1.25f)
        {
            startPoint = boss.GetComponent<Transform>().position;
            float angleStep = 360f / numberOfBullets;    
            float angle = 180f;
            for (int i = 0; i <= numberOfBullets - 1; i++)
            {
                shootPatternTimer1Var2 = 0;
                float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
                Vector3 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * bulletSpeed;
                var bullet = Instantiate(prefab, startPoint, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
                Destroy(bullet, bulletLifetime);
                angle += angleStep;
            }
        }
    }
    #endregion
}
