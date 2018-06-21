<template>
  <div>
    <h1> Add Book </h1>
    <label> Name </label>
    <input type="text" v-model="book.name"></input>
    </br>
    <label> Author </label>
    <input type="text" v-model="book.author"></input>
    </br>
    <label> Available Quantity </label>
    <input type="text" v-model="book.availableQuantity"></input>


    <br />
    <button @click="submitChanges()"> Save </button>
  </div>
</template>

<script>
  import axios from 'axios'

  export default {
    data() {
      return {
        book: {
          id: 0,
          name: "",
          author: "",
          availableQuantity: ""
        }
      }
    },

  //props: ["id"],
  created () {
  },

  methods: {
    submitChanges: function () {
      this.$http.post("/api/Api/AddBook", "Name=" + this.book.name + "&Author=" + this.book.author + "&AvailableQuantity=" + this.book.availableQuantity).then((res) => { if(res.data.result == "true") this.$router.push({path : "/books"})});
    }
  }
  }

</script>

<style>
</style>
