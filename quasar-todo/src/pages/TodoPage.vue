<template>
  <q-page class="column q-pa-md">
    <div class="col">
      <TodoList :entries="entries" />
    </div>

    <div>
      <TodoListEntry @add="handleAddEntry" />
    </div>
  </q-page>
</template>

<script setup lang="ts">

  import { ref } from 'vue'
  import TodoList from 'components/TodoList.vue'
  import type { Entry } from 'components/TodoList.vue'
  import TodoListEntry from 'components/TodoListEntry.vue'

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

  const handleAddEntry = (newEntry: Entry) => {
    entries.value.push(newEntry)
  };


</script>
