<template>
  <monaco-editor
    class="editor"
    v-model="code"
    language="javascript"
    theme="vs-dark"
    :diffEditor="true">
  </monaco-editor>
</template>

<script>
import MonacoEditor from 'vue-monaco'

export default {
  components: {
    MonacoEditor
  },
  data() {
    return {
      code: 'const noop = () => {}'
    }
  }
}
</script>

<style>
.editor {
  width: 100%;
  height: 500px;
}
</style>