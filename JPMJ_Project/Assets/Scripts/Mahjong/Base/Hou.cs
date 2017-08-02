/**
 * Hou
 * 管理河（日本麻将，玩家打出去的牌放置的那块区域叫河）
 * brandy added
 */

using System.Collections.Generic;

public class Hou
{
    // 舍牌的排列长度的最大值
    public readonly static int SUTE_HAIS_LENGTH_MAX = 24;

    // 舍牌队列
    protected List<SuteHai> _suteHais = new List<SuteHai>(SUTE_HAIS_LENGTH_MAX);

    public Hou()
    {

    }
    public Hou(Hou src)
    {
        copy(this, src);
    }


    public void initialize()
    {
        _suteHais.Clear();
    }

    // 复制
    public static void copy(Hou dest, Hou src)
    {
        dest._suteHais.Clear();

        for (int i = 0; i < src._suteHais.Count; i++)
        {
            dest._suteHais.Add(new SuteHai(src._suteHais[i]));
        }
    }


    // 取得舍牌队列
    public SuteHai[] getSuteHais()
    {
        return _suteHais.ToArray();
    }

    // 在舍牌队列上增加牌
    public bool addHai(Hai hai)
    {
        if (_suteHais.Count >= SUTE_HAIS_LENGTH_MAX)
            return false;

        _suteHais.Add(new SuteHai(hai));

        return true;
    }

    // 在舍牌排列的最后的牌上设定鸣叫标志
    public bool setNaki(bool isNaki)
    {
        if (_suteHais.Count <= 0)
            return false;

        _suteHais[_suteHais.Count - 1].IsNaki = isNaki;

        return true;
    }

    // 在舍牌排列的最后的牌上设定一个标记
    public bool setReach(bool isReach)
    {
        if (_suteHais.Count <= 0)
            return false;

        _suteHais[_suteHais.Count - 1].IsReach = isReach;

        return true;
    }

    // 在舍牌排列的最后一个牌上设定好标记
    public bool setTedashi(bool isTedashi)
    {
        if (_suteHais.Count <= 0)
            return false;

        _suteHais[_suteHais.Count - 1].IsTedashi = isTedashi;

        return true;
    }

}
