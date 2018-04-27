using Game.Entity.Accounts;
using Game.Entity.Agent;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Game.Entity.NativeWeb;
using Game.Facade.DataStruct;
using Game.Facade.Enum;
using Game.Web.Card.DataStruct;
using Game.Web.Helper;

// ReSharper disable InconsistentNaming

namespace Game.Web.Card
{
    /// <summary>
    /// DataHandler 的摘要说明
    /// </summary>
    public class DataHandler : IHttpHandler, IRequiresSessionState
    {
        #region Fields

        /// <summary>
        /// 实例是否可重复使用
        /// </summary>
        public bool IsReusable => true;

        /// <summary>
        /// 响应实体
        /// </summary>
        private static AjaxJsonValid _ajv;

        /// <summary>
        /// 通用用户标识
        /// </summary>
        private static int UserId => _agentInfo.UserID;

        private static int AgentId => _agentInfo.AgentID;

        private static string Password => _agentInfo.Password;

        private static Entity.Agent.AgentInfo _agentInfo { get; set; }

        #endregion

        #region Router

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = GameRequest.GetString("action").ToLower();
            int version = GameRequest.GetInt("version", 2);

            #region Version 2.0 Router

            if (version == 2)
            {
                try
                {
                    //不需要认证的action
                    string[] unNeedAuthActions = {"agentauth"};
                    string token = GameRequest.GetString("token");
                    string authheader = context.Request.Headers["Authorization"];
                    _ajv = new AjaxJsonValid();
                    _ajv.SetDataItem("apiVersion", 20180316);

                    //排除不需要认证后判断认证是否正确
                    if (!unNeedAuthActions.Contains(action))
                    {
                        if (string.IsNullOrEmpty(token) &&
                            (string.IsNullOrEmpty(authheader) || !authheader.Contains("Bearer")))
                        {
                            _ajv.code = (int) ApiCode.VertyParamErrorCode;
                            _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " token 缺失");
                            context.Response.Write(_ajv.SerializeToJson());
                            return;
                        }
                        string authToken = !string.IsNullOrEmpty(token) ? token : authheader.Replace("Bearer ", "");
                        AgentTokenInfo authInfo = FacadeManage.aideNativeWebFacade.VerifyAgentToken(authToken);
                        if (authInfo == null)
                        {
                            _ajv.code = (int) ApiCode.Unauthorized;
                            _ajv.msg = EnumHelper.GetDesc(ApiCode.Unauthorized);
                            context.Response.Write(_ajv.SerializeToJson());
                            return;
                        }
                        //认证完成后 设置到全局
                        _agentInfo = FacadeManage.aideAgentFacade.GetAgentInfo(authInfo.AgentID, authInfo.UserID);
                    }

                    switch (action)
                    {
                        case "agentauth": //1.0
                            AgentAuth(GameRequest.GetString("mobile"), GameRequest.GetString("pass"));
                            break;
                        case "getinfo": //1.1
                            GetAgentInfo();
                            break;
                        case "getnicknamebygameid": //1.2
                            GetNickNameByGameID(GameRequest.GetInt("gameid", 0));
                            break;
                        case "getrecord": //1.3
                            GetRecord(GameRequest.GetString("type"));
                            break;
                        case "getbelowlist": //1.4
                            GetBelowList(GameRequest.GetString("type"));
                            break;
                        case "getawardinfo": //1.5
                            GetAwardInfo();
                            break;
                        case "presentscore": //1.6
                            PresentScore(GameRequest.GetInt("gameid", 0), GameRequest.GetString("password"),
                                GameRequest.GetInt("count", 0), Convert.ToByte(GameRequest.GetInt("type", 0)));
                            break;
                        case "setpassword": //1.7
                            SetSafePass(GameRequest.GetString("password"));
                            break;
                        case "updatepassword": //1.8
                            UpdateSafePass(GameRequest.GetString("oldPassword"), GameRequest.GetString("newPassword"));
                            break;
                        case "updateinfo": //1.9
                            UpdateAgentInfo(GameRequest.GetString("address"), GameRequest.GetString("phone"),
                                GameRequest.GetString("qq"));
                            break;
                        case "addagent": //1.10
                            AddBelowAgent(GameRequest.GetInt("gameid", 0), GameRequest.GetString("agentDomain"),
                                GameRequest.GetString("compellation"),
                                GameRequest.GetString("address"), GameRequest.GetString("phone"),
                                GameRequest.GetString("note")
                            );
                            break;
                        default:
                            _ajv.code = (int) ApiCode.VertyParamErrorCode;
                            _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                                " action 对应接口不存在");
                            break;
                    }

                    context.Response.Write(_ajv.SerializeToJson());
                }
                catch (Exception ex)
                {
                    Log4Net.WriteInfoLog("下面一条为接口故障信息", "Agent_DataHandler");
                    Log4Net.WriteErrorLog(ex, "Agent_DataHandler");
                    _ajv = new AjaxJsonValid
                    {
                        code = (int) ApiCode.LogicErrorCode,
                        msg = EnumHelper.GetDesc(ApiCode.LogicErrorCode)
                    };
                    context.Response.Write(_ajv.SerializeToJson());
                }
            }

            #endregion
        }

        #endregion

        #region Version 2.0 Logic

        #region 认证接口

        /// <summary>
        /// 代理手机号+安全密码认证 换取 Token
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pass"></param>
        private static void AgentAuth(string mobile, string pass)
        {
            if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(pass))
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                    " mobile、pass 缺失");
                return;
            }
            Message msg = FacadeManage.aideAgentFacade.AgentMobileLogin(mobile, pass, GameRequest.GetUserIP());
            if (msg.Success)
            {
                Entity.Agent.AgentInfo info = msg.EntityList[0] as Entity.Agent.AgentInfo;
                if (info != null)
                {
                    string token =
                        Fetch.SHA256Encrypt(
                            $"<{info.UserID}>,<{info.AgentID}>,<{info.AgentDomain}>,<{Fetch.ConvertDateTimeToUnix(DateTime.Now)}>");

                    FacadeManage.aideNativeWebFacade.SaveAgentToken(info, token);
                    _ajv.SetValidDataValue(true);
                    _ajv.SetDataItem("token", token);
                    _ajv.SetDataItem("expirtAt", DateTime.Now.AddDays(1));
                    return;
                }
            }

            _ajv.code = (int) ApiCode.Unauthorized;
            _ajv.msg = EnumHelper.GetDesc(ApiCode.Unauthorized);
        }

        #endregion

        #region 查询系列接口

        /// <summary>
        /// 获取代理信息汇总
        /// </summary>
        private static void GetAgentInfo()
        {
            Entity.Agent.SystemStatusInfo diamondSave =
                FacadeManage.aideAgentFacade.GetSystemStatusInfo(AppConfig.AgentConfig.ReceiveDiamondSave.ToString());
            Entity.Agent.SystemStatusInfo goldSave =
                FacadeManage.aideAgentFacade.GetSystemStatusInfo(AppConfig.AgentConfig.ReceiveGoldSave.ToString());

            AccountsInfo userInfo = FacadeManage.aideAccountsFacade.GetAccountsInfoByUserID(UserId);
            Entity.Agent.AgentInfo agentInfo =
                FacadeManage.aideAgentFacade.GetAgentInfo(AgentId, UserId);
            DataStruct.AgentInfo info = new DataStruct.AgentInfo
            {
                //来源用户表
                UserID = userInfo.UserID,
                GameID = userInfo.GameID,
                AgentID = userInfo.AgentID,
                FaceUrl = FacadeManage.aideAccountsFacade.GetAccountsFace(userInfo.CustomID).FaceUrl,
                NickName = userInfo.NickName,
                //来源代理表
                AgentLevel = agentInfo.AgentLevel == 1 ? "一级代理" : (agentInfo.AgentLevel == 2 ? "二级代理" : "三级代理"),
                AgentDomain = agentInfo.AgentDomain,
                Compellation = agentInfo.Compellation,
                ContactAddress = agentInfo.ContactAddress,
                ContactPhone = agentInfo.ContactPhone,
                WCNickName = agentInfo.WCNickName,
                QQAccount = agentInfo.QQAccount,
                //来源各种统计
                BelowAgent = agentInfo.BelowAgent,
                BelowUser = agentInfo.BelowUser,
                DiamondAward = agentInfo.DiamondAward,
                GoldAward = agentInfo.GoldAward,
                TotalDiamondAward = FacadeManage.aideAgentFacade.GetTotalDiamondAward(UserId),
                TotalGoldAward = FacadeManage.aideAgentFacade.GetTotalGoldAward(UserId),
                BelowAgentsUser = FacadeManage.aideAgentFacade.GetBelowAgentsUser(UserId),
                BelowAgentsAgent = FacadeManage.aideAgentFacade.GetBelowAgentsAgent(UserId),
                IsHasPassword = !agentInfo.Password.Equals("")
            };
            _ajv.SetValidDataValue(true);
            _ajv.SetDataItem("info", info);
            _ajv.SetDataItem("DiamondSave", diamondSave?.StatusValue ?? 0);
            _ajv.SetDataItem("GoldSave", goldSave?.StatusValue ?? 0);
        }

        /// <summary>
        /// 根据GameID查询用户昵称（检查对象存在用）
        /// </summary>
        /// <param name="gameId"></param>
        private static void GetNickNameByGameID(int gameId)
        {
            if (gameId == 0)
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " gameid 缺失");
                return;
            }
            AccountsInfo userInfo = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(gameId);
            if (userInfo?.UserID > 0)
            {
                _ajv.SetDataItem("NickName", userInfo.NickName);
            }
            else
            {
                _ajv.msg = "所查询的GameID不存在";
            }
            _ajv.SetValidDataValue(true);
        }

        /// <summary>
        /// 获取记录列表 by type
        /// </summary>
        /// <param name="type"></param>
        private static void GetRecord(string type)
        {
            int number = GameRequest.GetInt("pagesize", 10);
            int page = GameRequest.GetInt("pageindex", 1);
            PagerSet ps;
            string where;
            switch (type)
            {
                case "cost": //创建约战房消耗记录
                    where = $"WHERE UserID = {UserId}";
                    ps = FacadeManage.aidePlatformFacade.GetCreateRoomCost(where, page, number);
                    IList<CostDiamondRecord> costList = new List<CostDiamondRecord>();
                    if (ps?.PageCount > 0)
                    {
                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            costList.Add(new CostDiamondRecord
                            {
                                CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                RoomID = Convert.ToInt32(dr["RoomID"]),
                                CreateTableFee = Convert.ToInt64(dr["CreateTableFee"]),
                                DissumeDate = Convert.ToDateTime(dr["DissumeDate"]).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                        }
                    }
                    _ajv.SetDataItem("record", costList);
                    break;
                case "exchange": //钻石兑换金币记录
                    where = $" WHERE UserID = {UserId}";
                    ps = FacadeManage.aideRecordFacade.GetAgentExchangeDiamondRecord(where, page, number);
                    IList<ExchGoldRecord> exchList = new List<ExchGoldRecord>();
                    if (ps?.PageCount > 0)
                    {
                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            exchList.Add(new ExchGoldRecord
                            {
                                CollectDate = Convert.ToDateTime(dr["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                ExchDiamond = Convert.ToInt64(dr["ExchDiamond"]),
                                PresentGold = Convert.ToInt64(dr["PresentGold"]),
                                CurDiamond = Convert.ToInt64(dr["CurDiamond"]) - Convert.ToInt64(dr["ExchDiamond"])
                            });
                        }
                    }
                    _ajv.SetDataItem("record", exchList);
                    break;
                case "pay": //获取充值记录
                    where = $" WHERE UserID = {UserId} AND OrderStatus = 1 ";
                    ps = FacadeManage.aideTreasureFacade.GetPayDiamondRecord(where, page, number);
                    IList<PayDiamondRecord> payList = new List<PayDiamondRecord>();
                    if (ps?.PageCount > 0)
                    {
                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            payList.Add(new PayDiamondRecord
                            {
                                PayDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                PayDiamond = Convert.ToInt64(dr["Diamond"]) + Convert.ToInt64(dr["OtherPresent"]),
                                BeforeDiamond = Convert.ToInt64(dr["BeforeDiamond"]),
                                Amount = Convert.ToDecimal(dr["Amount"])
                            });
                        }
                    }
                    _ajv.SetDataItem("record", payList);
                    break;
                case "present": //钻石赠送记录
                    where = $"WHERE SourceUserID = {UserId}";
                    ps = FacadeManage.aideRecordFacade.GetAgentPresentDiamondRecord(where, page, number);
                    IList<PresentDiamondRecord> presentList = new List<PresentDiamondRecord>();
                    if (ps?.PageCount > 0)
                    {
                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            presentList.Add(new PresentDiamondRecord
                            {
                                CollectDate = Convert.ToDateTime(dr["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                GameID = FacadeManage.aideAccountsFacade.GetGameIDByUserID(
                                    Convert.ToInt32(dr["TargetUserID"])),
                                SourceDiamond = Convert.ToInt64(dr["SourceDiamond"]),
                                PresentDiamond = Convert.ToInt64(dr["PresentDiamond"]),
                                CollectNote = dr["CollectNote"].ToString()
                            });
                        }
                    }
                    _ajv.SetDataItem("record", presentList);
                    break;
                case "register": //代理商推广记录
                    ps = new PagerSet();
                    DataSet ds = FacadeManage.aideAccountsFacade.GetAgentSpreadList(UserId, page, number);
                    IList<SpreadRegsiterRecord> regsiterList = new List<SpreadRegsiterRecord>();
                    if (ds?.Tables.Count > 0)
                    {
                        ps.PageSet = ds;
                        ps.RecordCount = Convert.ToInt32(FacadeManage.aideAccountsFacade.GetAgentSpreadCount(UserId));
                        ps.PageCount = ps.RecordCount / number;

                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            regsiterList.Add(new SpreadRegsiterRecord
                            {
                                RegisterDate = Convert.ToDateTime(dr["RegisterDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                GameID = Convert.ToInt32(dr["GameID"]),
                                RegisterOrigin = Fetch.RegisterOrigin(Convert.ToInt32(dr["RegisterOrigin"])),
                                AgentState = Convert.ToInt32(dr["AgentID"]) > 0 ? "代理商" : "非代理商"
                            });
                        }
                    }
                    _ajv.SetDataItem("record", regsiterList);
                    break;
                default:
                    _ajv.code = (int) ApiCode.VertyParamErrorCode;
                    _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " type 无对应记录");
                    return;
            }

            _ajv.SetDataItem("pageCount", ps?.PageCount);
            _ajv.SetDataItem("recordCount", ps?.RecordCount);
            _ajv.SetValidDataValue(true);
        }

        /// <summary>
        /// 用户代理列表获取接口
        /// </summary>
        private static void GetBelowList(string type)
        {
            int number = GameRequest.GetInt("pagesize", 15);
            int page = GameRequest.GetInt("pageindex", 1);
            long pCount = 0;
            BelowList list = new BelowList();

            string sqlWhere = $"WHERE AgentID IN (SELECT AgentID FROM AgentInfo(NOLOCK) WHERE UserID={UserId})";
            if (!string.IsNullOrEmpty(type))
            {
                sqlWhere += type == "agent" ? " AND UserID IN (SELECT UserID FROM AgentInfo(NOLOCK)) " : " AND UserID NOT IN (SELECT UserID FROM AgentInfo(NOLOCK)) ";
            }
            var ps = FacadeManage.aideAgentFacade.GetBelowListPagerSet(sqlWhere, page, number);
            if (ps?.PageCount > 0)
            {
                foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                {
                    AccountsInfo ai = FacadeManage.aideAccountsFacade.GetAccountsInfoByUserID(Convert.ToInt32(dr["UserID"].ToString()));
                    BelowDetail detail = new BelowDetail
                    {
                        UserID = ai.UserID,
                        GameID = ai.GameID,
                        NickName = ai.NickName,
                        AgentID = ai.AgentID,
                        RegisterDate = Convert.ToDateTime(dr["CollectDate"]).ToString("yyyy/MM/dd HH:mm:ss"),
                        TotalDiamond = FacadeManage.aideAgentFacade.GetTotalDiamondAward(UserId, ai.UserID),
                        TotalGold = FacadeManage.aideAgentFacade.GetTotalGoldAward(UserId, ai.UserID)
                    };
                    pCount++;
                    list.dataList.Add(detail);
                }
            }
            _ajv.SetDataItem("list", list.dataList);
            _ajv.SetDataItem("total", ps?.RecordCount ?? 0);
            _ajv.SetDataItem("count", pCount);
            _ajv.SetDataItem("pageCount", ps?.PageCount ?? 0);
            _ajv.SetValidDataValue(true);
        }

        /// <summary>
        /// 代理返利情况
        /// </summary>
        private static void GetAwardInfo()
        {
            Dictionary<string, long> dicGoldDetail = new Dictionary<string, long>
            {
                {"TotalAward", FacadeManage.aideAgentFacade.GetTotalGoldAward(UserId, Fetch.GetMonthTime())},
                {"MonthAward", FacadeManage.aideAgentFacade.GetTotalGoldAward(UserId, Fetch.GetMonthTime())},
                {"LastMonthAward", FacadeManage.aideAgentFacade.GetTotalGoldAward(UserId, Fetch.GetLastMonthTime())},
                {"TotalReceive", FacadeManage.aideAgentFacade.GetTotalGoldReceive(UserId)}
            };
            Dictionary<string, long> dicDiamondDetail = new Dictionary<string, long>
            {
                {"TotalAward", FacadeManage.aideAgentFacade.GetTotalDiamondAward(UserId, Fetch.GetMonthTime())},
                {"MonthAward", FacadeManage.aideAgentFacade.GetTotalDiamondAward(UserId, Fetch.GetMonthTime())},
                {"LastMonthAward", FacadeManage.aideAgentFacade.GetTotalDiamondAward(UserId, Fetch.GetLastMonthTime())},
                {"TotalReceive", FacadeManage.aideAgentFacade.GetTotalDiamondReceive(UserId)}
            };
            Dictionary<string, object> dicAwardInfo =
                new Dictionary<string, object> {{"Gold", dicGoldDetail}, {"Diamond", dicDiamondDetail}};
            _ajv.SetValidDataValue(true);
            _ajv.SetDataItem("info", dicAwardInfo);
        }

        #endregion

        #region 业务操作接口

        /// <summary>
        /// 赠送钻石接口
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="pass"></param>
        /// <param name="count"></param>
        /// <param name="type"></param>
        private static void PresentScore(int gameId, string pass, int count, byte type)
        {
            if (count == 0)
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                    " count 缺失");
                return;
            }
            if (gameId > 0 && string.IsNullOrEmpty(pass))
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                    " pass 转赠时安全密码必填");
                return;
            }
            if (gameId > 0 && !IsPassChecked(pass))
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                    " pass 安全密码不正确");
                return;
            }

            //代理领取返利
            Message msg =
                FacadeManage.aideAgentFacade.ReceiveAgentAward(UserId, type, count);
            if (gameId > 0) //当填写了别人的GameID为转赠功能
            {
                //代理转赠返利
                msg = FacadeManage.aideAgentFacade.GiveAgentAward(UserId, gameId, type, count, pass);
            }

            if (msg.Success)
            {
                _ajv.SetValidDataValue(true);
                _ajv.msg = gameId > 0 ? "提取并转赠成功" : "提取成功";
            }
            else
            {
                _ajv.code = msg.MessageID;
                _ajv.msg = msg.Content;
            }
        }

        /// <summary>
        /// 首次设置安全密码接口
        /// </summary>
        /// <param name="pass"></param>
        private static void SetSafePass(string pass)
        {
            if (string.IsNullOrEmpty(pass) || pass.Length != 32)
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                    " password 参数不正确或未加密");
                return;
            }
            int spResult = FacadeManage.aideAccountsFacade.SetAgentSafePassword(AgentId, pass);
            if (spResult > 0)
            {
                _ajv.SetValidDataValue(true);
                _ajv.msg = "设置安全密码成功";
            }
            else
            {
                _ajv.msg = "设置安全密码失败";
            }
        }

        /// <summary>
        /// 修改安全密码接口
        /// </summary>
        /// <param name="oldPass"></param>
        /// <param name="newPass"></param>
        private static void UpdateSafePass(string oldPass, string newPass)
        {
            if (!IsPassChecked(oldPass))
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                    " oldPassword 不正确");
                return;
            }
            if (string.IsNullOrEmpty(newPass) || newPass.Length != 32)
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                    " newPassword 参数不正确或未加密");
                return;
            }
            int upResult = FacadeManage.aideAccountsFacade.SetAgentSafePassword(AgentId, newPass);
            if (upResult > 0)
            {
                _ajv.SetValidDataValue(true);
                _ajv.msg = "修改安全密码成功";
            }
            else
            {
                _ajv.msg = "修改安全密码失败";
            }
        }

        /// <summary>
        /// 修改代理信息接口
        /// </summary>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="qq"></param>
        private static void UpdateAgentInfo(string address, string phone, string qq)
        {
            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(qq))
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                    " address、phone、qq 缺失");
                return;
            }
            if (!Validate.IsMobileCode(phone))
            {
                _ajv.msg = "抱歉，联系电话格式不正确";
                return;
            }
            AccountsAgentInfo uiAgent = new AccountsAgentInfo()
            {
                UserID = UserId,
                ContactAddress = address,
                ContactPhone = phone,
                QQAccount = qq
            };
            int uiResult = FacadeManage.aideAccountsFacade.UpdateAgentInfo(uiAgent);
            if (uiResult > 0)
            {
                _ajv.SetValidDataValue(true);
                _ajv.msg = "修改代理信息成功";
            }
            else
            {
                _ajv.msg = "修改代理信息失败";
            }
        }

        /// <summary>
        /// 添加下级代理接口
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="agentDomain"></param>
        /// <param name="compllation"></param>
        /// <param name="note"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="qq"></param>
        /// <param name="wcNickName"></param>
        private static void AddBelowAgent(int gameId, string agentDomain, string compllation,
            string address, string phone, string note)
        {
            if (gameId <= 0)
            {
                _ajv.msg = "抱歉，添加代理游戏ID不能为空";
                return;
            }
            if (string.IsNullOrEmpty(compllation))
            {
                _ajv.msg = "抱歉，真实姓名不能为空";
                return;
            }
            if (string.IsNullOrEmpty(phone))
            {
                _ajv.msg = "抱歉，联系电话不能为空";
                return;
            }
            if (!Validate.IsMobileCode(phone))
            {
                _ajv.msg = "抱歉，联系电话格式不正确";
                return;
            }
            if (string.IsNullOrEmpty(agentDomain))
            {
                _ajv.msg = "抱歉，代理域名不能为空";
                return;
            }

            AccountsInfo account = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(gameId);
            if (account == null)
            {
                _ajv.msg = "抱歉，添加代理异常，请稍后重试";
                return;
            }

            Entity.Agent.AgentInfo info = new Entity.Agent.AgentInfo
            {
                AgentDomain = agentDomain,
                AgentNote = note,
                Compellation = compllation,
                ContactAddress = address,
                ContactPhone = phone,
                WCNickName = account.NickName
            };

            Message msg = FacadeManage.aideAgentFacade.AddAgent(UserId, info, gameId);
            if (msg.Success)
            {
                _ajv.SetValidDataValue(true);
                _ajv.msg = "添加下级代理成功";
            }
            else
            {
                _ajv.msg = "添加下级代理失败";
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 验证安全密码是否正确
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        private static bool IsPassChecked(string pass)
        {
            return pass == Password;
        }

        #endregion

        #endregion
    }
}
