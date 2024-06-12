// Nuxt config file
import { defineNuxtConfig } from 'nuxt/config'

export default defineNuxtConfig({
  pages: true,
  modules: [
    'vuetify-nuxt-module',
  ],
  devtools: { enabled: true },
  vuetify: {
    moduleOptions: {
      /* module specific options */
    },
    vuetifyOptions: {
      theme: {
        defaultTheme: 'dark'
      },
    }
  },
})

