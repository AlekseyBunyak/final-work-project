using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool _paused = false;

    /// <summary>
    /// pauses the game or take it off
    /// </summary>
    public void GamePause() 
    {
        if(!_paused) 
        {
            Time.timeScale = 0f;
        }
        else if(_paused)
        {
            Time.timeScale = 1f;
        }
        _paused = !_paused;
    }
}
