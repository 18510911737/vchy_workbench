<template>
  <div>
    <el-row>
      <el-col :offset="4" :span="16">
        <Aplayer/>
      </el-col>
    </el-row>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Aplayer from "@/components/Aplayer.vue";

@Component({
  components: {
    Aplayer
  }
})
export default class Footer extends Vue {}
</script>

