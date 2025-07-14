<template>
  <q-page>
    <TodoList :entries="entries" />
  </q-page>
</template>

<script setup lang="ts">

  import { ref } from 'vue'
  import TodoList from 'components/TodoList.vue'
  import type { Entry } from 'components/TodoList.vue'

  const entries = ref<Entry[]>([
    {
      id: 'id1',
      name: '1',
      description: 'asd',
      category: 'chores'
    },
    {
      id: 'id2',
      name: '2',
      description: 'sadw',
      category: null
    },
    {
      id: 'id3',
      name: '3',
      description: 'asd',
      category: 'work'
    },
    {
      id: 'id4',
      name: '4',
      description: 's',
      category: 'exercise'
    }
  ])

</script>
