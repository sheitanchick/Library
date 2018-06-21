<template>
  <div>
    <router-link tag="button" :to="{ path: '/add-book/'}">Add Book</router-link>
    <table>
      <tr>
        <th>Name</th>
        <th>Author</th>
        <th>Quantity</th>
        <th style="border-right: none;"></th>
        <th style="border-right: none;"></th>
      </tr>
      <tr v-for="book in books">
        <td>{{book.name}}</td>
        <td>{{book.author}}</td>
        <td>{{book.availableQuantity}} </td>
        <td><button @click="DeleteBook(book.id)"> Delete Book</button></td>
        <td><router-link tag="button" :to="{ path: '/edit-book/' + book.id}">Edit Book</router-link></td>
      </tr>
    </table>
  </div>
</template>

<script>
  import axios from 'axios'

  export default {
    data() {
      return {
        books: []
      }
    },


  created () {
  this.getBooks()
  },

    methods:
      {
        getBooks: function () {
          let res = this.$http.get("api/Api/GetBooks").then((res) => { this.books = res.data });
        },

        DeleteBook: function (id) {
          this.$http.get("api/Api/deleteBook/" + id).then((res) => { if (res.data.result == "true") this.getBooks(); });
        }
      }

  }

</script>

<style>
</style>
