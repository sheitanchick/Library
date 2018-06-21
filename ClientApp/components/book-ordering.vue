<template>
  <div>
    <span> Choose book: </span>
    <select v-model="BookID">
      <option v-for="book in Books" v-bind:value="book.id">
        {{ book.name }} {{ book.author}}
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

  export default {
    data() {
      return {
        Books: [],
        Users: [],
        BookID: 0,
        UserID: 0,
        BookingDate: Date.now(),
        ExpectedReturnDate: null,
        ActualReturnDate: null
      }
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
          this.$http.post("api/Api/AddOrder", "BookID=" + this.BookID + "&UserID=" + this.UserID + "&ExpectedReturnDate=" + this.ExpectedReturnDate)
            .then((res) => {
              if (res.data.result == "true") this.$router.push({ path: "/books" });
              else alert(res.data.errors[0]);
            });
        }
      }

  }

</script>

<style>
</style>
