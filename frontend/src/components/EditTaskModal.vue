<script setup lang="ts">
import { ref, watch, nextTick } from 'vue'
import type { Task } from '../types/Task'

const API = 'https://localhost:7290/api/TodoItems'
const headers = { 'X-API-KEY': 'apikey1234' }

const props = defineProps<{ open: boolean; task: Task | null }>()
const emit = defineEmits<{
  close: []
  saved: []
}>()

const title = ref('')
const description = ref('')
const dueDate = ref('')
const titleInput = ref<HTMLInputElement>(null)

watch(() => props.open, async (isOpen) => {
  if (isOpen && props.task) {
    title.value = props.task.title
    description.value = props.task.description
    dueDate.value = props.task.dueDate
    await nextTick()
    titleInput.value?.focus()
  }
})

async function save() {
  if (!props.task || !title.value.trim()) return
  try {
    const res = await fetch(`${API}/${props.task.id}`, {
      method: 'PUT',
      headers: { ...headers, 'Content-Type': 'application/json' },
      body: JSON.stringify({
        ...props.task,
        title: title.value.trim(),
        description: description.value,
        dueDate: dueDate.value || null,
      }),
    })
    if (!res.ok) {
      console.error('Failed to update task', res.status)
      return
    }
    emit('saved')
    emit('close')
  } catch (err) {
    console.error('Failed to update task', err)
  }
}
</script>

<template>
  <Teleport to="body">
    <div
      v-if="open"
      class="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-xs"
      @click.self="emit('close')"
    >
      <form
        :class="task?.isCompleted ? 'bg-[#D3E0C3]' : 'bg-[#FAF2C3]'"
        class="flex flex-col gap-2 p-8 drop-shadow-sm w-full max-w-md"
        @submit.prevent="save"
        @keydown.esc="emit('close')">
        <input
          v-model="title"
          autofocus
          placeholder="Title"
          class="bg-transparent text-black/70 outline-none placeholder:text-black/40"/>
        <span class="w-full border-t border-black/40" />
        <textarea
          v-model="description"
          rows="3"
          placeholder="Description"
          class="bg-transparent text-sm text-black/60 outline-none resize-none placeholder:text-black/40"/>
        <span class="w-full border-t border-black/40" />
        <textarea
          v-model="dueDate"
          rows="2"
          placeholder="Due date"
          class="bg-transparent text-sm text-black/60 outline-none resize-none placeholder:text-black/40"/>
        <span class="w-full border-t border-black/40" />
        <div class="flex flex-row text-sm text-black/70 gap-2">
          <button type="submit" class="hover:underline">Save</button>
          <button type="button" class="hover:underline ml-auto" @click="emit('close')">Cancel</button>
        </div>
      </form>
    </div>
  </Teleport>
</template>
