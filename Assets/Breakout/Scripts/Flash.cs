using UnityEngine;
using UnityEngine.UI;  // add to the top
using System.Collections;

public class Flash : MonoBehaviour
{
    public CanvasGroup myCG;
    public float Rate = 1f;
    private bool flash = false;

    void Start()
    {
        myCG.alpha = 0;
    }

    void Update()
    {
        if (flash)
        {
            myCG.alpha = myCG.alpha - (Time.deltaTime * Rate);
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }

    public void StartFlash()
    {
        flash = true;
        myCG.alpha = 1;
    }
}
