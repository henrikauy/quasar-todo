<template>
  <q-page class="column q-pa-md">
    <div class="col">

      <!-- Incomplete Tasks Section -->
      <div class="q-mb-md">
        <div class="text-subtitle1">To Do</div>
        <TodoList :entries="incompletedEntries"
                  @delete="handleDeleteEntry" />
      </div>

      <!-- Completed Tasks Section -->
      <div class="q-mt-md">
        <div class="text-subtitle1">Completed</div>
        <TodoList :entries="completedEntries"
                  @delete="handleDeleteEntry" />
      </div>
    </div>

    <!-- Entry Form -->
    <div class="q-mt-md">
      <TodoListEntry @add="handleAddEntry" />
    </div>
  </q-page>
</template>

<script setup lang="ts">
  // Imports
  import { ref, computed } from 'vue'
  import TodoList from 'components/TodoList.vue'
  import type { Entry } from 'components/TodoList.vue'
  import TodoListEntry from 'components/TodoListEntry.vue'

  // List of all entries
  const entries = ref<Entry[]>([
    {
      id: 'id1',
      name: 'Wash the dishes.',
      category: 'chores',
      completed: true
    },
    {
      id: 'id2',
      name: 'eat an apple today!',
      category: null,
      completed: false
    },
    {
      id: 'id3',
      name: 'Gym sesh',
      category: 'exercise',
      completed: false
    },
    {
      id: 'id4',
      name: 'finish todo app',
      category: 'work',
      completed: false
    }
  ])

  // Computed list of entries that are not completed
  const incompletedEntries = computed(() =>
    entries.value.filter(entry => !entry.completed)
  )

  // Computed list of entries that are completed
  const completedEntries = computed(() =>
    entries.value.filter(entry => entry.completed)
  )

  // Handle new entry from form component
  const handleAddEntry = (newEntry: Entry) => {
    entries.value.push(newEntry)
  }

  // Handle entry deletion
  const handleDeleteEntry = (entry: Entry) => {
    const index = entries.value.findIndex(e => e.id === entry.id)
    if (index !== -1) {
      entries.value.splice(index, 1)
    }
  }
</script>
