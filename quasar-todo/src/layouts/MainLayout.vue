<template>
  <q-layout view="hHh lpR lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn flat
               dense
               round
               icon="menu"
               aria-label="Menu"
               @click="toggleLeftDrawer" />

        <q-toolbar-title>
          <div class="flex flex-center">
            <q-icon name="pending_actions" />
            Quasar App
          </div>
        </q-toolbar-title>

      </q-toolbar>
    </q-header>

    <q-drawer v-model="leftDrawerOpen"
              class="bg-primary"
              show-if-above
              bordered>
      <q-list>
        <q-item-label header class="text-white">
          Navigation
        </q-item-label>

        <NavLink v-for="link in linksList"
                       :key="link.title"
                       v-bind="link" />
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script setup lang="ts">
  import { ref } from 'vue';
  import NavLink, { type NavLinkProps } from 'components/Nav/NavLink.vue';

  const linksList: NavLinkProps[] = [
    {
      title: 'ToDo List',
      icon: 'school',
      link: '/'
    },
    {
      title: 'Settings',
      icon: 'code',
      link: '/settings'
    }
  ];

  const leftDrawerOpen = ref(false);

  function toggleLeftDrawer() {
    leftDrawerOpen.value = !leftDrawerOpen.value;
  }
</script>
