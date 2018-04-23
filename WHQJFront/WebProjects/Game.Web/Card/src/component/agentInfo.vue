<template>
  <div class="ui-main ui-info">
    <top></top>
    <table>
      <tbody>
        <tr>
          <td>我的昵称</td>
          <td>{{info.NickName}}</td>
        </tr>
        <tr>
          <td>我的ID</td>
          <td>{{info.GameID}}</td>
        </tr>
        <tr>
          <td>真实姓名</td>
          <td>{{info.Compellation}}</td>
        </tr>
        <tr>
          <td>联系电话</td>
          <td>{{info.ContactPhone}}</td>
        </tr>
        <tr>
          <td>联系QQ</td>
          <td>{{info.QQAccount}}</td>
        </tr>
        <tr>
          <td>联系微信</td>
          <td>{{info.WCNickName}}</td>
        </tr>
      </tbody>
    </table>
    <table>
      <tbody>
        <tr>
          <td>代理域名</td>
          <td>{{info.AgentDomain}}</td>
        </tr>
        <tr>
          <td>直属代理</td>
          <td class="ui-agent-number">
            <span>{{info.BelowAgent}}人</span><router-link to="/Under?type=agent">详情</router-link>
          </td>
        </tr>
        <tr>
          <td>直属玩家</td>
          <td class="ui-player-number">
            <span>{{info.BelowUser}}人</span><router-link to="/Under?type=user">详情</router-link>
          </td>
        </tr>
         <tr>
          <td>直属代理的下线代理</td>
          <td class="ui-agent-number">
            <span>{{info.BelowAgentsAgent}}人</span>
          </td>
        </tr>
        <tr>
          <td>直属代理的下线玩家</td>
          <td class="ui-player-number">
            <span>{{info.BelowAgentsUser}}人</span>
          </td>
        </tr>
      </tbody>
    </table>
    <table>
      <tbody>
        <tr>
          <td>当前钻石</td>
          <td>{{info.Diamond}}</td>
        </tr>
        <tr>
          <td>充值返利总奖励</td>
          <td>{{info.TotalDiamondAward}}</td>
        </tr>
        <tr>
          <td>可提取奖励</td>
          <td>{{info.DiamondAward}}</td>
        </tr>
      </tbody>
    </table>
    <table>
      <tbody>
        <tr>
          <td>当前金币</td>
          <td>{{info.Gold}}</td>
        </tr>
        <tr>
          <td>游戏税收返利总奖励</td>
          <td>{{info.TotalGoldAward}}</td>
        </tr>
        <tr>
          <td>可提取奖励</td>
          <td>{{info.GoldAward}}</td>
        </tr>
      </tbody>
    </table>
    <div class="ui-main-exit">
      <input type="submit" value="退出当前账号" @click="singOut"/>
    </div>
  </div>
</template>

<script>
import top from './common/top'
import { getInfo } from '../fetch/fetch'

export default {
  name: 'info',
  components: { top },
  data: function() {
    return {
      info: {}
    }
  },
  created() {
    getInfo({ token: localStorage.token }, data => {
      if (data.data.valid) {
        this.info = data.data.info
      }
    })
  },
  methods: {
    singOut: function() {
      localStorage.removeItem('token');
      this.$router.push('/Login')
    }
  }
}
</script>

<style scoped>
.ui-agent-number {
  position: relative;
}
.ui-agent-number > a,
.ui-player-number > a {
  color: #fff;
  display: inline-block;
  width: 1rem;
  height: 80%;
  position: absolute;
  right: 0.2rem;
  top: 0.06rem;
  line-height: 0.5rem;
  border-radius: 0.1rem;
  background: #0f7fd5;
  font-size: 0.3rem;
}
.ui-player-number {
  position: relative;
}
.ui-info > table:nth-child(2) {
  margin-top: 20px;
}
.ui-info > table:nth-child(3) {
  margin-top: 20px;
}
.ui-info > table:nth-child(4) {
  margin-top: 20px;
}
.ui-info > table:nth-child(5) {
  margin-top: 20px;
}
</style>
