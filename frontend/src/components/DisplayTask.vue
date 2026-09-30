<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import type { Task } from '../types/Task';

const props = defineProps<{ filter: 'all' | 'done' | 'not done' }>();
const API = 'https://localhost:7290/api/TodoItems';
const task = ref<Task[]>([]);

const filteredTask = computed(() => {
  if (props.filter === 'all') return task.value;
  if (props.filter === 'done') return task.value.filter(t => t.isCompleted);
  if (props.filter === 'not done') return task.value.filter(t => !t.isCompleted);
  return task.value;
});

async function loadTasks() {
  const response = await fetch(API, {
    headers: { 'X-API-KEY': 'apikey1234' },
  });
  if (!response.ok) {
    throw new Error('Failed to load tasks');
  }
  const data = await response.json();
  task.value = data;
}

onMounted(loadTasks);
</script>

<template>
  <div v-if="filteredTask && filteredTask.length > 0" class="flex w-full flex-col gap-2">
    <div class="grid w-full grid-cols-[repeat(auto-fit,minmax(12rem,16rem))] justify-center gap-4">
      <span v-for="t in filteredTask" :key="t.id" class="flex flex-col gap-2 p-8 drop-shadow-sm" :class="t.isCompleted ? 'bg-[#D8F0B6]' : 'bg-[#FAF2C3]'">
        <p class="text-black/70">{{ t.title }}</p>
        <p class="text-xs text-black/60">Task Id: {{ t.id }}</p>
        <span class="w-full border-t border-black/40" />
        <p class="text-sm text-black/60">{{ t.description }}</p>
        <p class="text-sm text-black/60">Due @ {{ t.dueDate }}</p>
        <span class="w-full border-t border-black/40" />
        <p class="text-sm text-black/60">Completed: {{ t.isCompleted ? 'yes' : 'no' }}</p>
        <span class="w-full border-t border-black/40" />
        <div class="flex flex-row text-sm text-black/70 gap-2">
          <button class="hover:underline">Edit</button>
          <button class="hover:underline">Delete</button>
          <button class="hover:underline">Mark as done</button>
        </div>
      </span>
    </div>
  </div>
  <div v-else class="flex justify-center">
    <p class="text-black/70">No tasks to show.</p>
  </div>
</template>
