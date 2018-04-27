// ReSharper disable once CheckNamespace

using Game.Utils;

// ReSharper disable InconsistentNaming

namespace Game.Facade
{
    public class AppConfig
    {
        #region 枚举

        /// <summary>
        /// 充值货币类型
        /// </summary>
        [EnumDescription("代币转换类型")]
        public enum PayScoreType
        {
            [EnumDescription("游戏币")] Score = 0,
            [EnumDescription("钻石")] Diamond = 1,
            [EnumDescription("游戏豆")] Currency = 2
        }

        /// <summary>
        /// 充值服务类型
        /// </summary>
        [EnumDescription("充值服务类型")]
        public enum ShareType
        {
            [EnumDescription("手机微信充值")] MobileWXPay = 101,
            [EnumDescription("H5WXPay")] H5WXPay = 102,
            [EnumDescription("手机支付宝充值")] MobileZFBPay = 201,
            [EnumDescription("手机零钱充值")] Mobile360LQPay = 301,
            [EnumDescription("竣付通微信充值")] JFTWXPay = 302,
            [EnumDescription("竣付通支付宝充值")] JFTZFBPay = 303,
            [EnumDescription("手机苹果充值")] MobileAppStorePay = 800
        }

        /// <summary>
        /// 充值服务类型
        /// </summary>
        [EnumDescription("代理返利类型")]
        public enum AwardType
        {
            [EnumDescription("钻石充值返利【返钻石】")]
            MobileWXPay = 1,
            [EnumDescription("游戏税收返利【返游戏币】")]
            H5WXPay = 2
        }

        #endregion

        #region 常量

        /// <summary>
        /// 验证码Session的KEY值
        /// </summary>
        public const string VerifyCodeKey = "VCKCache";

        /// <summary>
        /// 登录资源缓存KEY值
        /// </summary>
        public const string LoginResources = "LRESCache";

        /// <summary>
        /// 用户登录cookie缓存KEY值
        /// </summary>
        public const string UserCookieKey = "UCKCache";

        /// <summary>
        /// 用户登录cookie缓存时长
        /// </summary>
        public const int UserCookieTimeOut = 60;

        /// <summary>
        /// 用户登录session缓存KEY值
        /// </summary>
        public const string UserSessionKey = "USKCache";

        /// <summary>
        /// 用户登录session缓存时长
        /// </summary>
        public const int UserSessionTimeOut = 30;

        /// <summary>
        /// ip地址库缓存
        /// </summary>
        public const string IpSessionKey = "IPSKCache";

        #endregion 常量
    }
}
