<template>
  <div class="vue-ui-table-header">
    <table>
      <colgroup>
          <col v-for="(width, index) in columnsWidth" :key="index" :width="width">
      </colgroup>
      <thead>
        <tr>
          <td v-for="(column, index) in columns" :key="'th'+index" :class="column.className">
            {{column.title}}
          </td>
        </tr>
      </thead>
    </table>
  </div>
</template>
<script>
export default {
  name: "table-header",
  props: {
    columns: Array,
    columnsWidth: Array
  }
};
</script>
<style scoped>
  .vue-ui-table-header {
    z-index: 1000;
    position: relative;
  }
.vue-ui-table-header tr {
  border-top: 1px solid #dedfe0;
  background: #eee;
  color: #3075ce;
  height: 0.5rem;
  line-height: 0.5rem;
}
.vue-ui-table-header td {
  text-align: center;
  font-size: 0.24rem;
  font-weight: 900;
  
}
</style>
