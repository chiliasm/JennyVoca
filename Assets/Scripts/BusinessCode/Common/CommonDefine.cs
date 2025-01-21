namespace Jenny
{
    public enum E_MainUI
    {
        None = -1,

        MainUI_Intro_Base,

        MainUI_Lobby_Base,
        MainUI_Lobby_Regist,
        MainUI_Lobby_Exam,

        Last,
    }

    public enum E_SubUI
    {
        None = -1,

        SubUI_RegistOrder,
        SubUI_SelectOrder,
        SubUI_AppSetting,

        Last,
    }

    public enum E_MsgUI
    {
        None = -1,

        MsgUI_Common,

        Last,
    }

    public enum E_Sound_Item
    {
        None = -1,

        //  Bgm.
        Bgm_Begin,
        Bgm_End,


        //  Sfx.
        Sfx_Begin,
        Sfx_Click_Bubble,
        Sfx_End,

        Max,
    }
}
