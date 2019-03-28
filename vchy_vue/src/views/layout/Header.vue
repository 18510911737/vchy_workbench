<template>
  <div>
    <el-row :gutter="20">
      <el-col :offset="4" :span="16">
        <div class="grid-content bg-purple">
          <el-menu :default-active="activeIndex" mode="horizontal" @select="handleSelect">
            <el-menu-item index="1">
              <router-link to="/" tag="span">任务</router-link>
            </el-menu-item>
            <el-submenu index="2">
              <template slot="title">我的工作台</template>
              <el-menu-item index="2-1">选项1</el-menu-item>
              <el-menu-item index="2-2">选项2</el-menu-item>
              <el-menu-item index="2-3">选项3</el-menu-item>
              <el-submenu index="2-4">
                <template slot="title">选项4</template>
                <el-menu-item index="2-4-1">选项1</el-menu-item>
                <el-menu-item index="2-4-2">选项2</el-menu-item>
                <el-menu-item index="2-4-3">选项3</el-menu-item>
              </el-submenu>
            </el-submenu>
            <el-submenu index="3">
              <template slot="title">{{ $t('工具') }}</template>
              <el-menu-item index="3-1">
                <router-link to="/html" tag="span">{{ $t('HTML') }}</router-link>
              </el-menu-item>
              <el-menu-item index="3-2">
                <router-link to="/Monaco" tag="span">{{ $t('编辑器') }}</router-link>
              </el-menu-item>
              <el-menu-item index="3-3">
                <router-link to="/Calculator" tag="span">{{ $t('计算器') }}</router-link>
              </el-menu-item>
              <el-submenu index="3-4">
                <template slot="title">选项4</template>
                <el-menu-item index="3-4-1">选项1</el-menu-item>
                <el-menu-item index="3-4-2">选项2</el-menu-item>
                <el-menu-item index="3-4-3">选项3</el-menu-item>
              </el-submenu>
            </el-submenu>
            <el-submenu index="4">
              <template slot="title">{{ $t('语言') }}</template>
              <el-menu-item index="4-1" @click="Language('cn')">简体中文</el-menu-item>
              <el-menu-item index="4-2" @click="Language('en')">English</el-menu-item>
            </el-submenu>
          </el-menu>
        </div>
      </el-col>
    </el-row>
  </div>
</template>
<script>
export default {
  data() {
    return {
      activeIndex: "1",
      activeIndex2: "1"
    };
  },
  methods: {
    handleSelect(key, keyPath) {
      console.log(key, keyPath);
    },
    Language(language) {
      this.$i18n.locale = language;
    }
  }
};
</script>
