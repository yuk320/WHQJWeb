<template>
  <div class="ui-main ui-perfomance">
    <top :nav="nav"></top>
    <div class="ui-perfomance-info">
      <div class="ui-perfomance-diamond">
        <h2><img src="../assets/images/diamond.png" />钻石奖励总览<router-link to="/payInfo">查看明细</router-link></h2>
        <ul>
          <li>
            <h3>总累计奖励</h3>
            <div>{{ info.Diamond.TotalAward }}</div>
          </li>
          <li>
            <h3>上月奖励</h3>
            <div>{{ info.Diamond.LastMonthAward }}</div>
          </li>
          <li>
            <h3>当月奖励</h3>
            <div>{{ info.Diamond.MonthAward }}</div>
          </li>
          <li>
            <h3>当月累计提成汇总</h3>
            <div>{{ info.Diamond.TotalReceive }}</div>
          </li>
        </ul>
        <!-- <p>总累计奖励 ：<span>123456</span></p>
        <p>上月奖励 ：<span>10000</span></p>
        <p>当月直属玩家奖励 ：<span>10000</span></p>
        <p>当月累计提成汇总 ：<span>10000</span></p> -->
      </div>
      <div class="ui-perfomance-gmoney">
        <h2><img src="../assets/images/gold.png" />游戏币奖励总览<router-link to="/expendInfo">查看明细</router-link></h2>
        <ul>
          <li>
            <h3>总累计奖励</h3>
            <div>{{ info.Gold.TotalAward }}</div>
          </li>
          <li>
            <h3>上月奖励</h3>
            <div>{{ info.Gold.LastMonthAward }}</div>
          </li>
          <li>
            <h3>当月奖励</h3>
            <div>{{ info.Gold.MonthAward }}</div>
          </li>
          <li>
            <h3>当月累计提成汇总</h3>
            <div>{{ info.Gold.TotalReceive }}</div>
          </li>
        </ul>
      </div>
    </div>
  </div>



</template>
<script>
import top from "./common/top";
import UiTable from "./table/vue-table";
import { getAwardInfo } from "../fetch/fetch"; 
export default {
  name: "perfomance",
  props: {
    nav: String
  },
  components: { top, UiTable },
  data: function() {
    return {
      info: {}
    };
  },
  created() {
    getAwardInfo({ token: localStorage.getItem("token") }, data => {
      if (data.data.valid) {
        this.info = data.data.info;
      }
    });
  },
  methods:{

  }
};
</script>
<style scoped>
.ui-perfomance-info{
  width: 96%;
  margin: 0 auto;
}

/* 钻石 */
.ui-perfomance-diamond{
  margin: 0.4rem auto 0;
  border-bottom:1px solid #dedfe0;
  padding: 0.16rem 0;
}
.ui-perfomance-diamond>h2{
  font-size: 0.34rem;
  margin: 0.1rem 0 0.2rem 0.2rem;
  position: relative;
}
.ui-perfomance-diamond>h2>img{
  width: 0.5rem;
  vertical-align: middle;
  margin-right: 0.1rem;
}
.ui-perfomance-diamond>h2>a{
  display: inline-block;
  padding: 0.06rem 0.1rem 0.06rem;
  line-height: 0.4rem;
  font-size: 0.3rem;
  color: #000;
  background: #fff;
  border: 1px solid #ccc;
  border-radius: 0.12rem;
  font-weight: 400;
  width: 1.2rem;
  position: absolute;
  right: 0;
}
.ui-perfomance-diamond>ul{
  display: -webkit-flex;
  display: flex;
  flex-wrap: wrap;
  justify-content:space-around;
  
}
.ui-perfomance-diamond li{
  margin: 0.1rem 0;
  width: 2.8rem;
  border: 1px solid #ccc;
}
.ui-perfomance-diamond li h3{
  height: 0.54rem;
  line-height: 0.54rem;
  background: #fff;
  font-size: 0.26rem;
  color: #000;
  border-bottom: 1px solid #ccc;
  font-weight: 400;
  text-align: center;
  margin: 0;
}
.ui-perfomance-diamond li>div{
  height: 1rem;
  line-height: 1rem;
  text-align: center;
  background: #F5F4F4;
  color: #000;
  font-size: 0.44rem;
}


/* 游戏币 */
.ui-perfomance-gmoney{
  margin: 0.2rem auto 0;
  padding: 0.16rem 0;
  position: relative;
}
.ui-perfomance-gmoney>h2{
  font-size: 0.34rem;
  margin: 0.1rem 0 0.2rem 0.2rem;
  position: relative;
}
.ui-perfomance-gmoney>h2>img{
  width: 0.5rem;
  vertical-align: middle;
  margin-right: 0.1rem;
}
.ui-perfomance-gmoney>h2>a{
  display: inline-block;
  padding: 0.06rem 0.1rem 0.06rem;
  line-height: 0.4rem;
  font-size: 0.3rem;
  color: #000;
  background: #fff; 
  border: 1px solid #ccc;
  border-radius: 0.12rem;
  font-weight: 400;
  width: 1.2rem;
  position: absolute;
  right: 0;
}
.ui-perfomance-gmoney>ul{
  display: -webkit-flex;
  display: flex;
  flex-wrap: wrap;
  justify-content:space-around;
  
}
.ui-perfomance-gmoney li{
  margin: 0.1rem 0;
  width: 2.8rem;
  border: 1px solid #ccc;
}
.ui-perfomance-gmoney li h3{
  height: 0.54rem;
  line-height: 0.54rem;
  background: #fff;
  font-size: 0.26rem;
  color: #000;
  border-bottom: 1px solid #ccc;
  font-weight: 400;
  text-align: center;
  margin: 0;
}
.ui-perfomance-gmoney li>div{
  height: 1rem;
  line-height: 1rem;
  text-align: center;
  background: #F5F4F4;
  color: #000;
  font-size: 0.44rem;
}
</style>

