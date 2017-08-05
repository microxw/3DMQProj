/**
 * GameSettings
 * 游戏设置值
 * brandy added
 */

public static class GameSettings
{
    public static int PlayerCount = 4;

    // 食断
    public static bool UseKuitan = true;

    // 红Dora
    public static bool UseRedDora = true;

    // furiten：能胡自己打过的牌，也不能胡听牌后放弃胡牌的牌
    public static bool AllowFuriten = true;


    public const bool AllowRon3 = false;
    public const bool AllowReach4 = false;
    public const bool AllowSuteFonHai4 = false;

    // 局的最大値
    public const int Kyoku_Max = (int)EKyoku.Nan_4;
    public const int KanCountMax = 4;

    // 初始分数数值
    public const int Init_Tenbou = 25000;
    public const int Back_Tenbou = 30000; // used for calculating final pt

    public const int Reach_Cost = 1000;
    public const int HonBa_Cost = 100;

}
