<template>
  <div id="layout">
    <el-container>
      <el-header>
        <router-view name="Header"/>
      </el-header>
      <el-main>
        <router-view name="Main"/>
      </el-main>
      <el-footer>
        <router-view name="Footer"/>
      </el-footer>
    </el-container>
  </div>
</template>

<style>
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
  