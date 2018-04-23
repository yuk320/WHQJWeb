<template>
  <div class="ui-main ui-reward">
    <top :nav="nav"></top>
    <div class="vue-custom-tab">
      <div class="vue-tab-nav">
        <div class="ui-search">
          <div>
            <input type="date" /> - <input type="date" />
          </div>
          <input type="text" placeholder="玩家ID/昵称"/>
          <input type="submit" value="查询"/>
        </div>
        <div class="ui-reward-title">
          <p>消耗返利详情</p>
          <a>查询业绩汇总 ：123465789</a>
        </div>
      </div>
      <div class="ui-panel vue-tab-content">
        <ui-table :thead="theadObj[curRecord]" :datas="record" :type="curRecord" :isPull="true" :upload="upload" :download="download" />
      </div>
    </div>
  </div>
</template>
<script>
import top from './common/top'
import UiTable from './table/vue-table'
import { getRecord } from '../fetch/fetch'

export default {
  name: 'reward',
  props:{
    nav:String
  },
  components: { top, UiTable },
  data: function() {
    return {
      pageSize: 15,
      curRecord: 'expend',
      record: [],
      pages: 0,
      curPage: 1,
      disabled: false,
      labels: [
        {
          name: 'expend',
          title: '消耗返利'
        }
      ],
      theadObj: {
        expend: [
          {
            key: 'GameDate',
            title: '游戏时间'
          },
          {
            key: 'GameName',
            title: '玩家昵称'
          },
          {
            key: 'GameID',
            title: '玩家ID'
          },
          {
            key: 'GamePeople',
            title: '代理/玩家'
          },
          {
            key: 'GameRoom',
            title: '游戏房间'
          },
          {
            key: 'ServiceAmount',
            title: '服务费'
          },
          {
            key: 'Commission',
            title: '提成'
          }
        ]
      }
    }
  },
  created() {
    this.fetchData(this.curPage)
  },
  methods: {
    // 每次改变记录类型都从第一页开始
    changeTab: function(value) {
      this.curRecord = value
      this.curPage = 1
      this.fetchData(this.curPage)
    },
    // 根据页数获取数据
    fetchData: function(page) {
      let params = {
        token: localStorage.getItem('token'),
        type: this.curRecord,
        pagesize: this.pageSize,
        pageindex: page
      }
      getRecord(params, data => {
        // 没有数据时造一个空数据以避免报错（table-body）
        this.record = data.record || []
        this.pages = data.pageCount
      })
    },
    // 上拉操作
    upload: function(loaded) {
      this.curPage++
      this.curPage = this.curPage > this.pages ? this.pages : this.curPage
      this.fetchData(this.curPage)
      loaded('done')
    },
    // 下拉操作
    download: function(loaded) {
      this.curPage--
      this.curPage = this.curPage < 1 ? 1 : this.curPage
      this.fetchData(this.curPage)
      loaded('done')
    }
  }
}
</script>
<style scoped>
.vue-tab-nav{
  width: 96%;
  margin: 0 auto;
}
.ui-reward .vue-tab-nav li {
  display: inline-block;
  margin: 0.14rem 0rem 0.14rem 1%;
}
.ui-search{
  margin-top: 0.2rem;
  padding-bottom: 0.2rem;
  border-bottom: 1px solid #dedfe0;
  text-align: center;
}
input[type=date]{
  font-size: 0.3rem;
  height: 0.6rem;
  line-height: 0.6rem;
  width: 2.3rem;
  background: #f7f7f7;
  font-family: 'Microsoft-YaHei';
}
input[type=date]::-webkit-inner-spin-button { 
  visibility: hidden; 
}
input[type=date]::-webkit-clear-button {
  display: none;
}
.ui-search input[type="text"]{
  width: 4.2rem;
  height: 0.32rem;
  padding-top: 0.18rem;
  margin-top: 0.18rem;
  border-bottom: 1px solid #999ca0;
  background: #f7f7f7;
  font-size: 0.32rem;
}
.ui-search input[type="submit"]{
  width: 1rem;
  height: 0.5rem;
  line-height: 0.5rem;
  font-size: 0.32rem;
  margin-top: 0.26rem;
}
.ui-search >div{
  margin: 0 auto ;
  width: 5.3rem;
  height: 0.6rem;
  font-size: 0.3rem;
  border-radius: 0.12rem;
  border: 1px solid #ccc;
  color: #000;
}
.ui-reward-title{
  position: relative;
  height: 1.3rem;
}
.ui-reward-title > p{
  margin: 0.16rem 0;
  text-align: center;
}
.ui-reward-title > a{
  display: block;
  font-size: 0.25rem;
  border: 1px solid #ccc;
  width: 3.6rem;
  padding: 0.08rem 0;
  text-align: center;
  border-radius: 0.14rem;
  position: absolute;
  right: 0;
}
</style>
