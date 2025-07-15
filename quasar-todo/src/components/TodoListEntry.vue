<template>
  <q-card square bordered class="q-pa-md" style="max-width: 800px; margin: 0 auto;">
    <!-- Form for creating a new todo entry -->
    <q-form @submit.prevent="handleSubmit">
      <div class="col q-gutter-y-sm">
        <!-- Input task name -->
        <q-input square
                 dense
                 outlined
                 v-model="form.name"
                 placeholder="Todo Item" />

        <!-- Input category -->
        <q-input square
                 dense
                 outlined
                 v-model="form.category"
                 placeholder="Category" />
      </div>

      <!-- Submit Button -->
      <div class="row justify-end q-mt-sm">
        <q-btn label="Submit" type="submit" color="primary" />
      </div>
    </q-form>
  </q-card>
</template>

<script setup lang="ts">
  import { ref } from 'vue'
  import type { Entry } from './TodoList.vue'

  // Define emit new entry event
  const emit = defineEmits<{ (e: 'add', newEntry: Entry): void }>()

  // Form state for new entry
  const form = ref({
    name: '',
    category: '',
  })

  // Handle form submission
  const handleSubmit = () => {
    // Return if form is empty
    if (!form.value.name) return

    // Create new Entry object
    const newEntry: Entry = {
      id: `id-${Date.now()}`,
      name: form.value.name,
      category: form.value.category as Entry['category'],
      completed: false
    }

    // Emit to parent
    emit('add', newEntry)

    // Reset form
    form.value.name = ''
    form.value.category = ''
  }
</script>
