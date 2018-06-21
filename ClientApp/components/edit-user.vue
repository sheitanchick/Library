<template>
  <div>
    <h1> Edit User </h1>
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
  this.getUser()
  },

  methods: {
  show: function() {
  console.log(this.user.Name + ' ||| ' + this.user.Email + ' || ' + this.$route.params.id );
  },
  getUser: function(id) {
  let res = this.$http.get("/api/Api/GetUser/" + this.$route.params.id).then((res) => {this.user = res.data});
  console.log(res);
  },
    submitChanges: function () {
      this.$http.post("/api/Api/EditUser", "ID=" + this.user.id + "&Name=" + this.user.name + "&Email=" + this.user.email);
  }
  }

  }

</script>

<style>
</style>
