<template>
  <div class="ui-usermenu fn-clear">
    <ul class="ui-list">
      <li v-for="(item, index) in items" :key="index" class="ui-item">
        <router-link :to="item.left.path" class="ui-link"  @click="changeTab(item.left.nav)" :disabled="disabled" :class="nav === item.left.nav ? 'active' : ''">
          {{item.left.title}}
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "UiNav",
  props: {
    nav: String
  },
  data: function() {
    return {
      loading: false,
      disabled: false,
      items: [
        {
          left: {
            class: 'ui-info',
            nav: 'Home',
            title: '首页',
            path: '/Home'
          }
        },
        {
          left: {
            class: '',
            nav: 'Below',
            title: '我的直属',
            path: '/Below'
          }
        },
        {
          left: {
            class: '',
            nav: 'Award',
            title: '我的业绩',
            path: '/Award'
          }
        }
      ]
    }
  },
  methods:{
    
  }
};
</script>
<style scoped>
.ui-list {
  /* border: 1px solid #dedfe0; */
  display: flex;
  display: -webkit-flex;
  width: 98%;
  margin: 0.28rem auto 0.1rem;
}
.ui-item {
  height: 0.8rem;
  line-height: 0.8rem;
  text-align: center;
  width: 38%;
}
.ui-link > img {
  width: 0.18rem;
  float: right;
  margin: 0.34rem 0.24rem 0 0;
}
.ui-link {
  box-sizing: border-box;
  height: 100%;
  display: list-item;
  font-size: 0.35rem;
  color: #000;
  background: #fff;
}
.ui-item:nth-of-type(1){
  border-radius: 0.14rem 0 0 0.14rem;
  border: 1px solid #ccc;
  border-right:none;

}
.ui-item:nth-of-type(2){
  border: 1px solid #ccc;
}
.ui-item:nth-of-type(3){
  border-radius: 0 0.14rem 0.14rem 0;
  border: 1px solid #ccc;
  border-left:none;
}
.ui-item:first-child .ui-link{
  border-radius: 0.14rem 0 0 0.14rem;
}
.ui-item:nth-of-type(3) .ui-link{
  border-radius: 0 0.14rem 0.14rem 0;
}
.active {
  color:#000;
  background: #EBE9E9;
}
</style>
