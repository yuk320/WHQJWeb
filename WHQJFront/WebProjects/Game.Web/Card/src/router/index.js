import Vue from 'vue'
import Router from 'vue-router'
import Index from '../component/index.vue'
import Login from '../component/login.vue'
import Home from '../component/home.vue'
import Info from '../component/agentInfo.vue'
import Password from '../component/updatePass.vue'
import Below from '../component/below.vue'
import AddProxy from '../component/addAgent.vue'
import Record from '../component/record.vue'
import Send from '../component/present.vue'
import Award from '../component/award.vue'
import payInfo from '../component/payInfo.vue'
import expendInfo from '../component/expendInfo.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/Login',
      component: Login
    },
    {
      path: '/Index',
      component: Index,
      props: route => ({ token: route.query.token })
    },
    {
      path: '/Home',
      component: Home,
      props: route => ({ nav: 'Home' })
    },
    {
      path: '/Info',
      component: Info
    },
    {
      path: '/AddProxy',
      component: AddProxy,
      props: route => ({ nav: 'Below' })
    },
    {
      path: '/Below',
      component: Below,
      props: route => ({ type: '', nav: 'Below' })
    },
    {
      path: '/BelowAgent',
      component: Below,
      props: route => ({ type: 'agent', nav: 'Below' })
    },
    {
      path: '/BelowUser',
      component: Below,
      props: route => ({ type: 'user', nav: 'Below' })
    },
    {
      path: '/Award',
      component: Award,
      props: nav => ({ nav: 'Award' })
    },
    {
      path: '/payInfo',
      component: payInfo,
      props: nav => ({ nav: 'Award' })

    },
    {
      path: '/expendInfo',
      component: expendInfo,
      props: nav => ({ nav: 'Award' })
    },
    {
      path: '/',
      redirect: '/Index'
    }
  ]
})
