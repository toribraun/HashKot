using UnityEngine;
using UnityEngine.UI;

public class TextControllerMenu : MonoBehaviour
{
    [SerializeField] 
    public Text PointsSum;
    [SerializeField] 
    public Text Mark;
    
    [SerializeField] 
    public AudioSource win;
    [SerializeField] 
    public AudioSource lose;
    void Start()
    {
        PointsSum.text = $"{GameStates.CurrentPointSum}/100";
        if (GameStates.CurrentPointSum < 40)
            Mark.text = "2";
        else if (GameStates.CurrentPointSum == 100)
            Mark.text = "5";
        else
            Mark.text = (GameStates.CurrentPointSum / 20 + 1).ToString();
    }

}
