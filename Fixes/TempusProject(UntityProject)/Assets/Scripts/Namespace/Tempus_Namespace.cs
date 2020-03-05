using UnityEngine;
namespace Tempus
{
    //CLASSES
    #region STATS CLASS
    public static class Stats
    {
        //BASE STATS
        public static Stats4 BaseStats = new Stats4(35, 40, 40, 10);
        //PLAYER STATS FUNCTIONS
        #region HEALTH CALC FUNCTION
        public static int HealthCalc(int level)
        {
            int baseHealth = BaseStats.health;
            int health = Mathf.RoundToInt((2 * baseHealth) * level / 100 + level + 10);
            return health;
        }
        #endregion
        #region ATTACK CALC FUNCTION
        public static int AttackCalc(int level)
        {
            int baseAttack = BaseStats.attack;
            int attack = Mathf.RoundToInt((3 * baseAttack) * level / 100 + level + 5);
            return attack;
        }
        #endregion
        #region DEFENSE CALC FUNCTION
        public static int DefenseCalc(int level)
        {
            int baseDefense = BaseStats.defense;
            int defense = Mathf.RoundToInt((2 * baseDefense) * level / 100 + level + baseDefense);
            return defense;
        }
        #endregion
        #region SPEED CALC FUNCTION
        public static float SpeedCalc(int tempusLevel)
        {
            float speed = (tempusLevel * 10) / 5 + 10;
            return speed;
        }
        #endregion
        //ENEMY STATS FUNCTIONS
        #region ENEMY HEALTH INCREASE FUNCTION
        public static int EnemyHealthIncrease(int baseHealth, int level)
        {
            int health = Mathf.RoundToInt((2 * baseHealth) * level / 100 + level + 10);
            return health;
        }
        #endregion
        #region ENEMY ATTACK INCREASE FUNCTION
        public static int EnemyAttackIncrease(int baseAttack, int level)
        {
            int attack = Mathf.RoundToInt((3 * baseAttack) * level / 100 + level + 5);
            return attack;
        }
        #endregion
        #region ENEMY DEFENSE INCREASE FUNCTION
        public static int EnemyDefenseIncrease(int baseDefense, int level)
        {
            int defense = Mathf.RoundToInt((2 * baseDefense) * level / 100 + level);
            return defense;
        }
        #endregion
        #region ENEMY SPEED INCREASE FUNCTION
        public static float EnemySpeedIncrease(int tempusLevel)
        {
            float speed = (tempusLevel * 10) / 5 + 10;
            return speed;
        }
        #endregion
    }
    #endregion
    #region CALC CLASS
    public static class Calc
    {
        #region DAMAGE FUNCTION
        public static int Damage(int level, int attack, int enemyDefense, int power)
        {
            int damage = ((2 * level / 5 + 2) * power * attack/enemyDefense / 50 + 2);
            return damage;
        }
        #endregion
        #region DEFENSE FUNCTION
        public static int Defense(int enemyAttack, int defense, int dull)
        {
            int newDefense = defense + (Mathf.RoundToInt(defense / dull));
            return enemyAttack - newDefense;
        }
        #endregion
    }
    #endregion
    //STRUCTS
    #region ENEMY BATTLE DATA STRUCT
    /// <summary>
    /// This data is grabbed everytime the player enters a battle
    /// It translates the enemy's level into its corresponding stats
    /// It turns a bool true to signify that a battle is active
    /// It takes an enemyType variable to indicate its stats
    /// It also grabs the enemy object so the script will be able to control its components
    /// </summary>
    public struct EnemyBattleData
    {
        public bool battleOn;
        public string enemyType;
        public int enemyLevel;
        public GameObject enemy;
        public EnemyBattleData(bool battleOn, string enemyType, int enemyLevel, GameObject enemy)
        {
            this.battleOn = battleOn;
            this.enemyType = enemyType;
            this.enemyLevel = enemyLevel;
            this.enemy = enemy;
        }
    }
    #endregion
    #region STATS4 STRUCT
    public struct Stats4
    {
        /// <summary>
        /// Stats4 is a data struct that contains the 4 stats of the game
        /// Health, Attack, Defense, and Speed
        /// This is used to store player, enemy, ect. stats
        /// </summary>
        public int health;
        public int attack;
        public int defense;
        public int speed;
        public Stats4(int health, int attack, int defense, int speed)
        {
            this.health = health;
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
        }
    }
    #endregion
    //ENUMS
    #region ENEMY ENUM
    public enum EnemyEnum
    {
        Minion,
        Brute,
        MiniBoss,
        Boss,
        SuperBoss,
        HyperBoss,
        FinalBoss
    }
    #endregion
    #region TRIGGER ENUM
    public enum TriggerEnum
    {
        SceneToScene
    }
    #endregion
    #region UI ENUM
    public enum UIEnum
    {
        Main,
        Pause,
    }
    #endregion
    #region SCENE LIST ENUM
    public enum SceneListEnum
    {
        DinoAgeIntro,
        DinoAgeHub
    }
    #endregion
}