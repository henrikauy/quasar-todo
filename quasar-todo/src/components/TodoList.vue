<template>
  <q-card square bordered class="q-pa-md">
    <q-list bordered separator>
      <!-- Loop through all entries -->
      <q-item v-for="entry in entries" :key="entry.id" class="relative-position">

        <!-- Complete / Incomplete toggle button -->
        <q-item-section side>
          <q-btn :color="entry.completed ? 'positive' : 'negative'"
                 :icon="entry.completed ? 'check_circle' : 'radio_button_unchecked'"
                 @click="toggleComplete(entry)"
                 flat
                 round
                 dense />
        </q-item-section>

        <!-- Entry name and category -->
        <q-item-section>
          <q-item-label>{{ entry.name }}</q-item-label>
          <q-item-label caption lines="2">{{ getCategoryName(entry.category) }}</q-item-label>
        </q-item-section>

        <!-- Delete button -->
        <q-btn class="absolute-top-right q-ma-xs"
               color="red"
               icon="close"
               size="xs"
               flat
               round
               dense
               @click="deleteEntry(entry)" />
      </q-item>
    </q-list>
  </q-card>
</template>

<script setup lang="ts">
  // Export Entry type
  export type Entry = {
    id: string;
    name: string;
    category: number;
    completed: boolean;
  }

  // Export Category Type
  export type Category = {
    id: number;
    name: string;
  }

  // Props to receive to parent
  const { entries, categories } = defineProps<{
    entries: Entry[],
    categories: Category[]
  }>()

  // Define delete emit event
  const emit = defineEmits<{
    (e: 'delete', entry: Entry): void
  }>()

  // Emit delete event to parent
  const deleteEntry = (entry: Entry) => {
    emit('delete', entry)
  }

  // Toggle completed status
  const toggleComplete = (entry: Entry) => {
    entry.completed = !entry.completed
  }

  // Get category name from id
  function getCategoryName(categoryId: number): string {
    const category = categories.find(c => c.id === categoryId)
    return category?.name ?? 'Misc'
  }


</script>
