<script setup lang="ts">
import { ref } from 'vue';
import AddTaskModal from './AddTaskModal.vue';

const showModal = ref(false);
</script>

<template>
<AddTaskModal :open="showModal" @close="showModal = false" />
  <div class="flex w-full flex-col items-center gap-8">
    <div class="flex w-full flex-row items-center justify-between">
      <div class="flex items-center gap-4">
        <!-- <h1 class="text-lg text-black/80">Tasks</h1> -->
        <button @click="showModal = true" class="hover:underline cursor-pointer text-sm text-black/50">Add Task</button>
      </div>
      <div class="flex items-center gap-4 text-black/50">
        <h3 class="text-sm text-black/70">filter by:</h3>
        <button class="hover:underline cursor-pointer text-sm">done</button>
        <button class="hover:underline cursor-pointer text-sm">not done</button>
      </div>
    </div>
  </div>
</template>
