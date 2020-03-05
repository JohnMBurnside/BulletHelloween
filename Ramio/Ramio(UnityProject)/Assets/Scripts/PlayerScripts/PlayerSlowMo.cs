#region NAMESPACES
using UnityEngine;
using UnityEngine.UI;
#endregion
public class PlayerSlowMo : MonoBehaviour
{
  #region VARIABLES
  [Header("Slow Mo Settings")]
  public float slowMoMaxTime = 8f;
  public float slowMoTime;
  public Slider slowMoSlider;
  public bool slowMoOn;
  #endregion
  //UNITY FUNCTIONS
  #region START FUNCTION
  void Start()
  {
      slowMoTime = slowMoMaxTime;
      slowMoSlider.maxValue = slowMoMaxTime;
      slowMoSlider.value = slowMoTime;
  }
  #endregion
  #region UPDATE FUNCTION
  void Update()
  {
    //Slow-Mo
    if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        SlowMoSwitch();
    if(slowMoOn == true)
    {
        slowMoTime -= Time.deltaTime;
        if(slowMoTime < 0.1)
            SlowMoSwitch();
    }
    else if(slowMoOn == false && slowMoTime < slowMoMaxTime)
        slowMoTime += Time.deltaTime;
    //Slow Mo Slider
    slowMoSlider.value = slowMoTime;
  }
  #endregion
  //PLAYER SLOW MO FUNCTIONS
  #region SLOW MO SWITCH FUNCTION
  void SlowMoSwitch()
  {
      if(slowMoOn == false)
      {
          Time.timeScale = 0.6f;
          slowMoOn = true;
      }
      else
      {
          Time.timeScale = 1;
          slowMoOn = false;
      }
  }
  #endregion
}
