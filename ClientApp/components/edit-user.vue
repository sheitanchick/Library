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
    data() {
      return {
        user: {
          id: 0,
          name: "",
          email: ""
        }
      }
    },

    props: ['id'],

    created() {
      this.getUser()
    },

    methods: {
      getUser: function (id) {
        let res = this.$http.get("/api/Api/GetUser/" + this.id)
          .then((res) => {
            if (res.data.result == "false") alert(res.data.errors);
            else this.user = res.data;
          });
      },

      submitChanges: function () {
        this.$http.post("/api/Api/EditUser", "ID=" + this.user.id + "&Name=" + this.user.name + "&Email=" + this.user.email)
          .then((res) => {
            if (res.data.result == "true") alert("Successfully updated");
            else alert(res.data.errors);
          });
      }
    }
  }

</script>

<style>
</style>
