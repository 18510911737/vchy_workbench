import Vue from 'vue'
import './plugins/axios'
import './plugins/element'
import App from './App.vue'
import router from './router'
import store from './store'
import i18n from './i18n'
import mavonEditor from 'mavon-editor'
import axios from 'axios'

Vue.config.productionTip = false

Vue.use(mavonEditor)

new Vue({
  router,
  store,
  i18n,
  render: h => h(App)
}).$mount('#app')
axios.get("./config.json").then(res=>{
  axios.defaults.baseURL = res.data.baseURL;
})