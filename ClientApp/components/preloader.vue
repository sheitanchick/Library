<template>
  <div class="preloader-wr">
    <div class="preloader">
      <img src="../../Content/loading.gif" />
    </div>
  </div>  
</template>

<script>

  export default {
  }

</script>

<style>
  .preloader-wr{
    position: fixed;
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
    background-color: rgba(0, 0, 0, 0.4);
  }

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
