  <script setup lang="ts">
  import { ref, watch, nextTick } from 'vue'
  import { HandleError } from '@/composables/HandleError'
  import api from '@/services/api';

  const props = defineProps<{ open: boolean }>()
  const emit = defineEmits<{
    close: []
    saved: []
  }>()

  const title = ref('')
  const description = ref('')
  const dueDate = ref('')
  const titleInput = ref<HTMLInputElement | null>(null)
  const today = new Date().toLocaleDateString('en-CA')
  const { message, handleError, clearError } = HandleError()

  watch(() => props.open, async (isOpen) => {
    if (isOpen) {
      clearError()
      title.value = ''
      description.value = ''
      dueDate.value = ''
      await nextTick()
      titleInput.value?.focus()
    }
  })

  async function save() {
    clearError()
    if (!title.value.trim()) {
      message.value = 'Title is required'
      return
    }
    try {
      await api.post('', {
        title: title.value.trim(),
        description: description.value,
        dueDate: dueDate.value || null,
        isCompleted: false,
      })
      emit('saved')
      emit('close')
      location.reload()
    } catch (err) {
      handleError(err, 'Failed to add task')
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
          class="flex flex-col gap-2 p-8 m-6 drop-shadow-sm bg-[#FAF2C3] w-full max-w-md"
          @submit.prevent="save"
          @keydown.esc="emit('close')">
          <p v-if="message" class="text-red-500">{{ message }}</p>
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
          <input
            v-model="dueDate"
            type="date"
            :min="today"
            required
            class="bg-transparent text-sm text-black/60 outline-none placeholder:text-black/40"/>
          <span class="w-full border-t border-black/40" />
          <div class="flex flex-row text-sm text-black/70 gap-2">
            <button type="submit" class="hover:underline">Save</button>
            <button type="button" class="hover:underline ml-auto" @click="emit('close')">Cancel</button>
          </div>
        </form>
      </div>
    </Teleport>
  </template>
