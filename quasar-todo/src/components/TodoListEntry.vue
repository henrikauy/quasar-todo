<template>
  <q-form @submit.prevent="handleSubmit">
    <div class="col q-gutter-y-sm">
      <q-input square outlined v-model="form.name" label="Todo item" />
      <q-input square outlined v-model="form.description" label="Description" />
      <q-input square outlined v-model="form.category" label="Category" />
    </div>

    <div class="row justify-end q-mt-sm">
      <q-btn label="Submit" type="submit" color="primary" />
    </div>
  </q-form>

</template>

<script setup lang="ts">
  import { ref } from 'vue'
  import type { Entry } from './TodoList.vue'

  const emit = defineEmits<{ (e: 'add', newEntry: Entry): void; }>()

  const form = ref({
    name: '',
    description: '',
    category: ''
  });

  const handleSubmit = () => {
    if (!form.value.name || !form.value.description) return;

    const newEntry: Entry = {
      id: `id-${Date.now()}`,
      name: form.value.name,
      description: form.value.description,
      category: form.value.category as Entry['category']
    };

    emit('add', newEntry);

    form.value.name = '';
    form.value.description = '';
    form.value.category = '';

  };
</script>
