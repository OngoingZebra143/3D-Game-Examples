using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup SceneCover;
    [SerializeField] private bool _fadeIn;
    [SerializeField] private bool _fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        if(_fadeIn)
        {
            if(SceneCover.alpha < 1)
            {
                SceneCover.alpha += Time.deltaTime;
                if(SceneCover.alpha >= 1)
                {
                    _fadeIn = false;
                }
            }
        }

        if(_fadeOut)
        {
            if(SceneCover.alpha >= 0)
            {
                SceneCover.alpha -= Time.deltaTime;
                if(SceneCover.alpha == 0)
                {
                    _fadeOut = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void FadeInUI()
    {
        //SceneCover.alpha = 0;
        _fadeIn = true;
    }

    public void FadeOutUI()
    {
        //SceneCover.alpha = 1;
        _fadeOut = true;
        GameObject.Find("Game Session").GetComponent<Timer>();
    }
    
 }
