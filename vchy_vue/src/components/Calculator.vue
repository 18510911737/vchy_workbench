<template>
  <div>
    <el-row :gutter="20">
      <el-col :span="24">
        <div class="grid-content bg-purple">
          <el-input placeholder="0" value="0" :disabled="true" v-model="exception"></el-input>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="9">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">(</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">)</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">%</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple"></div>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="5">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="4">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">7</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">8</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">9</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">/</el-button>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="5">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="4">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">4</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">5</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">6</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">*</el-button>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="5">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="4">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">1</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">2</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">3</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">-</el-button>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="5">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="4">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple"></div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">0</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">.</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="calculator">=</el-button>
        </div>
      </el-col>
      <el-col :span="3">
        <div class="grid-content bg-purple">
          <el-button @click="click">+</el-button>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import axios from "axios";
export default Vue.extend({
  data() {
    return {
      exception: ""
    };
  },
  methods: {
    click: function(e) {
      this.exception += e.target.innerText;
    },
    calculator: function() {
      axios.post("api/tools/calculator",{"exception":this.exception}).then(res=>{
        this.exception=res.data;
      })
    }
  }
});
</script>


<style>
.el-button {
  width: 100%;
}
.el-row {
  margin-bottom: 20px;
}
.el-col {
  border-radius: 4px;
}
.bg-purple-dark {
  background: #99a9bf;
}
.bg-purple {
  background: #d3dce6;
}
.bg-purple-light {
  background: #e5e9f2;
}
.grid-content {
  border-radius: 4px;
  min-height: 36px;
}
.row-bg {
  padding: 10px 0;
  background-color: #f9fafc;
}
</style>