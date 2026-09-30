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
    <div class="flex w-full flex-row items-center justify-between">
      <div class="flex items-center gap-4">
         <h1 class="text-black/80">ToDo</h1>
      </div>

      <div class="flex items-center gap-4">
        <button @click="showModal = true" class="hover:underline cursor-pointer text-sm text-black/50">Add task</button>
        <p>|</p>
        <form @submit.prevent="handleSearch" class="flex items-center gap-2">
          <input
            type="number"
            v-model.number="search"
            class="text-sm   outline-none underline w-26"
            placeholder="search by id..."/>
          <button type="submit" class="text-black/60 cursor-pointer hover:underline text-sm">search</button>
        </form>
        <p>|</p>
        <h3 class="text-sm text-black/70">filter by:</h3>
        <button @click="setFilter('all')" class="hover:underline cursor-pointer text-sm text-black/60">all</button>
        <button @click="setFilter('done')" class="hover:underline cursor-pointer text-sm text-black/60">done</button>
        <button @click="setFilter('not done')" class="hover:underline cursor-pointer text-sm text-black/60">not done</button>
      </div>
    </div>
  </div>
</template>
