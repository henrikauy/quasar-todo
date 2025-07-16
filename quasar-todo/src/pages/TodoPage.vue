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
  import { ref, computed, watch } from 'vue'
  import TodoList from 'components/TodoList.vue'
  import type { Entry } from 'components/TodoList.vue'
  import TodoListEntry from 'components/TodoListEntry.vue'
  import { useQuasar } from 'quasar'

  const $q = useQuasar()

  // Create entries ref
  const entries = ref<Entry[]>([])

  // Populate entries with whats saved in local storage
  const savedEntries = $q.localStorage.getItem<Entry[]>('todos')
  if (savedEntries) {
    entries.value = savedEntries
  }

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
    const index = entries.value.findIndex(currentEntry => currentEntry.id === entry.id)
    if (index !== -1) {
      entries.value.splice(index, 1)
    }
  }

  // Watch the state of entries - on change update the local storage to current state.
  watch(entries, (newEntries: Entry[]) => {
    $q.localStorage.set('todos', newEntries)
    console.log("entry added")
  },
    { deep: true }
  )

</script>
