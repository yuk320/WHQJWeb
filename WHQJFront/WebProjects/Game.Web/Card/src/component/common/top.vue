<template>
  <div>
  <div class="ui-top fn-clear">
    <span>代理中心</span>
    <slot>
      <!-- 此处元素必须添加类名ui-top-right -->
    </slot>
  </div>
  <ui-nav :nav='nav' v-show="navShow"></ui-nav>
</div>
</template>

<script>
import UiNav from "./nav";
export default {
  name: "top",
  props: {
    back: {
      type: Boolean,
      default: true
    },
    nav: String
  },
  components : { UiNav },
  data: function() {
    return {
      backImg: "./assets/images/arrow-left.png",
      navShow:false,
      items: [
        {
          left: {
            class: "ui-info",
            nav: "Home",
            title: "首页",
            path: "/Home"
          }
        },
        {
          left: {
            class: "",
            nav: "Below",
            title: "我的直属",
            path: "/Below"
          }
        },
        {
          left: {
            class: "",
            nav: "Award",
            title: "我的业绩",
            path: "/Award"
          }
        }
      ]
    };
  },
  created:function(){
    this.navShow = !!this.nav;
  }
};
</script>

<style scoped>
.ui-top,
.ui-back,
.ui-top-right,
.ui-top > span {
  height: 2em;
  line-height: 2em;
}

.ui-top {
  text-align: center;
  width: 100%;
  background: #f7f7f7;
  border-bottom: 1px solid #dedfe0;
  box-sizing: border-box;
}

.ui-top > span {
  text-align: center;
  color: #000;
  margin: 0;
}

.ui-back,
.ui-top-right {
  position: absolute;
}

.ui-back {
  line-height: 0.7rem;
  left: 0.2rem;
  text-align: center;
  font-size: 0.34rem;
  color: #000;
}
.ui-back > img {
  width: 20%;
  float: left;
  position: relative;
  top: 0.17rem;
  left: 0.04rem;
}

.ui-top-right {
  right: 0.5em;
}

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
.ui-item:nth-of-type(1) {
  border-radius: 0.14rem 0 0 0.14rem;
  border: 1px solid #ccc;
  border-right: none;
}
.ui-item:nth-of-type(2) {
  border: 1px solid #ccc;
}
.ui-item:nth-of-type(3) {
  border-radius: 0 0.14rem 0.14rem 0;
  border: 1px solid #ccc;
  border-left: none;
}
.ui-item:first-child .ui-link {
  border-radius: 0.14rem 0 0 0.14rem;
}
.ui-item:nth-of-type(3) .ui-link {
  border-radius: 0 0.14rem 0.14rem 0;
}
.active {
  color: #000;
  background: #ebe9e9;
}
</style>
