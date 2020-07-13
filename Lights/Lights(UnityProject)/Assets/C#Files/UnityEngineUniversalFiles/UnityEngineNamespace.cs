#region NAMESPACES
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
#endregion
namespace UnityEngine
{
    #region ENUMS
    public enum LightsType { Safe, NextLevel, Gravity}
    public enum UITypes { MainMenu, PauseMenu }
    public enum OnEndFate { Death, NextLevel, Testing }
    public enum Colors { Red, Green, Blue }
    public enum VersionPhases { Development, Alpha, Beta, LiveRelease }
    #endregion
    #region UNIVERSAL F CLASS
    public static class UniversalF
    {
        #region VARIABLES
        public static bool loadLevel = false;
        #endregion
        #region PRINT FUNCTION
        public static void Print(string message) { Debug.Log(message); }
        #endregion
        #region LOAD LEVEL FUNCTION
        public static IEnumerator LoadLevel(int level, Animator fadeAnimator) 
        {
            if(loadLevel == false)
            {
                loadLevel = true;
                fadeAnimator.SetTrigger("FadeOut");
                yield return new WaitForSeconds(2f);
                loadLevel = false;
                SceneManager.LoadScene("Level " + level); 
            }
        }
        #endregion
        #region LERP FUNCTION 
        public static IEnumerator Lerp(float startValue, float finishValue, float speed, float floatToLerp)
        {
            floatToLerp = startValue;
            bool positive;
            if ((startValue - finishValue) < 0)
                positive = true;
            else
                positive = false;
            if (positive == true)
            {
                for (float i = startValue; i <= finishValue; i += (.01f * speed))
                {
                    floatToLerp += .01f * speed;
                    yield return new WaitForSeconds(.05f);
                }
            }
            else if (positive == false)
            {
                for (float i = startValue; i >= finishValue; i -= .01f * speed)
                {
                    floatToLerp -= .01f * speed;
                    yield return new WaitForSeconds(.05f);
                }
            }
            floatToLerp = finishValue;
        }
        #endregion
    }
    #endregion
    #region LIGHTS F CLASS
    public static class LightsF
    {
        #region ON START FUNCTION
        public static IEnumerator OnStart()
        {
            //Local Variables
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float cameraRange = player.GetComponentInChildren<Camera>().orthographicSize * 2;
            LayerMask lightLayer = LayerMask.GetMask("Lights");
            //Setting up
            player.GetComponent<Light2D>().intensity = 0;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.AddComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<BoxCollider2D>().size = new Vector2(cameraRange, cameraRange);
            //Get all the lights within the Box Collider 2D range and then turn them on seemlessly
            Collider2D[] lightList = Physics2D.OverlapBoxAll(player.transform.position, new Vector2(cameraRange, cameraRange), 0, lightLayer);
            foreach (Collider2D light in lightList)
            {
                light.GetComponent<Animator>().enabled = false;
                light.GetComponent<Light2D>().intensity = 0;
            }
            foreach (Collider2D light in lightList)
            {
                light.GetComponent<Animator>().enabled = true;
                yield return new WaitForSeconds(.25f);
            }
            //Destroy Box Collider 2Ds
            player.GetComponent<Player>().DestroyComponent(player.GetComponent<BoxCollider2D>());
            //Set the Player Rigidbody 2D to Dynamic
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //Turn on Player seemlessly
            for (float i = 0; i <= 1; i += .01f)
            {
                player.GetComponent<Light2D>().intensity += .01f;
                yield return new WaitForSeconds(.01f);
            }
            player.GetComponent<Light2D>().intensity = 1;
            player.GetComponent<Player>().startFunctionFinished = true;
        }
        #endregion
        #region ON END FUNCTION
        public static IEnumerator OnEnd(OnEndFate end)
        {
            //Local Variables
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float cameraRange = player.GetComponentInChildren<Camera>().orthographicSize * 2;
            LayerMask lightLayer = LayerMask.GetMask("Lights");
            //Setting up player components
            player.GetComponent<Light2D>().intensity = 0;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.AddComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<BoxCollider2D>().size = new Vector2(cameraRange + 10, cameraRange + 10);
            //Get all the lights within the Box Collider 2D range and then turn them on seemlessly
            Collider2D[] lightList = Physics2D.OverlapBoxAll(player.transform.position, new Vector2(cameraRange + 10, cameraRange + 10), 0, lightLayer);
            foreach (Collider2D light in lightList)
            {
                try
                {
                    light.GetComponent<Light2D>().intensity = 1;
                    light.GetComponent<Animator>().SetTrigger("FadeOut");
                } catch { }
                yield return new WaitForSeconds(.25f);
            }
            //End result
            yield return new WaitForSeconds(1f);
            if (end == OnEndFate.Death) { SceneManager.LoadScene(player.GetComponent<Transform>().gameObject.scene.name); }
            else if(end == OnEndFate.NextLevel) { SceneManager.LoadScene((player.GetComponent<Player>().currentLevel + 1)); }
            else if(end == OnEndFate.Testing) { Debug.Log("Testing Finished"); }
        }
        #endregion
        #region LIGHT LERP (REGULAR) FUNCTION 
        public static IEnumerator LightIntensityLerp(float startValue, float finishValue, float speed, GameObject lightIntensity)
        {
            lightIntensity.GetComponent<Light2D>().intensity = startValue;
            bool positive;
            if ((startValue - finishValue) < 0)
                positive = true;
            else
                positive = false;
            if (positive == true)
            {
                for (float i = startValue; i <= finishValue; i += (.01f * speed))
                {
                    lightIntensity.GetComponent<Light2D>().intensity += .01f * speed;
                    try 
                    {
                        if (lightIntensity.GetComponentInParent<Transform>().GetComponentInParent<MenuLighting>().exitMenu == true)
                            yield break;
                    } catch { }
                    yield return new WaitForSeconds(.05f);
                }
            }
            else if (positive == false)
            {
                for (float i = startValue; i >= finishValue; i -= .01f * speed)
                {
                    lightIntensity.GetComponent<Light2D>().intensity -= .01f * speed;
                    try
                    {
                        if (lightIntensity.GetComponentInParent<Transform>().GetComponentInParent<MenuLighting>().exitMenu == true)
                            yield break;
                    } catch { }
                    yield return new WaitForSeconds(.05f);
                }
            }
            lightIntensity.GetComponent<Light2D>().intensity = finishValue;
        }
        #endregion
        #region LIGHT LERP (MAIN MENU) FUNCTION
        public static IEnumerator LightIntensityLerp(float startValue, float finishValue, float speed, GameObject light, bool mainMenu)
        {
            if (mainMenu == true)
            {
                for (float i = startValue; i >= finishValue; i -= .01f * speed)
                {
                    light.GetComponent<Light2D>().intensity -= .01f * speed;
                    yield return new WaitForSeconds(.05f);
                }
                light.GetComponent<Light2D>().intensity = finishValue;
            }
            else
                Debug.LogError("The function LightIntensityLerp(MainMenu) was used incorrectly pass through a true boolean for the last parameter for LightIntensityLerp to excute without fail");
        }
        #endregion
    }
    #endregion
    #region COLORS F CLASS
    public static class ColorsF
    {
        #region CALL COLOR FUNCTION
        public static Color CallColor(Colors color)
        {
            switch (color)
            {
                case Colors.Red:
                    return new Color(255, 0, 0);
                case Colors.Green:
                    return new Color(0, 255, 0);
                case Colors.Blue:
                    return new Color(0, 0, 255);
                default:
                    break;
            }
            return new Color(255, 255, 255);
        }
        #endregion
    }
    #endregion
}