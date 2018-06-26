<template>
  <div>
    <preloader v-show="loading == true"></preloader>

    <h1>New order</h1>
    <span> Choose book: </span>
    <select v-model="BookID">
      <option v-for="book in Books" v-bind:value="book.id">
        "{{ book.name }}" by {{ book.author}}
      </option>
    </select>

    <br />
    <span> Choose user: </span>
    <select v-model="UserID">
      <option v-for="user in Users" v-bind:value="user.id">
        {{ user.name }}
      </option>
    </select>
    <br />
    <span> Choose date: </span>
    <input type="date" v-model="ExpectedReturnDate" />

    <button @click="order()"> Order </button>
  </div>
</template>

<script>
  import axios from 'axios'
  import { Date } from 'core-js';
  import preloader from './preloader.vue'

  export default {
    data() {
      return {
        Books: [],
        Users: [],
        BookID: 0,
        UserID: 0,
        BookingDate: Date.now(),
        ExpectedReturnDate: null,
        ActualReturnDate: null,
        loading: false
      }
    },

    components: {
      "preloader": preloader
    },


  created () {
    this.getBooks();
    this.getUsers();
  },

    methods:
      {
        getBooks: function () {
          this.$http.get("api/Api/GetBooks").then((res) => { this.Books = res.data });
        },

        getUsers: function () {
          this.$http.get("api/Api/GetUsers").then((res) => { this.Users = res.data });
        },

        order: function () {
          this.loading = true;
          this.$http.post("api/Api/AddOrder", "BookID=" + this.BookID + "&UserID=" + this.UserID + "&ExpectedReturnDate=" + this.ExpectedReturnDate)
            .then((res) => {
              if (res.data.result == "true" & this.$route.path == "/orders") {
                //this.$parent.$options.methods.getOrders();
                this.$emit('rerender');
                this.loading = false;
                console.log(this.$route.path);
              }
              else if (res.data.result == "true" & this.$route.path != "/orders") {
                this.$router.push({ path: "/books" });
                this.loading = false;
                console.log(this.$route.path);
              }
              else {
                this.loading = false;
                alert(res.data.errors);
              }
            });
        }
      }

  }

</script>

<style>
</style>
