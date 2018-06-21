<template>
  <div>
    <router-link tag="button" :to="{ path: '/book-ordering'}">Order a book</router-link>
    <a href="" target="_blank">Export bad guys to excel</a>
      <table>
        <tr>
          <th>Order Id</th>
          <th>Customer</th>
          <th>Book</th>
          <th>Author</th>
          <th>Booking Date</th>
          <th>Expected Date</th>
          <th>Status</th>
          <th></th>
        </tr>
        <tr v-for="order in Orders">
          <td>{{ order.id}}</td>
          <td>{{ order.user.name }}</td>
          <td>{{ order.book.name }}</td>
          <td>{{ order.book.author}}</td>
          <td>{{order.bookingDate.substring(0, 10)}}</td>
          <td>{{order.expectedReturnDate.substring(0, 10) }}</td>
          <td v-if="order.actualReturnDate != null">Returned</td>
          <td v-else>Not returned</td>
          <td v-if="order.actualReturnDate != null"></td>
          <td v-else><button @click="bookReturned(order.id)"> Mark as returned </button></td>
        </tr>
      </table>
  </div>
</template>

<script>
  import axios from 'axios'
import { Date } from 'core-js';

  export default {
    data() {
      return {
        Orders: []
      }
    },



    created() {
      this.getOrders()
    },

    methods:
      {
        getOrders: function () {
          this.$http.get("api/Api/GetOrders").then((res) => { this.Orders = res.data });
        },

        bookReturned: function (id) {
          this.$http.get("api/Api/BookReturned/" + id).then((res) => { this.getOrders() });
        },
        /*getBooks: function () {
          let res = this.$http.get("api/Api/GetBooks").then((res) => { this.books = res.data });
        },

        DeleteBook: function (id) {
          this.$http.get("api/Api/deleteBook/" + id).then((res) => { if (res.data.result == "true") this.getBooks(); });
        }
      },*/
        

      },

    computed: {
      isReturned() { return order.actualReturnDate != null }
    }
  }

</script>

<style>
  th {
    border-right: solid 1px;
    border-bottom: solid 1px;
  }

  td {
    border-right: solid 1px;
  }
</style>
