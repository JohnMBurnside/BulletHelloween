#region NAMESPACES
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
#endregion
public class Player : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    public int level = 1;
    public int currentLevel;
    [Header("Movement Settings")]
    public float moveSpeed = 3;
    bool gravity;
    [Header("Lighting Settings")]
    Light2D playerLight;
    public float maxLightRadius;
    public float currentLightRadius;
    public bool startFunctionFinished;
    bool heal;
    bool damage;
    [Header("Test Settings")]
    public float rangeTest;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        currentLevel = GameObject.Find("LevelDetails").GetComponent<LevelDetails>().level;
        if (currentLevel > level)
            level = currentLevel;
        SaveGame();
        StartCoroutine(LightsF.OnStart());
        //Lighting
        playerLight = GetComponent<Light2D>();
        maxLightRadius = playerLight.pointLightOuterRadius;
        currentLightRadius = maxLightRadius;
        //Collision
        GetComponent<CircleCollider2D>().radius = playerLight.pointLightInnerRadius;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //Movement
        float x = Input.GetAxis("Horizontal");
        float y;
        if (gravity == false)
            y = Input.GetAxis("Vertical");
        else
            y = 0;
        Vector2 moveDir = new Vector2(x, y);
        if(GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
            GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;
        //Lighting
        playerLight.pointLightOuterRadius = currentLightRadius;
        //Hit points
        if(startFunctionFinished == true)
        {
            LayerMask lightLayer = LayerMask.GetMask("Lights");
            Collider2D light = Physics2D.OverlapCircle(transform.position, GetComponent<CircleCollider2D>().radius, lightLayer);
            if (light != null)
            {
                if (light.CompareTag("Light") && playerLight.intensity < 1 && heal == false)
                    StartCoroutine(Heal(playerLight));
                switch (light.GetComponent<SafeLight>().lightType)
                {
                    case LightsType.NextLevel:
                        StartCoroutine(NextLevel());
                        break;
                    case LightsType.Gravity:
                        gravity = true;
                        GetComponent<Rigidbody2D>().gravityScale = 10;
                        break;
                }

            }
            else if(light == null && damage == false)
            {
                gravity = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                StartCoroutine(Damage(playerLight));
            }
        }
        if (playerLight.intensity > 1)
            playerLight.intensity = 1;
    }
    #endregion
    //PLAYER FUNCTIONS
    #region SAVE GAME FUNCTION
    public void SaveGame()
        { SaveSystem.Save(this); }
    #endregion
    #region LOAD GAME FUNCTION
    public void LoadGame()
    {
        PlayerData data = SaveSystem.Load();
        level = data.level;
    }
    #endregion
    #region HEAL FUNCTION
    IEnumerator Heal(Light2D playerLight)
    {
        damage = false;
        heal = true;
        for (float i = playerLight.intensity; i < 1;)
        {
            if (heal == false)
                break;
            playerLight.intensity += .01f;
            i = playerLight.intensity;
            yield return new WaitForSeconds(.01f);
        }
    }
    #endregion
    #region DAMAGE FUNCTION
    IEnumerator Damage(Light2D playerLight)
    {
        heal = false;
        damage = true;
        for (float i = playerLight.intensity; i > .00000001;)
        {
            if (damage == false)
                break;
            playerLight.intensity -= .01f;
            i = playerLight.intensity;
            yield return new WaitForSeconds(.05f);
        }
        if (playerLight.intensity < 0.01)
            StartCoroutine(LightsF.OnEnd(OnEndFate.Death));
    }
    #endregion
    #region NEXT LEVEL FUNCTION
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(LightsF.OnEnd(OnEndFate.NextLevel));
    }
    #endregion
    #region DESTROY COMPONENT FUNCTION
    public void DestroyComponent(Component component) 
        { Destroy(component); }
    #endregion
}
