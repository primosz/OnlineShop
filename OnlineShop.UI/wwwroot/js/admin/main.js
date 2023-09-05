const { createApp, ref, computed } = Vue

createApp({
    setup() {
        const message = ref('Admin Panel')
        const price = ref(0)

        const formatPrice = computed(() => {
            return "$" + price.value;
        })

        return {
            message,
            price,
            formatPrice
        }
    }
}).mount('#app')
