<script setup lang="ts">
import{ ref } from 'vue';
import AppHeader from './components/AppHeader.vue';
import DisplayTask from './components/DisplayTask.vue';

type Filter = 'all' | 'done' | 'not done';
const filter = ref<Filter>('all');
const searchId = ref<number | null>(null);
const list = ref<InstanceType<typeof DisplayTask> | null>(null);
</script>

<template>
  <main class="mx-auto flex min-h-screen w-full max-w-4xl flex-col gap-8 p-10">
    <AppHeader v-model="filter" @search="searchId = $event"/>
    <DisplayTask ref="list" :filter="filter" :search-id="searchId" />
  </main>
</template>
