﻿/**
 * GameManager
 * singleton game manager, a component
 * brandy added
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameManager : StateMachine, IObserver
{
    private static GameManager _instance;
    public static GameManager Get()
    {
        return _instance;
    }

    private MahjongMain mahjong; //it's the game logic manager
    public MahjongMain LogicMain
    {
        get { return mahjong; }
    }

    private EVoiceType _systemVoiceType = EVoiceType.W_B;
    public EVoiceType SystemVoiceType
    {
        get{ return _systemVoiceType; }
        set{ _systemVoiceType = value; }
    }


    void OnEnable() 
    {
        EventManager.Get().addObserver(this);
    }
    void OnDisable() 
    {
        EventManager.Get().removeObserver(this);
    }


    //create instance here
    void Awake() {
        _instance = this; //GetComponent<GameManager>()
		mahjong = new MahjongMain();
    }

    void Start() {
        ChangeState<GameStartState>();
    }

    void OnDestroy()
    {
        ResManager.ClearMahjongPaiPool();
    }

    void OnApplicationQuit()
    {
        ResManager.ClearMahjongPaiPool();
    }


    public void Restart()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    public void Speak( ECvType content )
    {
        AudioManager.Get().PlaySFX( AudioConfig.GetCVPath(SystemVoiceType, content) );
    }

    public void OnHandleEvent(UIEventType evtID, object[] args) 
    {
        if( CurrentState is GameStateBase )
            (CurrentState as GameStateBase).OnHandleEvent(evtID, args);
    }

}
