<template>
  <div class="preloader">
    <img src="../../Content/loading.gif" /> 
  </div>
</template>

<script>

  export default {
    data() {
      return {
        users: []
      }
    },
  }

</script>

<style>
  .preloader {
    position: absolute;
    left: 50%;
    top: 40%;
    margin-left: -30px;
    width: 60px;
    
  }

  .preloader img {
    width: 100%;
  }
</style>
