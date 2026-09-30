<script setup lang="ts">
defineProps<{ open: boolean; message?: string }>()
const emit = defineEmits<{
  confirmed: []
  canceled: []
}>()
</script>

<template>
  <Teleport to="body">
    <div
      v-if="open"
      class="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-xs"
      @click.self="emit('canceled')"
      @keydown.esc="emit('canceled')">
      <div class="flex flex-col gap-2 p-8 drop-shadow-sm bg-white w-full max-w-sm">
        <p class="text-black/70">{{ message ?? 'Are you sure?' }}</p>
        <span class="w-full border-t border-black/40" />
        <div class="flex flex-row text-sm text-black/70 gap-2">
          <button class="hover:underline text-red-500" @click="emit('confirmed')">Delete</button>
          <button class="hover:underline ml-auto" @click="emit('canceled')">Cancel</button>
        </div>
      </div>
    </div>
  </Teleport>
</template>
