<template>
    <q-list bordered separator>
      <q-item v-for="entry in entries" :key="entry.id">
        <q-item-section>
          <q-item-label> {{ entry.name }} </q-item-label>
          <q-item-label caption lines="2"> {{ entry.description }} </q-item-label>
        </q-item-section>
        <q-item-section side top>
          <q-item-label caption> {{ entry.category }} </q-item-label>
        </q-item-section>
      </q-item>
    </q-list>
</template>

<script setup lang="ts">
  export type Entry = {
    id: string;
    name: string;
    description: string;
    category: 'chores' | 'work' | 'exercise' | null;
  };

  defineProps<{ entries: Entry[] }>()
</script>
