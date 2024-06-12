
<template>
  <v-layout>
    <v-app-bar :elevation="2">
      
      <template v-slot:prepend>
        
        <v-app-bar-nav-icon icon="mdi-home" href="/" > </v-app-bar-nav-icon>
        
      </template>
    
      <v-app-bar-title class="text-center">Banco de Dados</v-app-bar-title>
    </v-app-bar>
  </v-layout>
  
  <br>
  <br>
  <br>
  <br>
  <br>

  <v-data-table v-model:search="search"
    :headers="headers"
    :items="artistas"
    :sort-by="[{ key: 'id', order: 'asc' }]"
  >
    <template v-slot:top>
      <v-toolbar
        flat
      >
      <v-toolbar-title>Artistas</v-toolbar-title>
      <v-spacer></v-spacer> 
        <v-text-field
        v-model="search"
        density="compact"
        label="Search"
        prepend-inner-icon="mdi-magnify"
        variant="solo-filled"
        flat
        hide-details
        single-line 
      ></v-text-field>
        <v-dialog
          v-model="dialog"
          max-width="500px"
        >
          <template v-slot:activator="{ props }">
            <v-btn
              class="mb-2"
              color="primary"
              dark
              v-bind="props"
            >
              Adicionar Artista
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                
                <v-row>
                  
                  <v-col
                    cols="12"
                    md="4"
                    sm="6"
                  >
                  <v-text-field
                      v-model="editedItem.name"
                      label="Artist name"
                      required
                    >
                    </v-text-field>
                    
                  </v-col>
                  
                  <!-- Html Comments<v-col
                    cols="12"
                    md="4"
                    sm="6"
                  >
                    <v-file-input
                      @change="getFileObject($event)"
                      :rules="rules"
                      accept="image/png, image/jpeg, image/bmp"
                      label="Image"
                      placeholder="Pick an avatar"
                      prepend-icon="mdi-camera"
                    ></v-file-input>
                  </v-col>/-->
                  <v-col
                    cols="12"
                    md="4"
                    sm="6"
                  >
                    <v-text-field
                      v-model="editedItem.email"
                      label="E-Mail"
                      rules:="emailRules"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    md="4"
                    sm="6"
                  >
                    <v-text-field
                      v-model="editedItem.password"
                      label="Password"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    md="4"
                    sm="6"
                  >
                    <v-text-field
                      v-model="editedItem.image"
                      label="Image"
                      required
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="blue-darken-1"
                variant="text"
                @click="close"
              >
                Cancel
              </v-btn>
              <v-btn
                color="blue-darken-1"
                variant="text"
                @click="save"
              >
                Save
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="closeDelete">Cancel</v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="deleteItemConfirm">OK</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.name ="{ item }">
      <NuxtLink to="/album">{{item.name}}</NuxtLink>
    </template>
    <template v-slot:item.image = "{ item }">
      <img :src="item.image" height="64">
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon
        class="me-2"
        size="small"
        @click="editItem(item)"
      >
        mdi-pencil
      </v-icon>
      <v-icon
        size="small"
        @click="deleteItem(item)"
      >
        mdi-delete
      </v-icon>
    </template>
    <template v-slot:no-data>
      <p>Adiciona Um Item!</p>
    </template>
  </v-data-table>
  <NuxtPage />
</template>
<script>
  export default {
    data: () => ({
      rules: [
        value => {
          return !value || !value.length || value[0].size < 2000000 || 'Avatar size should be less than 2 MB!'
        },
      ],
      emailRules: [ 
        v => !v ||/^[^\s@]+@[^\s@]+$/.test(v)  || 'E-mail must be valid'
        ],
      search: '',
      dialog: false,
      dialogDelete: false,
      headers: [
        { title: 'UserID', key: 'id' },
        {
          title: 'Artistas',
          align: 'start',
          sortable: false,
          key: 'name',
        },
        { title: 'E-mail', key: 'email' },
        { title: 'SenhaHash', key: 'password' },
        { title: 'Imagem', key: 'image' },
        { title: 'Actions', key: 'actions', sortable: false },
        
      ],
      artistas: [],
      editedIndex: -1,
      editedItem: {
        name: 'nome',
        email: "conta@gmail.com",
        password: '0',
        image: '/joia.jpg',
      },
      defaultItem: {
        name: 'a',
        email: "a@gmail.com",
        password: '123',
        image: '/joia.jpg',
      },
    }),
    
    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
      },
    },
    
    watch: {
      dialog (val) {
        val || this.close()
      },
      dialogDelete (val) {
        val || this.closeDelete()
      },
    },

    created () {
      this.initialize()
    },

    methods: {
      initialize () {
        this.artistas = []
      },
      /*async getFileObject(files){
        this.fileObj = await files
        console.log(this.fileObj)
      },*/
      async editItem (item) {
        console.log(this.artistas[this.artistas.indexOf(item)].id)
        this.editedIndex = this.artistas[this.artistas.indexOf(item)].id-1
        this.editedItem = Object.assign({}, item)
        this.dialog = true
      },

      deleteItem (item) {
        this.editedIndex = this.artistas.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialogDelete = true
      },

      async deleteItemConfirm () {
        console.log('que')
        var url = 'https://localhost:7210/api/Artistas/'+(this.editedItem.id)
        try{
        var response = await $fetch(url,{
          method: "DELETE",
        })
        }
      catch{}
      try{
        var responseGet = await $fetch('https://localhost:7210/api/Artistas',{
          method: "GET",
        })
        this.artistas = responseGet
        console.log(responseGet.data)
        console.log("del2")
      }
      catch{}
        this.closeDelete()
      },

      close () {
        this.editedIndex = -1
        this.dialog = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },

      closeDelete () {
        this.dialogDelete = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },

      async save () {
        
        if (this.editedIndex > -1) {
          this.editedItem.id = this.editedIndex+1
          Object.assign(this.artistas[this.editedIndex], this.editedItem)
        } else if(this.artistas.length != 0) {
          this.editedItem.id = this.artistas.length+1
          this.artistas.push(this.editedItem)
        }else{
          this.editedItem.id = 1
          this.artistas.push(this.editedItem)
        }
        console.log(this.artistas)
        try{
        var response = await $fetch('https://localhost:7210/api/Artistas',{
          method: "POST",
          headers: {
          'Content-Type': 'application/json',
          },
          body: this.editedItem,
        })
        console.log("yo")
        }
      catch{}
      try{
        var responseGet = await $fetch('https://localhost:7210/api/Artistas',{
          method: "GET",
        })
        this.artistas = responseGet
        console.log(this.artistas)
        console.log(responseGet.data)
        console.log("yo2")
      }
      catch{}
      this.close()
      },
    },
  }
</script>