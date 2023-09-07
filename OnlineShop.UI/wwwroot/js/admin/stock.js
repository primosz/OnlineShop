const { createApp, ref } = Vue

createApp({
    setup() {
        const products = ref([]);
        const selectedProduct = ref(null)
        const loading = ref(false);
        const newStock = ref({
            productId: 0,
            description: "Size",
            qty: 11
        })

        function getStock() {
            loading.value = true;
            axios
                .get('/Admin/stock')
                .then((res) => {
                    console.log(res);
                    products.value = res.data;
                })
                .catch((err) => {
                    console.error(err);
                })
                .then(() => {
                    loading.value = false;
                });
        }

        function updateStock() {
            loading.value = true;
            axios
                .put('/Admin/stock', {
                    stock: selectedProduct.value.stock.map(x => {
                        return {
                            id: x.id,
                            description: x.description,
                            quantity: x.quantity,
                            productId: selectedProduct.value.id
                        };
                    })
                })
                .then((res) => {
                    console.log(res);
                })
                .catch((err) => {
                    console.error(err);
                })
                .then(() => {
                    loading.value = false;
                });
        }

        function deleteStock(id, index) {
            loading.value = true;
            axios
                .delete('/Admin/stock/' + id)
                .then((res) => {
                    console.log(res);
                    selectedProduct.value.stock.splice(index, 1);
                })
                .catch((err) => {
                    console.error(err);
                })
                .then(() => {
                    loading.value = false;
                });
        }

        function addStock() {
            loading.value = true;
            axios
                .post('/Admin/stock', newStock.value)
                .then((res) => {
                    console.log(res);
                    selectedProduct.value.stock.push(res.data)
                })
                .catch((err) => {
                    console.error(err);
                })
                .then(() => {
                    loading.value = false;
                });
        }

        function selectProduct(product) {
            selectedProduct.value = product;
            newStock.value.productId = product.id;
        }


        return {
            loading,
            products,
            newStock,
            getStock,
            selectProduct,
            selectedProduct,
            addStock,
            updateStock,
            deleteStock
        };
    },
    mounted() {
        console.log('Mounted stock');
        this.getStock();
    },
})
    .mount('#app');
