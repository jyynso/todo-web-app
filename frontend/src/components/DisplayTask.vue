<script setup lang="ts">
import { ref } from 'vue';

const task = ref([
  { id: 1, title: 'Task 1', description: 'Description for task 1', isDone: false },
  { id: 2, title: 'Task 2', description: 'Description for task 2 ', isDone: false },
  { id: 3, title: 'Task 3', description: 'Description for task 3', isDone: true },
])
</script>

<template>
  <div class="flex w-full flex-col gap-2">
    <div class="grid grid-cols-3 gap-4">
      <span v-for="t in task" :key="t.id" class="flex flex-col gap-2 p-8" :class="t.isDone ? 'bg-green-200/40' : 'bg-amber-200/40'">
        <p class="text-black/70">{{ t.title }}</p>
        <p class="text-sm text-black/60">{{ t.description }}</p>
        <p class="text-sm text-black/60">{{ t.isDone ? 'done' : 'not done' }}</p>
        <div class="flex flex-row text-sm text-black/70 gap-2">
          <button class="hover:underline">Delete</button>
          <button class="hover:underline">Edit</button>
          <button class="hover:underline">Mark as done</button>
        </div>
      </span>
    </div>
  </div>
</template>
