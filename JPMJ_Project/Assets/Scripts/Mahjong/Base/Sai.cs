/**
 * Sai
 * 管理骰子
 * brandy added
 */

public class Sai
{
    // 番号
    private int _num = 1;

    public int Num
    {
        get { return _num; }
    }

    // 摇动骰子获取号码（结果1-6）
    public int SaiFuri()
    {
        _num = Utils.GetRandomNum(1, 7);

        return _num;
    }
}