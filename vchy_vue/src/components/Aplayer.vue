<template>
  <div>
    <aplayer
      autoplay
      theme
      listFolded
      :music="{
    title: '朵',
    artist: 'Dorr',
    src: 'http://music.163.com/song/media/outer/url?id=447926063.mp3',
    pic: 'http://p1.music.126.net/BJgUd9aD9gpougZFASRTTw==/18548761162235571.jpg?param=130y130'
  }"
    :list="[{
    title: '朵',
    artist: 'Dorr',
    src: 'http://music.163.com/song/media/outer/url?id=447926063.mp3',
    pic: 'http://p1.music.126.net/BJgUd9aD9gpougZFASRTTw==/18548761162235571.jpg?param=130y130'
  }]"
    />
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Aplayer from "vue-aplayer";

@Component({
  components: {
    Aplayer
  }
})
export default class Music extends Vue {}
</script>

