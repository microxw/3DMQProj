/**
 * UIEventType
 * UI事件类型
 * brandy added
 */

public enum UIEventType
{
    #region event id
    // 抓牌 
    PickTsumoHai,
    PickRinshanHai,

    // 选择舍牌
    Select_SuteHai,
    // 舍牌
    SuteHai,
    // 听
    Reach,
    // 碰
    Pon,
    // 吃(左) 
    Chii_Left,
    // 吃(中央) 
    Chii_Center,
    // 吃(右) 
    Chii_Right,
    // 大明杠
    DaiMinKan,
    // 加杠 
    Kakan,
    // 暗杠
    Ankan,
    // 点炮检查
    Ron_Check,
    // 自摸胡
    Tsumo_Agari,
    // 炮胡
    Ron_Agari,
    // 满贯
    Nagashi,
    #endregion

    DisplayMenuList,
    HideMenuList,

    DisplayKyokuInfo,

    // callback of all EActionType, and other system animations
    On_UIAnim_End,

    // 开始游戏
    Start_Game,

    // 开局
    Start_Kyoku,

    Init_Game,

    // 选择吃牌顺序
    Select_ChiiCha,
    On_Select_ChiiCha_End,

    Init_PlayerInfoUI,

    Select_Wareme,
    On_Select_Wareme_End,

    // 配牌
    HaiPai,

    SetYama_BeforeHaipai,
    SetUI_AfterHaipai,

    Display_Agari_Panel,

    // 流局 
    RyuuKyoku,

    // 本局结束
    End_Kyoku,
    End_RyuuKyoku,

    // 游戏结束
    End_Game,

}
