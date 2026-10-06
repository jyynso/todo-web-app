<script setup lang="ts">
import { ref } from 'vue';
import AddTaskModal from './AddTaskModal.vue';

type Filter = 'all' | 'done' | 'not done';

const emit = defineEmits<{
  search: [id: number | null]
  saved: []
}>();

const search = ref<number | null>(null);
const showModal = ref(false);
const filter = defineModel<Filter>({ default: 'all' });

function handleSearch() {
  emit('search', typeof search.value === 'number' ? search.value : null)
}

function setFilter(f: Filter) {
  search.value = null;
  emit('search', null);
  filter.value = f;
}
</script>

<template>
  <AddTaskModal :open="showModal" @close="showModal = false" @saved="emit('saved')" />
  <div class="flex w-full flex-col items-center gap-8">
    <div class="flex w-full flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
      <h1 class="font-semibold text-black/80">ToDo</h1>

      <div class="flex flex-wrap items-center gap-x-4 gap-y-2">
        <button @click="showModal = true" class="hover:underline cursor-pointer text-sm text-black/50">Add Task</button>
        <p class="hidden sm:block">|</p>

        <form @submit.prevent="handleSearch" class="flex items-center gap-2 w-full sm:w-auto">
          <input
            type="number"
            v-model.number="search"
            class="text-sm outline-none underline min-w-0 flex-1 sm:w-26 sm:flex-none"
            placeholder="Search By Id..."/>
          <button type="submit" class="text-black/60 cursor-pointer hover:underline text-sm">Search</button>
        </form>
        <p class="hidden sm:block">|</p>

        <div class="flex items-center gap-4">
          <h3 class="text-sm text-black/70">Filter By:</h3>
          <button @click="setFilter('all')" class="hover:underline cursor-pointer text-sm text-black/60">All</button>
          <button @click="setFilter('done')" class="hover:underline cursor-pointer text-sm text-black/60">Done</button>
          <button @click="setFilter('not done')" class="hover:underline cursor-pointer text-sm text-black/60">Not Done</button>
        </div>
      </div>
    </div>
  </div>
</template>
