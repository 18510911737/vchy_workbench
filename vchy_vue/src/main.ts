import Vue from 'vue'
import './plugins/axios'
import App from './App.vue'
import router from './router'
import store from './store'
import './plugins/element'
import i18n from './i18n'
import mavonEditor from 'mavon-editor'


Vue.config.productionTip = false

Vue.use(mavonEditor)

new Vue({
  router,
  store,
  i18n,
  render: h => h(App)
}).$mount('#app')
