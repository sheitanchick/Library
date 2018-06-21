<template>
  <div>
    <router-link tag="button" :to="{ path: '/add-user'}">Add User</router-link>
    <table>
      <tr>
        <th>Name</th>
        <th>Email</th>
        <th style="border-right: none;"></th>
        <th style="border-right: none;"></th>
      </tr>
      <tr v-for="user in users">
        <td>{{ user.name}}</td>
        <td>{{user.email}}</td>
        <td><button @click="DeleteUser(user.id)"> Delete User</button></td>
        <td><router-link tag="button" :to="{ path: '/edit-user/' + user.id}">Edit User</router-link></td>
      </tr>
    </table>
  </div>
</template>

<script>
  import axios from 'axios'

  export default {
  data () {
  return {
  users : []
  }
  },


  created () {
  this.getUsers()
  },

  methods: {
  getUsers: function() {
  let res = this.$http.get("api/Api/GetUsers").then((res) => {this.users = res.data});
  console.log(res);
  },

  DeleteUser: function(id) {
  this.$http.get("api/Api/deleteuser/" + id).then((res) => {if(res.data.result == "true") this.getUsers();});
  }
  }

  }

</script>

<style>
</style>
