<script setup lang="ts">
import { ref, watch, nextTick } from 'vue'

const API = 'https://localhost:7290/api/TodoItems'
const headers = { 'X-API-KEY': 'apikey1234' }

const props = defineProps<{ open: boolean }>()
const emit = defineEmits<{
  close: []
  saved: []
}>()

const title = ref('')
const description = ref('')
const dueDate = ref('')
const titleInput = ref<HTMLInputElement | null>(null)

watch(() => props.open, async (isOpen) => {
  if (isOpen) {
    title.value = ''
    description.value = ''
    dueDate.value = ''
    await nextTick()
    titleInput.value?.focus()
  }
})

async function save() {
  if (!title.value.trim()) return
  try {
    const res = await fetch(API, {
      method: 'POST',
      headers: { ...headers, 'Content-Type': 'application/json' },
      body: JSON.stringify({
        title: title.value.trim(),
        description: description.value,
        dueDate: dueDate.value || null,
        isCompleted: false,
      }),
    })
    if (!res.ok) {
      console.error('Failed to add task', res.status)
      return
    }
    emit('saved')
    emit('close')
    location.reload()
  } catch (err) {
    console.error('Failed to add task', err)
  }
}
</script>

<template>
  <Teleport to="body">
    <div
      v-if="open"
      class="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-xs"
      @click.self="emit('close')">
      <form
        class="flex flex-col gap-2 p-8 drop-shadow-sm bg-[#FAF2C3] w-full max-w-md"
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
