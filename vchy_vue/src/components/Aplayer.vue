<template>
  <div>
    <aplayer
      autoplay
      :music="{
    title: 'secret base~君がくれたもの~',
    artist: 'Silent Siren',
    src: 'https://moeplayer.b0.upaiyun.com/aplayer/secretbase.mp3',
    pic: 'https://moeplayer.b0.upaiyun.com/aplayer/secretbase.jpg'
  }"
    />
  </div>
</template>
<script>
 import { Component, Vue } from 'vue-property-decorator';
 import Aplayer from 'vue-aplayer'


    
@Component({
  components: {
    Aplayer
  },
})
export default class Music extends Vue {}
</script>

