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
  import { ref, computed, watch, onMounted } from 'vue'
  import TodoList from 'components/TodoList.vue'
  import type { Entry } from 'components/TodoList.vue'
  import TodoListEntry from 'components/TodoListEntry.vue'
  import { useQuasar } from 'quasar'

  const apiUrl = import.meta.env.VITE_API_URL;
  const $q = useQuasar()

  // Create entries ref
  const entries = ref<Entry[]>([])

  type EntryApi = {
    id: number;
    name: string;
    isComplete: boolean;
    categoryId: number;
  }

  // Populate Entries from API on mount
  onMounted(async () => {
    try {
      const response = await fetch(`${apiUrl}/api/Todos`)
      const data = await response.json()

      const mappedData: Entry[] = data.map((item: EntryApi) => ({
        id: item.id,
        name: item.name,
        completed: item.isComplete,
        category: item.categoryId
      }))

      entries.value = mappedData
    }
    catch (err) {
      console.error('Error fetching todos: ', err)
      const savedEntries = $q.localStorage.getItem<Entry[]>('todos') // Populate entries with whats saved in local storage
      if (savedEntries) {
        entries.value = savedEntries
      }
    }
  });

  // Computed list of entries that are not completed
  const incompletedEntries = computed(() =>
    entries.value.filter(entry => !entry.completed)
  )

  // Computed list of entries that are completed
  const completedEntries = computed(() =>
    entries.value.filter(entry => entry.completed)
  )

  function getCategoryId(category: string): number {
    switch (category) {
      case 'chores': return 0;
      case 'work': return 1;
      case 'exercise': return 2;
      default: return 3; // 'misc' or fallback
    }
  }

  // Handle new entry from form component
  const handleAddEntry = async (newEntry: Entry) => {
    try {
      const response = await fetch(`${apiUrl}/api/Todos`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },

        body: JSON.stringify({
          name: newEntry.name,
          isComplete: newEntry.completed,
          categoryId: getCategoryId(newEntry.category)
        })
      })

      if (!response.ok) {
        console.error(`Error saving data! status: ${response.status}`);
      }

      entries.value.push(newEntry)
    } catch (err) {
      console.error('Error adding entry:', err)
    }
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
    console.log("local storage updated")
  },
    { deep: true }
  )

</script>
