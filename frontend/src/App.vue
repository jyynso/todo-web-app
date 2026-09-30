<script setup lang="ts">
import{ ref } from 'vue';
import Header from './components/Header.vue';
import DisplayTask from './components/DisplayTask.vue';

type Filter = 'all' | 'done' | 'not done';
const filter = ref<Filter>('all');
</script>

<template>
  <main class="mx-auto flex min-h-screen w-full max-w-4xl flex-col gap-8 p-10">
    <Header v-model="filter" />
    <DisplayTask :filter="filter" />
  </main>
</template>
