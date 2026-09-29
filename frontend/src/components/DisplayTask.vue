<script setup lang="ts">
import { ref, onMounted } from 'vue';

interface Task {
  id: number;
  title: string;
  description: string;
  isCompleted: boolean;
}

const API = 'https://localhost:7290/api/TodoItems';
const task = ref<Task[]>([]);

async function loadTasks() {
  const response = await fetch(API, {
    headers: { 'X-API-KEY': 'apikey1234' },
  });
  if (!response.ok) {
    throw new Error('Failed to load tasks');
    return;
  }
  const data = await response.json();
  task.value = data;
}

onMounted(loadTasks);
</script>

<template>
  <div class="flex w-full flex-col gap-2">
    <div class="grid w-full grid-cols-[repeat(auto-fit,minmax(12rem,16rem))] justify-center gap-4">
      <span v-for="t in task" :key="t.id" class="flex flex-col gap-2 p-8 drop-shadow-sm" :class="t.isCompleted ? 'bg-green-200/40' : 'bg-amber-200/40'">
        <p class="text-black/70">{{ t.title }}</p>
        <span class="w-full border-t border-black/40" />
        <p class="text-sm text-black/60">{{ t.description }}</p>
        <span class="w-full border-t border-black/40" />
        <p class="text-sm text-black/60">Status: {{ t.isCompleted ? 'done' : 'not done' }}</p>
        <span class="w-full border-t border-black/40" />
        <div class="flex flex-row text-sm text-black/70 gap-2">
          <button class="hover:underline">Edit</button>
          <button class="hover:underline">Delete</button>
          <button class="hover:underline">Mark as done</button>
        </div>
      </span>
    </div>
  </div>
</template>
