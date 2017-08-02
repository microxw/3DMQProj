/**
 * SuteHai
 * 管理舍牌（舍牌：玩家打出去的牌）
 * brandy added
 */

public class SuteHai : Hai
{
    // (吃碰以及明杠)
    private bool _isNaki = false;

	// (听牌flag)
	private bool _isReach = false;

    // (正常打出去)
    private bool _isTedashi = false;


    public bool IsNaki
    {
        get{ return _isNaki; }
        set{ _isNaki = value; }
    }

    public bool IsReach
    {
        get{ return _isReach; }
        set{ _isReach = value; }
    }

    public bool IsTedashi
    {
        get{ return _isTedashi; }
        set{ _isTedashi = value; }
    }


    public SuteHai() : base() {
    }

    public SuteHai(int id) : base(id) {
    }

    public SuteHai(Hai hai) : base(hai) {
    }

    public SuteHai(SuteHai src) : base()
    {
        copy(this, src);
    }


    public static void copy(SuteHai dest, SuteHai src)
    {
        Hai.copy(dest, src);
        dest._isNaki = src._isNaki;
        dest._isReach = src._isReach;
        dest._isTedashi = src._isTedashi;
    }

    public static void copy(SuteHai dest, Hai src)
    {
        Hai.copy(dest, src);
        dest._isNaki = false;
        dest._isReach = false;
        dest._isTedashi = false;
    }

}
