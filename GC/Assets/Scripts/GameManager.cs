
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //private event EventHandler mFinalEvent; 

    private static GameManager mInstance;

    public static GameManager GetInstance()
    {
        return mInstance;
    }

    public HeroController hero;

    private void Awake()
    {
        mInstance = this;
    }
    /*public void AddFinalDelegate(EventHandler eventHandler){
        mFinalEvent += eventHandler;
    }*/
}
