import axios from 'axios'
import { ref } from 'vue'

export function HandleError() {
  const message = ref('')

  function handleError(err: unknown, fallback: string) {
    if (axios.isAxiosError(err) && err.response?.status === 404) {
      message.value = err.response.data
    } else {
      message.value = fallback
    }
    console.error(fallback, err)
  }

  function clearError() {
    message.value = ''
  }

  return { message, handleError, clearError }
}
