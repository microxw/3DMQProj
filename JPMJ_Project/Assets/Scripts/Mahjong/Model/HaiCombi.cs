/**
 * HaiCombi
 * 胡牌的组合: 包括刻子（杠、碰）、顺子，头(atama)是剩余那个牌
 * 这些后续都不再需要了，后台提供功能支持
 * brandy added
 */

using System.Collections;
using System.Collections.Generic;


public class HaiCombi
{
    // 头的NK
    public int atamaNumKind = 0;

    // 顺子的NK队列 (store the left hai's NK of a shun mentsu)
    public int[] shunNumKinds = new int[4];

    // 顺子的NK队列有效个数
    public int shunCount = 0;

    // 刻子的NK队列
    public int[] kouNumKinds = new int[4];

    // 刻子的NK队列有效个数
    public int kouCount = 0;

    //public List<int> shunNumKinds = new List<int>();
    //public List<int> kouNumKinds = new List<int>();


    public void Clear()
    {
        this.atamaNumKind = 0;

        int count = shunNumKinds.Length;
        for (int i = 0; i < count; i++)
        {
            shunNumKinds[i] = 0;
        }
        this.shunCount = 0;

        count = kouNumKinds.Length;
        for (int i = 0; i < count; i++)
        {
            kouNumKinds[i] = 0;
        }
        this.kouCount = 0;

        //shunNumKinds.Clear();
        //kouNumKinds.Clear();
    }

    public static void copy(HaiCombi dest, HaiCombi src)
    {
        dest.Clear();

        dest.atamaNumKind = src.atamaNumKind;

        dest.shunCount = src.shunCount;
        for (int i = 0; i < dest.shunCount; i++)
        {
            dest.shunNumKinds[i] = src.shunNumKinds[i];
        }

        dest.kouCount = src.kouCount;
        for (int i = 0; i < dest.kouCount; i++)
        {
            dest.kouNumKinds[i] = src.kouNumKinds[i];
        }

        /*
        for( int i = 0; i < src.shunNumKinds.Count; i++ )
            dest.shunNumKinds.Add( src.shunNumKinds[i] );
        for( int i = 0; i < src.kouNumKinds.Count; i++ )
            dest.kouNumKinds.Add( src.kouNumKinds[i] );
        */
    }
}
