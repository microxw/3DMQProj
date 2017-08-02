/**
 * Fuuro
 * 管理副牌（副牌:就是吃／碰／杠之后放在桌角的牌）
 * brandy added
 */

using System.Collections.Generic;

public class Fuuro
{
    // 类别
    private EFuuroType _type = EFuuroType.MinShun;

    // 组成牌
    private Hai[] _hais = null;

    // 同人的关系(和其他人的关系, 新牌从谁那里得来)
    private int _fromRelation = -1;

    // index of the new hai in m_hais that is newly picked by player or from others(AI)
    private int _newPickIndex = -1; // 新牌的位置


    public Fuuro(Fuuro other)
    {
        copy(this, other);
    }

    public Fuuro(EFuuroType newType, Hai[] newHais, int newRelation, int newPickIndex)
    {
        this._type = newType;
        this._hais = newHais;
        this._fromRelation = newRelation;
        this._newPickIndex = newPickIndex;
    }


    public EFuuroType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public Hai[] Hais
    {
        get { return _hais; }
        set { _hais = value; }
    }

    public int FromRelation
    {
        get { return _fromRelation; }
        set { _fromRelation = value; }
    }

    public int NewPickIndex
    {
        get { return _newPickIndex; }
        set { _newPickIndex = value; }
    }


    public void Update(EFuuroType newType, Hai[] newHais, int newRelation, int newPick)
    {
        Type = newType;
        Hais = newHais;
        FromRelation = newRelation;
        NewPickIndex = newPick;
    }

    // 复制副牌 Copy the specified src furro to dest.
    public static void copy(Fuuro dest, Fuuro src)
    {
        dest._type = src._type;
        dest._fromRelation = src._fromRelation;
        dest._newPickIndex = src._newPickIndex;

        if (src._hais != null)
        {
            List<Hai> hai_copy = new List<Hai>();

            for (int i = 0; i < src._hais.Length; i++)
                hai_copy.Add(new Hai(src._hais[i]));

            dest._hais = hai_copy.ToArray();
        }

    }

}
