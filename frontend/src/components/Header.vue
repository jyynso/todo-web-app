<script setup lang="ts">
import { ref } from 'vue';
import AddTaskModal from './AddTaskModal.vue';

type Filter = 'all' | 'done' | 'not done';

const search = ref<number | null>(null);
const showModal = ref(false);
const filter = defineModel<Filter>({ default: 'all' });

function handleSearch(e: Event) {
  const target = e.target as HTMLInputElement;
  search.value = target.value ? Number(target.value) : null;
}
</script>

<template>
<AddTaskModal :open="showModal" @close="showModal = false" />
  <div class="flex w-full flex-col items-center gap-8">
    <div class="flex w-full flex-row items-center justify-between">
      <div class="flex items-center gap-4">
         <h1 class="text-black/80">ToDo</h1>
      </div>

      <div class="flex items-center gap-4">
        <button @click="showModal = true" class="hover:underline cursor-pointer text-sm text-black/50">Add task</button>
        <p>|</p>
        <form @submit.prevent="handleSearch" class="flex items-center gap-2">
          <input
            type="text"
            v-model="search"
            class="text-sm outline-none underline w-20"
            placeholder="search by id..."
          />
          <button type="submit" class="text-black/60 cursor-pointer hover:underline text-sm">search</button>
        </form>
        <p>|</p>
        <h3 class="text-sm text-black/70">filter by:</h3>
        <button @click="filter = 'all'" class="hover:underline cursor-pointer text-sm text-black/60">all</button>
        <button @click="filter = 'done'" class="hover:underline cursor-pointer text-sm text-black/60">done</button>
        <button @click="filter = 'not done'" class="hover:underline cursor-pointer text-sm text-black/60">not done</button>
      </div>
    </div>
  </div>
</template>
