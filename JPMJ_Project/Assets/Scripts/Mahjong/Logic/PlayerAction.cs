/**
 * PlayerAction
 * player's action and state
 * brandy added
 */

using System.Collections.Generic;

public enum EActionState
{
    None = 0,

    Select_Agari = 1, // 炮胡或者自摸. and enable sute hai.
    Select_Sutehai = 2, // 舍牌

    Select_Reach = 3,
    Select_Chii = 4, // 吃
    Select_Kan = 5, // 杠
}

public enum EActionType
{
    Pon = 0,  // 碰
    Chii = 1,  // 吃
    Kan = 2,  // 杠
    Reach = 3,
    Tsumo = 4,  // 自摸
    Ron = 5,  // 炮胡
    Nagashi = 6,

    RyuuKyoku = 7, //流局
}


public class PlayerAction
{
    public const int Chii_Select_Left = 0;
    public const int Chii_Select_Center = 1;
    public const int Chii_Select_Right = 2;


    public PlayerAction()
    {
        Reset();
    }

    public void Reset()
    {
        _response = EResponse.Nagashi;
        _state = EActionState.None;

        _sutehaiIndex = Sutehai_Index_Max;

        _validRon = false;
        _validTsumo = false;
        _validPon = false;
        _validReach = false;

        _validChiiLeft = false;
        _validChiiCenter = false;
        _validChiiRight = false;

        IsValidChii = false;
        _allSarashiHais.Clear();

        _sarashiHaiLeft.Clear();
        _sarashiHaiCenter.Clear();
        _sarashiHaiRight.Clear();

        _validTsumoKan = false;
        _validDaiMinKan = false;
        _tsumoKanHais.Clear();

        _reachHaiIndexList.Clear();
        _menuList.Clear();

        _reachSelectIndex = 0;
        _ponSelectIndex = 0;
        _kanSelectIndex = 0;
        _chiiSelectType = 0;
    }


    private EResponse _response = EResponse.Nagashi;
    public EResponse Response
    {
        get { return _response; }
        set { _response = value; }
    }

    private EActionState _state = EActionState.None;
    public EActionState State
    {
        get { return _state; }
        set { _state = value; }
    }


    public const int Sutehai_Index_Max = 13;

    // 舍牌的索引
    private int _sutehaiIndex = Sutehai_Index_Max;
    public int SutehaiIndex
    {
        get { return _sutehaiIndex; }
        set { _sutehaiIndex = value; }
    }


    // 炮胡的可能
    private bool _validRon;
    public bool IsValidRon
    {
        get { return _validRon; }
        set { _validRon = value; }
    }

    // 自摸的可能
    private bool _validTsumo;
    public bool IsValidTsumo
    {
        get { return _validTsumo; }
        set { _validTsumo = value; }
    }

    // 碰的可能
    private bool _validPon;
    public bool IsValidPon
    {
        get { return _validPon; }
        set { _validPon = value; }
    }

    // 听牌的可能
    private bool _validReach;
    public bool IsValidReach
    {
        get { return _validReach; }
        set { _validReach = value; }
    }

    // all reach hai index
    private List<int> _reachHaiIndexList = new List<int>();
    public List<int> ReachHaiIndexList
    {
        get { return _reachHaiIndexList; }
        set { _reachHaiIndexList = value; }
    }


    #region Chii
    public bool IsValidChii
    {
        get; protected set;
    }

    private List<Hai> _allSarashiHais = new List<Hai>();
    public List<Hai> AllSarashiHais
    {
        get { return _allSarashiHais; }
        protected set { _allSarashiHais = value; }
    }

    protected void SetAnySarashiHai(List<Hai> sarashiHai)
    {
        if (sarashiHai == null) return;

        IsValidChii = true;
        int count = sarashiHai.Count;
        Hai sHai = null;
        for (int i = 0; i < count; i++)
        {
            sHai = sarashiHai[i];
            if (!AllSarashiHais.Exists(h => h.ID == sHai.ID))
            {
                AllSarashiHais.Add(new Hai(sHai));
            }
        }
    }

    // chii left
    private bool _validChiiLeft = false;
    public bool IsValidChiiLeft
    {
        get { return _validChiiLeft; }
        protected set { _validChiiLeft = value; }
    }

    private List<Hai> _sarashiHaiLeft = new List<Hai>();
    public List<Hai> SarashiHaiLeft
    {
        get { return _sarashiHaiLeft; }
        protected set { _sarashiHaiLeft = value; }
    }

    public void setValidChiiLeft(bool validChii, List<Hai> sarashiHai)
    {
        this._validChiiLeft = validChii;
        this._sarashiHaiLeft = sarashiHai;

        SetAnySarashiHai(sarashiHai);
    }

    // chii center
    private bool _validChiiCenter = false;
    public bool IsValidChiiCenter
    {
        get { return _validChiiCenter; }
        protected set { _validChiiCenter = value; }
    }

    private List<Hai> _sarashiHaiCenter = new List<Hai>();
    public List<Hai> SarashiHaiCenter
    {
        get { return _sarashiHaiCenter; }
        protected set { _sarashiHaiCenter = value; }
    }

    public void setValidChiiCenter(bool validChii, List<Hai> sarashiHai)
    {
        this._validChiiCenter = validChii;
        this._sarashiHaiCenter = sarashiHai;

        SetAnySarashiHai(sarashiHai);
    }

    // chii right
    private bool _validChiiRight = false;
    public bool IsValidChiiRight
    {
        get { return _validChiiRight; }
        protected set { _validChiiRight = value; }
    }

    private List<Hai> _sarashiHaiRight = new List<Hai>();
    public List<Hai> SarashiHaiRight
    {
        get { return _sarashiHaiRight; }
        protected set { _sarashiHaiRight = value; }
    }

    public void setValidChiiRight(bool validChii, List<Hai> sarashiHai)
    {
        this._validChiiRight = validChii;
        this._sarashiHaiRight = sarashiHai;

        SetAnySarashiHai(sarashiHai);
    }
    #endregion


    // 暗杠和加杠的可能
    private bool _validTsumoKan = false;
    public bool IsValidTsumoKan
    {
        get { return _validTsumoKan; }
        protected set { _validTsumoKan = value; }
    }

    private List<Hai> _tsumoKanHais = new List<Hai>();
    public List<Hai> TsumoKanHaiList
    {
        get { return _tsumoKanHais; }
        protected set { _tsumoKanHais = value; }
    }

    public void setValidTsumoKan(bool validKan, List<Hai> kanHais)
    {
        _validTsumoKan = validKan;
        _tsumoKanHais = kanHais;
    }

    // 大明杠的可能
    private bool _validDaiMinKan = false;
    public bool IsValidDaiMinKan
    {
        get { return _validDaiMinKan; }
        set { _validDaiMinKan = value; }
    }


    #region Menu
    private List<EActionType> _menuList = new List<EActionType>();
    public List<EActionType> MenuList
    {
        get { return _menuList; }
    }


    private int _reachSelectIndex = 0;
    // Gets or sets the index of the ReachHaiIndexList
    public int ReachSelectIndex
    {
        get { return _reachSelectIndex; }
        set { _reachSelectIndex = value; }
    }

    private int _ponSelectIndex = 0;
    public int PonSelectIndex
    {
        get { return _ponSelectIndex; }
        set { _ponSelectIndex = value; }
    }

    private int _kanSelectIndex = 0;
    // Gets or sets the selected index of the TsumoKanHaiList
    public int KanSelectIndex
    {
        get { return _kanSelectIndex; }
        set { _kanSelectIndex = value; }
    }

    private int _chiiSelectType = 0;
    public int ChiiSelectType
    {
        get { return _chiiSelectType; }
        set { _chiiSelectType = value; }
    }
    #endregion
}