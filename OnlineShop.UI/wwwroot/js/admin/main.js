const { createApp, ref, computed } = Vue

createApp({
    setup() {
        const message = ref('Admin Panel')
        const loading = ref(false)
        const productModel = ref({
            name: "ProductName",
            description: "ProductDescription",
            value: 6.66
        })
        const products = ref([])


        function getProducts() {
            loading.value = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                    products.value = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    loading.value = false;
                })
        }

        function createProduct() {
            loading.value = true;
            axios.post('/Admin/products', productModel.value)
                .then(res => {
                    console.log(res.data);
                    products.value.push(res.data);
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
            productModel,
            getProducts,
            createProduct,
            products
        }
    }
}).mount('#app')
