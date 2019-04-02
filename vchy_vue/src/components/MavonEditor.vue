<template>
  <div>
    <span>{{ $t(title)}}</span>
    <mavon-editor :ishljs="true" @save="save"/>
    <el-dialog :title="$t('保存')" :visible.sync="dialogVisible" width="30%" >
      <span>这是一段信息</span>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="saveTitle">{{ $t("确 认") }}</el-button>
      </span>
    </el-dialog>
  </div>
</template>
<script lang="ts">
import "mavon-editor/dist/css/index.css";
export default {
  data() {
    return {
      title:'无标题',
      dialogVisible: false
    };
  },
  methods: {
    save(text, html) {
        (this as any).dialogVisible=true;
    },
    saveTitle(){
        (this as any).dialogVisible=false;
    }
  }
};
</script>

