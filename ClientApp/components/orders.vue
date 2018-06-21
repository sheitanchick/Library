<template>
  <div>
    <router-link tag="button" :to="{ path: '/book-ordering'}">Order a book</router-link>
    <br />
    <button @click="filtered = !filtered" v-if="filtered==true">Show all</button>
    <button @click="filtered = !filtered" v-if="filtered==false">Show only bad guys (overdue and hasn't returned)</button>
    <br />
    <a href="/api/api/GetExcel" target="_blank">Export bad guys to excel</a>
    <table>
      <tr>
        <th>Order Id</th>
        <th>Customer</th>
        <th>Book</th>
        <th>Author</th>
        <th>Booking Date</th>
        <th>Expected Date</th>
        <th>Status</th>
        <th>Days after due date</th>
        <th class="last"></th>
      </tr>
      <tr v-for="order in filteredList">
        <td>{{ order.id}}</td>
        <td>{{ order.user.name }}</td>
        <td>{{ order.book.name }}</td>
        <td>{{ order.book.author}}</td>
        <td>{{order.bookingDate.substring(0, 10)}}</td>
        <td>{{order.expectedReturnDate.substring(0, 10) }}</td>
        <td v-if="order.actualReturnDate != null">Returned</td>
        <td v-else>Not returned</td>
        <td v-if="order.daysAfterDue > 0" style="background-color:red;text-align:center;">{{order.daysAfterDue}} day(s)</td>
        <td v-else style="text-align:center;">—</td>
        <td v-if="order.actualReturnDate != null" class="last"></td>
        <td v-else class="last"><button @click="bookReturned(order.id)"> Mark as returned </button></td>
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
        Orders: [],
        filtered: false
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

      },

    computed: {
      filteredList: function() {
        return this.Orders.filter((order) => {
          if (this.filtered == false) return true;
          else return (order.daysAfterDue > 0 & order.actualReturnDate == null);
        })
      }
    }
  }

</script>

<style>
  th {
    border-right: solid 1px;
    border-bottom: solid 1px;
    padding-left: 5px;
    padding-right: 5px;
  }

  td {
    border-right: solid 1px;
    padding-left: 5px;
    padding-right: 5px;
  }

  .last {
    border-right: none;
  }
</style>
