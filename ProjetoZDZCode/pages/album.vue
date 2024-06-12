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
    :items="albuns"
    :sort-by="[{ key: 'title', order: 'asc' }]"
  >
    <template v-slot:top>
      <v-toolbar
        flat
      >
      <v-toolbar-title>albuns</v-toolbar-title>
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
              Adicionar Album
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
                      v-model="editedItem.title"
                      label="Album name"
                    >
                    </v-text-field>
                    
                  </v-col>
                  <v-col
                    cols="12"
                    md="4"
                    sm="6"
                  >
                    <v-text-field
                      v-model="editedItem.id"
                      label="AlbumID"
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
    <template v-slot:item.title ="{ item }">
                    <NuxtLink to="/musica">{{item.title}}</NuxtLink>
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
      <v-btn
        color="primary"
        @click="initialize"
      >
        Reset
      </v-btn>
    </template>
  </v-data-table>
  <NuxtPage  />
</template>

<script>
  export default {
    data: () => ({
      search: '',
      dialog: false,
      dialogDelete: false,
      headers: [
        {
          title: 'Álbuns',
          align: 'start',
          sortable: false,
          key: 'title',
        },
        { title: 'AlbumID', key: 'id' },
        { title: 'albuns', key: 'name' },
        { title: 'Imagem', key: 'image' },
        { title: 'Data de Lançamento', key: 'date' },
        { title: 'Quantidade de Músicas', key: 'qtde' },
        { title: 'Tempo Total', key: 'tempo' },
        { title: 'Actions', key: 'actions', sortable: false },
        
      ],
      albuns: [],
      editedIndex: -1,
      editedItem: {
        title: '',
        id: 0,
        name: '',
        image: 0,
        qtde: 0,
        tempo: 0,
      },
      defaultItem: {
        title: '',
        id: 0,
        name: '',
        image: 0,
        qtde: 0,
        tempo: 0,
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
        this.albuns = [
          {
            title: 'blonde',
            id: 0,
            name: 'Frank Ocean',
            image: 1,
            qtde: 14,
            tempo: 60,
          },
          {
            title: 'IGOR',
            id: 1,
            name: 'Tyler, The Creator',
            image: 1,
            qtde: 10,
            tempo: 80,
          },
        ]
      },

      editItem (item) {
        this.editedIndex = this.albuns.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialog = true
      },

      deleteItem (item) {
        this.editedIndex = this.albuns.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialogDelete = true
      },

      deleteItemConfirm () {
        this.albuns.splice(this.editedIndex, 1)
        this.closeDelete()
      },

      close () {
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

      save () {
        if (this.editedIndex > -1) {
          Object.assign(this.albuns[this.editedIndex], this.editedItem)
        } else {
          this.albuns.push(this.editedItem)
        }
        this.close()
      },
    },
  }
</script>