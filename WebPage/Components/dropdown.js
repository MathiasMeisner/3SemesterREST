Vue.component('v-dropdown', {
    template: 
    /*html*/
    `<div class="v-dropdown">
      <button @click="toggle">Toggle</button>
      <div v-if="active">menu</div>
    </div>`,
    data(){
      return{
        active: false,
        lokationer: ["Roskilde", "Slagelse",]
        }
    },
    methods: {
      toggle() {
        this.active = !this.active
      }
    }
})