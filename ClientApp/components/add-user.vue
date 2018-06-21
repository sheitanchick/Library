<template>
  <div>
    <h1> Add User </h1>
    <label> Name </label>
    <input type="text" v-model="user.name"></input>
    </br>
    <label> Email </label>
    <input type="text" v-model="user.email"></input>

    <br />
    <button @click="submitChanges()"> Save </button>
  </div>
</template>

<script>
  import axios from 'axios'

  export default {
  data () {
  return {
  user: {
  id: 0,
  name: "",
  email: ""
  }
  }
  },

  props: ["id"],
  created () {
  },

  methods: {
    submitChanges: function () {
      this.$http.post("/api/Api/AddUser", "Name=" + this.user.name + "&Email=" + this.user.email).then((res) => { this.$router.push({path: "/"})});
    }
  }
  }

</script>

<style>
</style>
