<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import type { Task } from '../types/Task';
import EditTaskModal from './EditTaskModal.vue';
import ConfirmationModal from './ConfirmationModal.vue';
import api from '@/services/api';

const props = defineProps<{ filter: 'all' | 'done' | 'not done'; searchId: number | null }>();


const task = ref<Task[]>([]);
const taskToDelete = ref<Task | null>(null);
const editModalOpen = ref(false);
const editing = ref<Task | null>(null);
const confirmModalOpen = ref(false);


const filteredTask = computed(() => {
  if (props.searchId !== null) {
    return task.value.filter(t => t.id === props.searchId);
  }
  if (props.filter === 'done') return task.value.filter(t => t.isCompleted);
  if (props.filter === 'not done') return task.value.filter(t => !t.isCompleted);
  return task.value;
});

async function loadTasks() {
  try {
    const response = await api.get<Task[]>('');
    task.value = response.data
  } catch (err) {
    console.error('Failed to load tasks', err)
  }
}

async function updateTaskStatus(t: Task) {
  try {
    await api.put(`/${t.id}`, { ...t, isCompleted: !t.isCompleted, });
    await loadTasks();
  } catch (err) {
    console.error('Failed to update task', err)
  }
}

function openEditModal(t: Task) {
  editing.value = t
  editModalOpen.value = true
}

function confirmDelete(t: Task) {
  taskToDelete.value = t
  confirmModalOpen.value = true
}

async function deleteTask() {
  const t = taskToDelete.value
  if (!t) return
  try {
    await api.delete(`/${t.id}`);
    await loadTasks();
    confirmModalOpen.value = false
    taskToDelete.value = null
  } catch (err) {
    console.error('Failed to delete task', err)
  }
}

onMounted(loadTasks);
</script>

<template>
  <EditTaskModal
    :open="editModalOpen"
    :task="editing"
    @saved="loadTasks"
    @close="editModalOpen = false" />
  <ConfirmationModal
    :open="confirmModalOpen"
    :message="taskToDelete?.title ? `Are you sure you want to delete ${taskToDelete.title}?` : undefined"
    @confirmed="deleteTask"
    @canceled="confirmModalOpen = false" />
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
          <button @click="openEditModal(t)" class="hover:underline">Edit</button>
          <button @click="confirmDelete(t)" class="hover:underline">Delete</button>
          <button @click="updateTaskStatus(t)" class="hover:underline">
            {{ t.isCompleted ? 'Mark as not done' : 'Mark as done' }}
          </button>
        </div>
      </span>
    </div>
  </div>
  <div v-else class="flex justify-center">
    <p class="text-black/70">No tasks to show.</p>
  </div>
</template>
