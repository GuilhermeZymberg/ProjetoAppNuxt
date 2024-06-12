import { createMemoryHistory, createRouter } from 'vue-router'

import app from './app.vue'
import index from './index.vue'
import album from './album.vue'

const routes = [
  { path: '/', component: index},
  { path: '/app', component: index},
  { path: '/album', component: album },
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
})

router.addRoute({ path: '/app', component: index })
router.addRoute({ path: '/album', component: album })

router.replace(router.currentRoute.value.fullPath)
