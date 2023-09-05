const { createApp, ref, computed } = Vue

createApp({
    setup() {
        const message = ref('Admin Panel')
        const price = ref(0)
        const showPrice = ref(true)
        const loading = ref(false)

        const formatPrice = computed(() => {
            return "$" + price.value;
        })

        function togglePrice() {
            showPrice.value = !showPrice.value;
        }

        function getProducts() {
            loading.value = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    loading.value = false;
                })
        }


        return {
            message,
            price,
            formatPrice,
            showPrice,
            togglePrice,
            getProducts
        }
    }
}).mount('#app')
