/**
 * Tehai
 * 管理手牌（手牌:拿到手上的牌，不包括吃碰杠的牌）
 * brandy added
 */

using System.Collections.Generic;

public class Tehai
{
    public static int Compare(Hai x, Hai y)
    {
        if (x.ID == y.ID)
        {
            if (x.IsRed && !y.IsRed)
                return -1;
            else if (!x.IsRed && y.IsRed)
                return 1;
            else
                return 0;
        }
        else
        {
            return x.ID - y.ID;
        }
    }


    // 纯手牌的长度的最大值
    public readonly static int JYUN_TEHAI_LENGTH_MAX = 14;
    // 副露の最大値（最多4铺牌，包括吃碰杠，放在桌面上）
    public readonly static int FUURO_MAX = 4;

    // 面子の長さ(3)
    public readonly static int MENTSU_LENGTH_3 = 3;
    // 面子の長さ(4)
    public readonly static int MENTSU_LENGTH_4 = 4;


    // 純手牌
    private List<Hai> _jyunTehais = new List<Hai>(JYUN_TEHAI_LENGTH_MAX);

    // 副露の配列
    private List<Fuuro> _fuuros = new List<Fuuro>(FUURO_MAX);


    public Tehai()
    {
        initialize();
    }

    public Tehai(Tehai src)
    {
        initialize();
        copy(this, src, true);

        Sort();
    }

    public void initialize()
    {
        _jyunTehais.Clear();
        _fuuros.Clear();
    }


    // 复制手牌
    public static void copy(Tehai dest, Tehai src, bool jyunTehaiCopy)
    {
        if (jyunTehaiCopy == true)
        {
            dest._jyunTehais.Clear();

            for (int i = 0; i < src._jyunTehais.Count; i++)
                dest._jyunTehais.Add(new Hai(src._jyunTehais[i]));
        }

        dest._fuuros.Clear();

        for (int i = 0; i < src._fuuros.Count; i++)
            dest._fuuros.Add(new Fuuro(src._fuuros[i]));
    }


    // 副露の配列を取得する
    public Fuuro[] getFuuros()
    {
        return _fuuros.ToArray();
    }

    // 鸣听Flag
    public bool isNaki()
    {
        for (int i = 0; i < _fuuros.Count; i++)
        {
            if (_fuuros[i].Type != EFuuroType.AnKan)
                return true;
        }

        return false;
    }

    // <summary>
    // 副露の配列をコピーする
    // </summary>

    public static bool copyFuuros(Fuuro[] dest, Fuuro[] src, int length)
    {
        if (length > FUURO_MAX)
            return false;

        for (int i = 0; i < length; i++)
            Fuuro.copy(dest[i], src[i]);

        return true;
    }

    public void Sort()
    {
        _jyunTehais.Sort(Tehai.Compare);
        return;
    }

    // 取得纯手牌
    public Hai[] getJyunTehai()
    {
        return _jyunTehais.ToArray();
    }

    public int getJyunTehaiCount()
    {
        return _jyunTehais.Count;
    }

    public int getHaiIndex(int haiID)
    {
        return _jyunTehais.FindIndex(h => h.ID == haiID);
    }

    // 在纯手牌上追加牌
    public bool addJyunTehai(Hai hai)
    {
        if (_jyunTehais.Count >= JYUN_TEHAI_LENGTH_MAX)
            return false;

        _jyunTehais.Add(new Hai(hai));

        return true;
    }

    public bool insertJyunTehai(int index, Hai hai)
    {
        if (_jyunTehais.Count >= JYUN_TEHAI_LENGTH_MAX)
            return false;

        if (index < 0 || index > _jyunTehais.Count)
            return false;

        _jyunTehais.Insert(index, hai);

        return true;
    }

    // 从纯手牌中删除指定位置的牌
    public Hai removeJyunTehaiAt(int index)
    {
        if (index >= _jyunTehais.Count)
            return null;

        Hai hai = _jyunTehais[index];
        _jyunTehais.RemoveAt(index);

        return hai;
    }

    // 从纯手牌中删除指定的牌
    public bool removeJyunTehai(Hai hai)
    {
        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].ID == hai.ID)
            {
                _jyunTehais.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    // 复制纯手牌
    public static bool copyJyunTehai(Hai[] dest, Hai[] src, int length = -1)
    {
        if (length <= 0)
            length = src.Length;

        if (length > JYUN_TEHAI_LENGTH_MAX)
            return false;

        for (int i = 0; i < length; i++)
        {
            Hai.copy(dest[i], src[i]);
        }

        return true;
    }

    // 复制纯手牌指定位置的牌
    public bool copyJyunTehaiIndex(Hai hai, int index)
    {
        if (index >= _jyunTehais.Count)
            return false;

        Hai.copy(hai, _jyunTehais[index]);

        return true;
    }


    // 检查一下这个sute牌是否可在左边被吃
    public bool validChiiLeft(Hai suteHai, List<Hai> sarashiHais)
    {
        if (_fuuros.Count >= FUURO_MAX)
            return false;

        if (suteHai.IsTsuu)
            return false;

        if (suteHai.Num == Hai.NUM_8 || suteHai.Num == Hai.NUM_9)
            return false;

        int noKindLeft = suteHai.NumKind;
        int noKindCenter = noKindLeft + 1;
        int noKindRight = noKindLeft + 2;

        int count = _jyunTehais.Count;
        for (int i = 0; i < count; i++)
        {
            if (_jyunTehais[i].NumKind == noKindCenter)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (_jyunTehais[j].NumKind == noKindRight)
                    {
                        sarashiHais.Add(_jyunTehais[i]);
                        sarashiHais.Add(_jyunTehais[j]);

                        return true;
                    }
                }
            }
        }

        return false;
    }

    // 设定左边吃牌
    public bool setChiiLeft(Hai suteHai, int relation)
    {
        List<Hai> sarashiHais = new List<Hai>();

        if (!validChiiLeft(suteHai, sarashiHais))
            return false;

        Hai[] hais = new Hai[Tehai.MENTSU_LENGTH_3]; //创建新的一聚牌队列（三个）

        hais[0] = new Hai(suteHai);
        int newPickIndex = 0;

        int noKindLeft = suteHai.NumKind;
        int noKindCenter = noKindLeft + 1;
        int noKindRight = noKindLeft + 2;

        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].NumKind == noKindCenter)
            {
                hais[1] = new Hai(_jyunTehais[i]);

                removeJyunTehaiAt(i);

                for (int j = i; j < _jyunTehais.Count; j++)
                {
                    if (_jyunTehais[j].NumKind == noKindRight)
                    {
                        hais[2] = new Hai(_jyunTehais[j]);

                        removeJyunTehaiAt(j); //从纯手牌中删除

                        //再把新建的hais添加到副牌中展示
                        _fuuros.Add(new Fuuro(EFuuroType.MinShun, hais, relation, newPickIndex));

                        return true;
                    }
                }
            }
        }

        return false;
    }

    // 检查一下这个sute牌是否可在中间被吃
    public bool validChiiCenter(Hai suteHai, List<Hai> sarashiHais)
    {
        if (_fuuros.Count >= FUURO_MAX)
            return false;

        if (suteHai.IsTsuu)
            return false;

        if (suteHai.Num == Hai.NUM_1 || suteHai.Num == Hai.NUM_9)
            return false;

        int noKindCenter = suteHai.NumKind;
        int noKindLeft = noKindCenter - 1;
        int noKindRight = noKindCenter + 1;

        int count = _jyunTehais.Count;
        for (int i = 0; i < count; i++)
        {
            if (_jyunTehais[i].NumKind == noKindLeft)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (_jyunTehais[j].NumKind == noKindRight)
                    {
                        sarashiHais.Add(_jyunTehais[i]);
                        sarashiHais.Add(_jyunTehais[j]);

                        return true;
                    }
                }
            }
        }

        return false;
    }

    // 设定中间吃牌
    public bool setChiiCenter(Hai suteHai, int relation)
    {
        List<Hai> sarashiHais = new List<Hai>();

        if (!validChiiCenter(suteHai, sarashiHais))
            return false;

        Hai[] hais = new Hai[Tehai.MENTSU_LENGTH_3];

        hais[1] = new Hai(suteHai);
        int newPickIndex = 1;

        int noKindCenter = suteHai.NumKind;
        int noKindLeft = noKindCenter - 1;
        int noKindRight = noKindCenter + 1;

        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].NumKind == noKindLeft)
            {
                hais[0] = new Hai(_jyunTehais[i]);

                removeJyunTehaiAt(i);

                for (int j = i; j < _jyunTehais.Count; j++)
                {
                    if (_jyunTehais[j].NumKind == noKindRight)
                    {
                        hais[2] = new Hai(_jyunTehais[j]);

                        removeJyunTehaiAt(j);

                        _fuuros.Add(new Fuuro(EFuuroType.MinShun, hais, relation, newPickIndex));

                        return true;
                    }
                }
            }
        }

        return false;
    }

    // 检查一下这个sute牌是否可在右边被吃
    public bool validChiiRight(Hai suteHai, List<Hai> sarashiHais)
    {
        if (_fuuros.Count >= FUURO_MAX)
            return false;

        if (suteHai.IsTsuu)
            return false;

        if (suteHai.Num == Hai.NUM_1 || suteHai.Num == Hai.NUM_2)
            return false;

        int noKindRight = suteHai.NumKind;
        int noKindLeft = noKindRight - 2;
        int noKindCenter = noKindRight - 1;

        int count = _jyunTehais.Count;
        for (int i = 0; i < count; i++)
        {
            if (_jyunTehais[i].NumKind == noKindLeft)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (_jyunTehais[j].NumKind == noKindCenter)
                    {
                        sarashiHais.Add(_jyunTehais[i]);
                        sarashiHais.Add(_jyunTehais[j]);

                        return true;
                    }
                }
            }
        }

        return false;
    }

    // 设定右边吃牌
    public bool setChiiRight(Hai suteHai, int relation)
    {
        List<Hai> sarashiHais = new List<Hai>();

        if (!validChiiRight(suteHai, sarashiHais))
            return false;

        Hai[] hais = new Hai[Tehai.MENTSU_LENGTH_3];

        hais[2] = new Hai(suteHai);
        int newPickIndex = 2;

        int noKindRight = suteHai.NumKind;
        int noKindLeft = noKindRight - 2;
        int noKindCenter = noKindRight - 1;

        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].NumKind == noKindLeft)
            {
                hais[0] = new Hai(_jyunTehais[i]);

                removeJyunTehaiAt(i);

                for (int j = i; j < _jyunTehais.Count; j++)
                {
                    if (_jyunTehais[j].NumKind == noKindCenter)
                    {
                        hais[1] = new Hai(_jyunTehais[j]);

                        removeJyunTehaiAt(j);

                        _fuuros.Add(new Fuuro(EFuuroType.MinShun, hais, relation, newPickIndex));

                        return true;
                    }
                }
            }
        }

        return false;
    }

    // 检查一下是否可以碰
    public bool validPon(Hai suteHai)
    {
        if (_fuuros.Count >= FUURO_MAX)
            return false;

        int count = 1; // include the suteHai.
        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].ID == suteHai.ID)
            {
                count++;

                if (count >= Tehai.MENTSU_LENGTH_3)
                    return true;
            }
        }

        return false;
    }

    // 设定碰
    public bool setPon(Hai suteHai, int relation)
    {
        if (!validPon(suteHai))
            return false;

        Hai[] hais = new Hai[Tehai.MENTSU_LENGTH_3];

        hais[0] = new Hai(suteHai);
        int newPickIndex = 0;

        int count = 1;
        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].ID == suteHai.ID)
            {
                hais[count] = new Hai(_jyunTehais[i]);
                count++;

                removeJyunTehaiAt(i);
                i--;

                if (count >= Tehai.MENTSU_LENGTH_3)
                    break;
            }
        }

        _fuuros.Add(new Fuuro(EFuuroType.MinKou, hais, relation, newPickIndex));

        return true;
    }


    // 检查是否有自摸杠
    public bool validAnyTsumoKan(Hai addHai, List<Hai> kanHais)
    {
        if (_fuuros.Count > FUURO_MAX) // the 4th can kakan. 四铺还是可以杠的
            return false;

        addJyunTehai(addHai); //添加到纯手牌
        Sort();

        Hai checkHai = null;

        // 加杠的检查（铺牌里面有碰的就可能会产生杠的情况）
        for (int i = 0; i < _fuuros.Count; i++)
        {
            if (_fuuros[i].Type == EFuuroType.MinKou)
            {
                checkHai = _fuuros[i].Hais[0];
                for (int j = 0; j < _jyunTehais.Count; j++)
                {
                    if (_jyunTehais[j].ID == checkHai.ID)
                        kanHais.Add(new Hai(checkHai));
                }
            }
        }

        if (_fuuros.Count >= FUURO_MAX)
        {
            removeJyunTehai(addHai);
            Sort();
            return kanHais.Count > 0;
        }

        // 暗杠检查
        checkHai = _jyunTehais[0];
        int count = 1;

        for (int i = 1; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].ID == checkHai.ID)
            {
                count++;
                if (count >= Tehai.MENTSU_LENGTH_4)
                    kanHais.Add(new Hai(checkHai));
            }
            else
            {
                checkHai = _jyunTehais[i];
                count = 1;
            }
        }

        removeJyunTehai(addHai);
        Sort();

        return kanHais.Count > 0;
    }

    //检查是否有大明杠
    public bool validDaiMinKan(Hai suteHai)
    {
        if (_fuuros.Count >= FUURO_MAX)
            return false;

        int count = 1;
        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].ID == suteHai.ID)
            {
                count++;
                if (count >= Tehai.MENTSU_LENGTH_4)
                    return true;
            }
        }

        return false;
    }

    // 设定大明杠
    public bool setDaiMinKan(Hai suteHai, int relation)
    {
        Hai[] hais = new Hai[Tehai.MENTSU_LENGTH_4];

        hais[0] = new Hai(suteHai);
        int newPickIndex = 0;

        int count = 1;

        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].ID == suteHai.ID)
            {
                hais[count] = new Hai(_jyunTehais[i]);
                count++;

                removeJyunTehaiAt(i);
                i--;

                if (count >= Tehai.MENTSU_LENGTH_4)
                    break;
            }
        }

        _fuuros.Add(new Fuuro(EFuuroType.DaiMinKan, hais, relation, newPickIndex));

        return true;
    }


    // 检查是否加杠
    public bool validKaKan(Hai tsumoHai)
    {
        if (_fuuros.Count > FUURO_MAX)
            return false;

        for (int i = 0; i < _fuuros.Count; i++)
        {
            if (_fuuros[i].Type == EFuuroType.MinKou)
            {
                if (_fuuros[i].Hais[0].ID == tsumoHai.ID)
                    return true;
            }
        }

        return false;
    }

    // 设定加杠
    public bool setKaKan(Hai tsumoHai)
    {
        if (!validKaKan(tsumoHai))
            return false;

        int relation = (int)ERelation.JiBun; //0;
        int newPickIndex = 3;

        for (int i = 0; i < _fuuros.Count; i++)
        {
            if (_fuuros[i].Type == EFuuroType.MinKou)
            {
                if (_fuuros[i].Hais[0].ID == tsumoHai.ID)
                {
                    List<Hai> fuuroHais = new List<Hai>(_fuuros[i].Hais);
                    fuuroHais.Add(new Hai(tsumoHai));

                    _fuuros[i].Update(EFuuroType.KaKan, fuuroHais.ToArray(), relation, newPickIndex);
                }
            }
        }

        return true;
    }


    // 检查是否暗杠
    public bool validAnKan(Hai tsumoHai)
    {
        if (_fuuros.Count >= FUURO_MAX)
            return false;

        int count = 1;
        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].ID == tsumoHai.ID)
            {
                count++;
                if (count >= Tehai.MENTSU_LENGTH_4)
                    return true;
            }
        }

        return false;
    }

    // 设定暗杠
    public bool setAnKan(Hai tsumoHai)
    {
        if (!validAnKan(tsumoHai))
            return false;

        int relation = (int)ERelation.JiBun; //0;

        Hai[] hais = new Hai[Tehai.MENTSU_LENGTH_4];

        hais[0] = new Hai(tsumoHai);
        int newPickIndex = 3;

        int count = 1;
        for (int i = 0; i < _jyunTehais.Count; i++)
        {
            if (_jyunTehais[i].ID == tsumoHai.ID)
            {
                hais[count] = new Hai(_jyunTehais[i]);
                count++;

                removeJyunTehaiAt(i);
                i--;

                if (count >= Tehai.MENTSU_LENGTH_4)
                    break;
            }
        }

        _fuuros.Add(new Fuuro(EFuuroType.AnKan, hais, relation, newPickIndex));

        return true;
    }

}
