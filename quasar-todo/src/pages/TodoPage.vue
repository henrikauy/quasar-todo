<template>
  <q-page class="column q-pa-md">
    <div class="col">

      <!-- Incomplete Tasks Section -->
      <div class="q-mb-md">
        <div class="text-subtitle1">To Do</div>
        <TodoList :categories="categories"
                  :entries="incompletedEntries"
                  @delete="handleDeleteEntry"
                  @completeToggle="handleToggleComplete"/>
      </div>

      <!-- Completed Tasks Section -->
      <div class="q-mt-md">
        <div class="text-subtitle1">Completed</div>
        <TodoList :categories="categories"
                  :entries="completedEntries"
                  @delete="handleDeleteEntry"
                  @completeToggle="handleToggleComplete"/>
      </div>
    </div>

    <!-- Entry Form -->
    <div class="q-mt-md">
      <TodoListEntry :categories="categories"
                     @add="handleAddEntry" />
    </div>
  </q-page>
</template>

<script setup lang="ts">
  // Imports
  import { ref, computed, watch, onMounted } from 'vue'
  import TodoList from 'components/TodoList.vue'
  import type { Entry, Category } from 'components/TodoList.vue'
  import type { NewEntry } from 'components/TodoListEntry.vue'
  import TodoListEntry from 'components/TodoListEntry.vue'
  import { useQuasar } from 'quasar'

  // Base API URL from .env
  const apiUrl = import.meta.env.VITE_API_URL;
  const $q = useQuasar()

  // State variables
  const entries = ref<Entry[]>([])
  const categories = ref<Category[]>([])

  // Define types for mapping API response
  type EntryApi = {
    id: number;
    name: string;
    isComplete: boolean;
    categoryId: number;
  }

  type CategoryApi = {
    categoryId: number;
    name: string;
  }

  // Fetch categories on page mount
  onMounted(async () => {
    try {
      const response = await fetch(`${apiUrl}/api/Categories`)
      const data = await response.json()

      // Map API shape to local Category type
      const mappedData: Category[] = data.map((item: CategoryApi) => ({
        id: item.categoryId,
        name: item.name
      }))

      categories.value = mappedData
    } catch (err) {
      console.error('Error fetching categories: ', err)
    }
  });

  // Fetch todos on page mount
  onMounted(async () => {
    try {
      const response = await fetch(`${apiUrl}/api/Todos`)
      const data = await response.json()

      // Map API shape to local Entry type
      const mappedData: Entry[] = data.map((item: EntryApi) => ({
        id: item.id,
        name: item.name,
        completed: item.isComplete,
        category: item.categoryId
      }))

      entries.value = mappedData

    } catch (err) {
      console.error('Error fetching todos: ', err)

      // Load from local storage if fetch fails
      const savedEntries = $q.localStorage.getItem<Entry[]>('todos')
      if (savedEntries) {
        entries.value = savedEntries
      }
    }
  });

  // todos not completed
  const incompletedEntries = computed(() =>
    entries.value.filter(entry => !entry.completed)
  )

  // todos that are completed
  const completedEntries = computed(() =>
    entries.value.filter(entry => entry.completed)
  )

  // Handle new entry from form component
  const handleAddEntry = async (newEntry: NewEntry) => {
    try {
      const response = await fetch(`${apiUrl}/api/Todos`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          name: newEntry.name,
          isComplete: newEntry.completed,
          categoryId: newEntry.category
        })
      })

      if (!response.ok) {
        console.error(`Error saving data! status: ${response.status}`);
      }

      const createdEntry: EntryApi = await response.json()

      // Map API shape to local Entry type
      const mappedData: Entry = {
        id: createdEntry.id,
        name: createdEntry.name,
        completed: createdEntry.isComplete,
        category: createdEntry.categoryId
      };

      entries.value.push(mappedData) // Add new entry to local list
    } catch (err) {
      console.error('Error adding entry:', err)
    }
  }

  // Handle deletion of an entry by ID
  const handleDeleteEntry = async (entry: Entry) => {
    try {
      const response = await fetch(`${apiUrl}/api/Todos/${entry.id}`, {
        method: 'DELETE',
      })

      if (!response.ok) {
        console.error(`Error deleting entry! status: ${response.status}`)
      }

      // Reflect change on screen
      const index = entries.value.findIndex(currentEntry => currentEntry.id === entry.id)
      if (index !== -1) {
        entries.value.splice(index, 1)
      }
    } catch (err) {
      console.error('Error deleting entry:', err)
    }

  }

  // toggle the complete bool
  const handleToggleComplete = async (entry: Entry) => {
    try {
      const mappedData: EntryApi = {
        id: entry.id,
        name: entry.name,
        isComplete: !entry.completed,
        categoryId: entry.category
      }
      const response = await fetch(`${apiUrl}/api/todos/${entry.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(mappedData),
      });

      if (!response.ok) {
        console.error(`Error updating entry! status: ${response.status}`);
      }

      // reflect change on screen
      entry.completed = !entry.completed
    } catch (error) {
      console.error('Error updating resource:', error);
    }
  }

  // update todos to localStorage whenever `entries` changes
  watch(entries, (newEntries: Entry[]) => {
    $q.localStorage.set('todos', newEntries)
    console.log("local storage updated")
  }, { deep: true })

</script>



