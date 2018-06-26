<template>
  <div>
    <router-link tag="button" :to="{ path: '/book-ordering'}">Order a book</router-link>
    <br />
    <button @click="filtered = !filtered" v-if="filtered==true">Show all</button>
    <button @click="filtered = !filtered" v-if="filtered==false">Show only bad guys (overdue and hasn't returned)</button>
    <button @click="sort()">The worst guys at the top(kinda sorting)</button>
    <br />
    <a href="/api/api/GetExcel" target="_blank">Export bad guys to excel</a>
    <booking @rerender="getOrders()"></booking>
    <preloader v-show="loading == true"></preloader>
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
      <tr v-for="order in filteredList" class="order-line">
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
  import { Date, Array } from 'core-js';
  import booking from './book-ordering.vue'
  import { app } from '../app';
  import preloader from './preloader.vue'
import Vue from 'vue';

  export default {
    data() {
      return {
        Orders: [],
        tempArr: [],
        filtered: false,
        loading: false,
        sortedAsc: false
      }
    },

    name: "ord",



    created() {
      this.getOrders()
    },

    mounted() {
      //this.$on('rerender', this.getOrders());
    },

    methods:
      {
        getOrders: function () {
          this.loading = true;
          return this.$http.get("api/Api/GetOrders")
            .then((res) => {
              this.Orders = this.tempArr = res.data;
              this.loading = false;
            });
        },

        bookReturned: function (id) {
          this.loading = true;
          this.$http.get("api/Api/BookReturned/" + id)
            .then((res) => {
              if (res.data.result == "true") {
                this.Orders.forEach((elem) => {
                  if (elem.id == id) elem.actualReturnDate = Date.now();
                })
                this.loading = false;
              }
              else {
                alert(res.data.errors);
                this.loading = false;
              }
            });
        },

        sort: function (array) {
          this.tempArr = this.Orders;
          if (this.sortedAsc == false) {
            this.sortedAsc = true;
            this.tempArr.sort((a, b) => { return a.daysAfterDue > b.daysAfterDue ? 1 : -1 });
          }
          else {
            this.sortedAsc = false;
            this.tempArr.sort((a, b) => { return a.daysAfterDue < b.daysAfterDue ? 1 : -1 });
          }
        }
      },

    computed: {

      filteredList: {
        get: function () {
          return this.tempArr.filter((order) => {
            if (this.filtered == false) {
              return true;
            }
            else return (order.daysAfterDue > 0 & order.actualReturnDate == null);
          })
        },

        set: function (newArr) {
          Vue.set(this.tempArr, null, newArr);
        }
      }
    },

    components: {
      'booking': booking,
      "preloader": preloader,
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

  .order-line:hover {
    background-color: lightgrey;
  }
</style>
