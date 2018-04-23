<template>
  <div class="ui-dialog-message ui-underdetail">
    <div class="ui-message-close">
      {{title}}
      <a @click="$emit('close')"><img :src="closeImg"/></a>
    </div>
    <div class="ui-underdetail-content">
      <table>
        <tbody>
          <tr>
            <td>游戏ID：</td>
            <td>{{belowDetail.GameID}}</td>
          </tr>
          <tr>
            <td>昵称：</td>
            <td>{{belowDetail.NickName}}</td>
          </tr>
          <tr>
            <td>类型：</td>
            <td>{{belowDetail.TypeDesc}}</td>
          </tr>
          <tr>
            <td>累计充值贡献：</td>
            <td>{{belowDetail.TotalDiamond}}</td>
          </tr>
          <tr>
            <td>累计游戏税收贡献</td>
            <td>{{belowDetail.TotalGold}}</td>
          </tr>
        </tbody>
      </table>
    </div>
    <!-- <div class="ui-underdetail-close">
      <a @click="$emit('close')">关闭</a>
    </div> -->
    <button class="ui-confirm" @click="$emit('close')">确&ensp;定</button>
  </div>
</template>
<script>
export default {
  name: "belowDetail",
  props: {
    belowDetail: Object,
    title:String
  },
  data: function() {
    return {
      closeImg: '../../assets/images/close.png',
    }
  }
};
</script>
<style scoped>
.ui-dialog-message {
  position: absolute;
  z-index: 1001;
  left: 50%;
  top: 3rem;
  background: #fff;
  width: 7rem;
  margin-left:-3.5rem;
  border-radius: 0.14rem;
  box-shadow: 0rem 0rem 0.2rem rgb(85, 84, 84);
}
.ui-message-close{
  position: relative;
  left: 50%;
  top: 0.06rem;
  width: 98%;
  height: 0.6rem;
  background:#666;
  color:#fff;
  margin-left: -49%;
  border-radius: 0.1rem;
  text-align: center;
  font-size: 0.4rem;
}
.ui-message-close img{
  position: absolute;
  width: 7%;
  right: 0.1rem;
  top: 0.06rem;
}
/* .ui-underdetail-close a {
  display: inline-block;
  width: 68%;
  height: 2em;
  line-height: 2em;
  text-align: center;
  background: #E80511;
  color: #fff;
  border-radius: 0.2rem;
} */
/* .ui-underdetail-close{
  text-align: center;
  height:1rem;
  line-height: 1rem;
} */
.ui-underdetail-content td{
  text-align: center;
  border-top: 0;
  border-bottom: 1px solid #dedfe0;
  height: 0.8rem;
  color: #000;
}
.ui-confirm{
  background: #1AAD19;
  border: none;
  color: #fff;
  font-size: 0.44rem;
  text-align: center;
  position: relative;
  left: 48%;
  top: -0.06rem;
  width: 98%;
  height: 0.8rem;
  margin-left: -47%;
  border-radius: 0.1rem;
}
.ui-underdetail-content > table{
  margin: 0.1rem auto 0.18rem;
  width: 98%;
}
@media screen and (max-width:320px) {
  .ui-dialog-message{
    width: 6.2rem;
    margin-left: -3.1rem;
  }
}
</style>
