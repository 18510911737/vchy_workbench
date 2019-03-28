import Vue from 'vue'
import Router from 'vue-router'
import Header from "@/views/layout/Header.vue"
import Main from "@/views/layout/Main.vue"
import Footer from "@/views/layout/Footer.vue"

Vue.use(Router)

export default new Router({
  mode: "history",
  routes: [
    {
      path: '/',
      name: 'layout',
      component: () => import('./views/layout/Layout.vue'),
      children: [
        {
          path: '/',
          components: {
            Header,
            Main,
            Footer
          },
          children: [
            {
              path: '/',
              component: () => import('@/components/MavonEditor.vue')
            },
            {
              path: '/Monaco',
              component: () => import('@/components/Monaco.vue')
            },
            {
              path:'/html',
              component:()=> import('./views/utlis/HTML.vue')
            },
            {
              path:'/Calculator',
              component:()=> import('@/components/Calculator.vue')
            }
          ]
        }
      ]
    }
  ]
})
