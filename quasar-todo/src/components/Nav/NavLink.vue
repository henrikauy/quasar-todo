<template>
  <q-item clickable
          class="text-white"
          :to="link"
          tag="router-link"
          exact
          active-class="bg-secondary">
    <q-item-section v-if="icon"
                    avatar>
      <q-icon :name="icon" />
    </q-item-section>

    <q-item-section>
      <q-item-label>{{ title }}</q-item-label>
    </q-item-section>
  </q-item>
</template>

<script setup lang="ts">
  export interface NavLinkProps {
    title: string;
    link?: string;
    icon?: string;
  };

  withDefaults(defineProps<NavLinkProps>(), {
    link: '#',
    icon: '',
  });
</script>
