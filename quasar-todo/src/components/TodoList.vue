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
          <q-item-label caption lines="2">{{ entry.category }}</q-item-label>
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
    category: 'chores' | 'work' | 'exercise' | 'misc';
    completed: boolean;
  }

  // Props to pass to parent: list of entries to render
  defineProps<{ entries: Entry[] }>()

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


</script>
