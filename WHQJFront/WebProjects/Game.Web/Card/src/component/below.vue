<template>
  <div class="ui-main ui-myagent">
    <top :nav="nav"></top>
    <div class="vue-custom-tab">
      <div class="vue-tab-nav">
        <div class="ui-search">
          <input type="text" placeholder="玩家ID/昵称"/>
          <input type="submit" value="查询"/>
        </div>
        <ul>
          <li v-for="(label, index) in labels" :key="index" >
            <a @click="changeTab(label.name)" :disabled="disabled" :class="method === label.name ? 'active' : ''">{{label.title}}</a>
          </li>
          <li>
            <router-link to="/AddProxy" class="ui-addagent">添加代理</router-link>
          </li>
        </ul>
      </div>
      <div class="ui-panel vue-tab-content">
         <ui-table :thead="theadObj" :datas="record" :type="method" :isPull="true" :upload="upload" :download="download" />
      </div>
    </div>
    <ui-dialog :show="showDetail">
      <below-detail :belowDetail="belowDetail" title="直属详情" v-on:close="closeDialog"></below-detail>
    </ui-dialog>
  </div>
</template>
<script>
import top from "./common/top";
import UiDialog from "./common/dialog";
import UiTable from "./table/vue-table";
import belowDetail from "./belowDetail";
import { getBelowList } from "../fetch/fetch";

export default {
  name: "below",
  props: {
    type: String,
    nav: String
  },
  components: { top, UiDialog, UiTable, belowDetail },
  data: function() {
    return {
      pageSize: 15,
      record: [],
      pages: 0,
      curPage: 1,
      method:this.type,
      disabled: false,
      showDetail: false,
      loading:false,
      belowDetail: {},
      linkData: {
        type: "render",
        render: (h, params) => {
          return h("a", {
            domProps: {
              innerHTML: params.title
            },
            on: {
              click: this.fetchBelowDetail
            }
          });
        }
      },
      labels: [
        {
          name: "user",
          title: "我的玩家"
        },
        {
          name: "agent",
          title: "我的代理"
        }
      ],
      theadObj: [
        {
          key: "NickName",
          title: "昵称/ID"
        },
        {
          key: "TypeDesc",
          title: "我的"
        },
        {
          key: "RegisterDate",
          title: "注册时间"
        },
        {
          key: "TotalDiamond",
          title: "充值贡献"
        },
        {
          key: "TotalGold",
          title: "税收贡献"
        }
      ]
    };
  },
  created() {
    this.fetchData(this.curPage);
  },
  methods: {
    // 每次改变记录类型都从第一页开始
    changeTab: function(value) {
      this.method = value;
      this.curPage = 1;
      this.fetchData(this.curPage);
    },
    // 根据页数获取数据
    fetchData: function(page) {
      let params = {
        token: localStorage.getItem("token"),
        pagesize: this.pageSize,
        pageindex: page
      };
      if (this.method) params.type=this.method;
      this.disabled = true;
      getBelowList(params, data => {
        
        // 没有数据时造一个空数据以避免报错（table-body）
        this.record = data.list || [];
        this.pages = data.pageCount;
        this.disabled = false;
      });
    },
    fetchBelowDetail: function(e) {
      // 因为该数据不定，所以不进行store存储
      // 获取gameid
      let gameid = parseInt(e.target.attributes["gameid"].nodeValue);

      this.showDetail = true;
      this.belowDetail = {
        GameID: gameid,
        TypeDesc: "玩家",
        NickName: "aaa",
        TotalDiamond: 0,
        TotalGold: 0
      };
      getBelowDetail({ token: localStorage.getItem('token'), gameid: gameid }, data => {
        this.underDetail = data.data.info
      })
    },
    closeDialog: function() {
      this.showDetail = false
    },
    // 上拉操作
    upload: function(loaded) {
      this.curPage++;
      this.curPage = this.curPage >= this.pages ? this.pages : this.curPage;
      this.fetchData(this.curPage);
      loaded("done");
    },
    // 下拉操作
    download: function(loaded) {
      this.curPage--;
      this.curPage = this.curPage < 1 ? 1 : this.curPage;
      this.fetchData(this.curPage);
      loaded("done");
    }
  }
};
</script>
<style scoped>
.vue-tab-nav {
  width: 96%;
  margin: 0 auto;
}
.vue-tab-nav > ul {
  position: relative;
}
.ui-myagent .vue-tab-nav li {
  display: inline-block;
  margin: 0.14rem 0rem 0.14rem 1%;
}
.ui-myagent .vue-tab-nav li:first-child {
  margin-left: 0;
}
.ui-myagent .vue-tab-nav li:nth-child(3) {
  position: absolute;
  right: 0;
}
.ui-myagent .vue-tab-nav a {
  display: inline-block;
  padding: 0.06rem 0.1rem 0.06rem;
  line-height: 0.4rem;
  font-size: 0.3rem;
  background: #fff;
  color: #000;
  border: 1px solid #ccc;
  border-radius: 0.12rem;
  text-align: center;
  width: 1.2rem;
}
.ui-myagent .vue-tab-nav li:first-child a {
  border: 1px solid #ccc;
  color: #000;
  margin-top: 0.1rem;
}
.ui-myagent .vue-tab-nav li:nth-child(2) a {
  border: 1px solid #ccc;
  color: #000;
  margin-top: 0.1rem;
}
.ui-myagent .vue-tab-nav li:nth-child(3) a {
  border: 1px solid #ccc;
  color: #000;
  margin-top: 0.1rem;
}
.ui-search {
  margin-top: 0.3rem;
  border-bottom: 1px solid #dedfe0;
  padding-bottom: 0.3rem;
  text-align: center;
}
.ui-search input[type="text"] {
  width: 4.2rem;
  height: 0.32rem;
  padding-top: 0.18rem;
  margin-top: 0.16rem;
  border-bottom: 1px solid #999ca0;
  background: #f7f7f7;
  font-size: 0.32rem;
}
.ui-search input[type="submit"] {
  width: 1rem;
  height: 0.5rem;
  line-height: 0.5rem;
  font-size: 0.32rem;
  margin-top: 0.24rem;
}

.ui-myagent .vue-tab-nav li a.active {
  color: #000;
  background: #ebe9e9;
}
button{
  background: #fff;
  border: none;
}
</style>
