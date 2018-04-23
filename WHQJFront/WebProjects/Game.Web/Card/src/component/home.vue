<template>
  <div class="ui-main">
    <loading :loading="loading" />
    <top :nav="nav"></top>
    <div class="ui-main-info">
      <div class="ui-info-mydetail">
        <ul>
          <li class="ui-mydetail-1 ui-left">
            <p>我的昵称：<span>{{info.NickName}}</span></p>
            <p>真实姓名：<span>{{info.Compellation}}</span></p>
            <p>我的ID：<span>{{info.GameID}}</span></p>
            <p>联系电话：<span>{{info.ContactPhone}}</span></p>
            <p>身份证号码：<span>{{info.PassPortID}}</span></p>
          </li>
        </ul>  
      </div>
      <div class="ui-info-myqr">
        <img src="../assets/images/face.png"/>
        <a href="javascript:;">点击保存
          我的二维码</a>
      </div>
    </div>
    <div class="ui-main-agent">
      <p>我的直属玩家 ：<span>{{info.BelowUser}}</span><router-link to="/BelowUser">查看明细</router-link></p>
      <p>我的直属代理 ：<span>{{info.BelowAgent}}</span><router-link to="/BelowAgent">查看明细</router-link></p>
      <p>我的下线玩家 ：<span>{{info.BelowAgentsUser}}</span></p>
      <p>我的下级代理 ：<span>{{info.BelowAgentsAgent}}</span></p>
    </div>
    <div class="ui-main-diamond">
      <h2><img src="../assets/images/diamond.png" />钻石提成<router-link to="/payInfo">查看详情</router-link></h2>
      <p>总累计奖励 ：<span>{{info.TotalDiamondAward}}</span></p>
      <p>可提取钻石 ：<span>{{info.DiamondAward}}</span><a href="javascript:;" :disabled="disabled" @click="takeDiamond">提&nbsp;取</a></p>
    </div>
    <div class="ui-main-gmoney">
      <h2><img src="../assets/images/gold.png" />游戏币提成<router-link to="/expendInfo">查看详情</router-link></h2>
      <p>总累计奖励 ：<span>{{info.TotalGoldAward}}</span></p>
      <p>可提取游戏币 ：<span>{{info.GoldAward}}</span><a href="javascript:;" :disabled="disabled" @click="takeGold">提&nbsp;取</a></p>
    </div>
    <ui-dialog :show="msg.show">
      <extract :content="msg.content" :title="msg.msg" :info="info" :limit="save[msg.content]" v-on:close="closeDialog" :state="msg.state"></extract>
    </ui-dialog>
  </div>
</template>

<script>
import top from "./common/top";
import loading from "./common/loading";
import UiDialog from "./common/dialog";
import message from "./common/message";
import extract from "./extract";
import { mapMutations } from "vuex";
import { getInfo } from "../fetch/fetch";
export default {
  name: "home",
  props: {
    nav: String
  },
  components: { top, loading, UiDialog, message, extract },
  data: function() {
    return {
      loading: false,
      disabled: false,
      save: {},
      msg: {
        show: false,
        content: null,
        state: false,
        msg:null
      },
      info: {}
    };
  },
  created() {
    getInfo({ token: localStorage.getItem("token") }, data => {
      if (data.data.valid) {
        this.info = data.data.info;
        this.save = {
          "钻石":data.data.DiamondSave,
          "游戏币":data.data.GoldSave
        };
        // 若用户没有设置安全密码则跳转到安全密码页面
        if (!data.data.info.IsHasPassword) {
          this.$router.push("/setPassword");
        }
      }
    });
  },
  methods: {
    singOut: function() {
      localStorage.removeItem("token");
      this.$router.push("/Login");
    },
    closeDialog: function() {
      this.msg.show = false;
    },
    takeDiamond: function() {
      this.msg.content = "钻石";
      this.msg.show = true;
      this.msg.msg = "";      
    },
    takeGold: function() {
      this.msg.content = "游戏币";
      this.msg.show = true;
      this.msg.msg = "";      
    }
  }
};
</script>

<style scoped>
.ui-main {
  height: 100%;
}
.ui-head-portrait {
  margin: 0 0 0.4rem 0;
  padding-top: 0.33rem;
  border-bottom: 1px solid #dedfe0;
  background: #fff;
}
.ui-face-box {
  height: 1.7rem;
  width: 1.7rem;
  margin: 0 auto 0.16rem;
  padding: 0.05rem;
  border: 1px solid #dedfe0;
  border-radius: 0.25rem;
}
.ui-face-box img {
  height: 100%;
  width: 100%;
  border-radius: 0.2rem;
}
.ui-name {
  width: 100%;
  text-align: center;
  margin-top: 0.2rem;
}
.ui-item:last-child .ui-link {
  border: none;
}

/* 个人信息 */
.ui-main-info {
  width: 96%;
  margin: 0 auto;
  display: flex;
  display: -webkit-flex;
  justify-content: space-between;
  position: relative;
}
.ui-info-mydetail {
  width: 79%;
}
.ui-info-mydetail > ul > li {
  display: inline-block;
  margin-left: 0.2rem;
}
.ui-info-mydetail > ul > li p {
  margin: 0.2rem 0;
  font-size: 0.3rem;
}
.ui-info-mydetail > ul > li img {
  width: 0.3rem;
  vertical-align: -10%;
}
.ui-info-IDCard > span {
  font-size: 0.26rem;
}
.ui-info-myqr {
  text-align: right;
}
.ui-info-myqr img {
  width: 1.5rem;
  height: 1.5rem;
}
.ui-info-myqr a {
  width: 1.2rem;
  font-size: 0.24rem;
  display: block;
  text-align: center;
  padding: 0 0.15rem 0;
  color: #000;
  font-weight: 900;
}

/* 首页直属 */
.ui-main-agent {
  width: 96%;
  margin: 0 auto;
  border-bottom: 1px solid #dedfe0;
  border-top: 1px solid #dedfe0;
  position: relative;
}
.ui-main-agent p {
  margin: 0.12rem 0 0.12rem 0.2rem;
  font-size: 0.3rem;
  height: 0.56rem;
}
.ui-main-agent p a {
  display: inline-block;
  padding: 0.06rem 0.1rem 0.06rem;
  line-height: 0.4rem;
  font-size: 0.3rem;
  background: #fff;
  color: #000;
  border: 1px solid #ccc;
  border-radius: 0.12rem;
  position: absolute;
  left: 4.8rem;
  width: 1.2rem;
}

/* 钻石提成 */
.ui-main-diamond {
  width: 96%;
  margin: 0 auto;
  border-bottom: 1px solid #dedfe0;
  padding: 0.16rem 0;
  position: relative;
}
.ui-main-diamond > h2 {
  font-size: 0.34rem;
  margin: 0.1rem 0 0.1rem 0.2rem;
}
.ui-main-diamond > h2 > img {
  width: 0.5rem;
  vertical-align: middle;
  margin-right: 0.1rem;
}
.ui-main-diamond > h2 > a {
  display: inline-block;
  padding: 0.06rem 0.1rem 0.06rem;
  line-height: 0.4rem;
  font-size: 0.3rem;
  background: #fff;
  color: #000;
  border: 1px solid #ccc;
  border-radius: 0.12rem;
  font-weight: 400;
  position: absolute;
  left: 4.8rem;
  width: 1.2rem;
}
.ui-main-diamond > p {
  margin: 0.12rem 0 0.12rem 0.2rem;
  font-size: 0.3rem;
  height: 0.56rem;
}
.ui-main-diamond > p > a {
  display: inline-block;
  padding: 0.06rem 0.1rem 0.06rem;
  line-height: 0.4rem;
  font-size: 0.3rem;
  background: #fff;
  color: #000;
  border: 1px solid #ccc;
  border-radius: 0.12rem;
  position: absolute;
  left: 4.8rem;
  text-align: center;
  width: 1.2rem;
}

/* 游戏币提成 */
.ui-main-gmoney {
  width: 96%;
  margin: 0 auto;
  padding: 0.16rem 0 0;
  position: relative;
}
.ui-main-gmoney > h2 {
  font-size: 0.34rem;
  margin: 0.1rem 0 0.1rem 0.2rem;
}
.ui-main-gmoney > h2 > img {
  width: 0.5rem;
  vertical-align: middle;
  margin-right: 0.1rem;
}
.ui-main-gmoney > h2 > a {
  display: inline-block;
  padding: 0.06rem 0.1rem 0.06rem;
  line-height: 0.4rem;
  font-size: 0.3rem;
  background: #fff;
  color: #000;
  border: 1px solid #ccc;
  border-radius: 0.12rem;
  font-weight: 400;
  position: absolute;
  left: 4.8rem;
  width: 1.2rem;
}
.ui-main-gmoney > p {
  margin: 0.12rem 0 0.12rem 0.2rem;
  font-size: 0.3rem;
  height: 0.56rem;
}
.ui-main-gmoney > p > a {
  display: inline-block;
  padding: 0.06rem 0.1rem 0.06rem;
  line-height: 0.4rem;
  font-size: 0.3rem;
  background: #fff;
  color: #000;
  border: 1px solid #ccc;
  border-radius: 0.12rem;
  position: absolute;
  left: 4.8rem;
  text-align: center;
  width: 1.2rem;
}
@media only screen and (max-width: 320px) {
  .ui-info-mydetail > ul > li span {
    font-size: 0.24rem;
  }
  .ui-mydetail-1 {
    margin-right: 3%;
  }
  .ui-dialog-message {
    width: 6.2rem;
    margin-left: -3.1rem;
  }
}
</style>
