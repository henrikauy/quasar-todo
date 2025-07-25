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
        <q-select square
                  dense
                  outlined
                  v-model.number="form.category"
                  :options="categories"
                  option-label="name"
                  option-value="id"
                  emit-value
                  map-options />
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
  import type { Entry, Category } from './TodoList.vue'

  // Define emit new entry event
  const emit = defineEmits<{ (e: 'add', newEntry: Entry): void }>()

  // Form state for new entry
  const form = ref<{ name: string; category: number | null }>({
    name: '',
    category: null,
  })

  // Props to receive from parent
  defineProps<{ categories: Category[] }>()

  // Handle form submission
  const handleSubmit = () => {
    // Return if form is empty
    if (!form.value.name) return

    // temporary. need to add dropdown box.~~~~~~~~~~~~~~~~~~~
    if (!form.value.category) return

    // Create new Entry object
    const newEntry: Entry = {
      id: `id-${Date.now()}`,
      name: form.value.name,
      category: form.value.category,
      completed: false
    }

    // Emit to parent
    emit('add', newEntry)

    // Reset form
    form.value.name = ''
    form.value.category = null
  }
</script>
