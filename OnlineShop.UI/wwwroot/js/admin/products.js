const { createApp, ref } = Vue

createApp({
    setup() {
        const editing = ref(false);
        const message = ref('Admin Panel');
        const loading = ref(false);
        const objectIndex = ref(0);
        const productModel = ref({
            id: 0,
            name: 'ProductName',
            description: 'ProductDescription',
            value: 6.66,
        });
        const products = ref([]);

        function getProduct(id) {
            loading.value = true;
            axios
                .get('/Admin/products/' + id)
                .then((res) => {
                    console.log(res);
                    const product = res.data;
                    productModel.value = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value,
                    };
                })
                .catch((err) => {
                    console.error(err);
                })
                .then(() => {
                    loading.value = false;
                });
        }

        function getProducts() {
            loading.value = true;
            axios
                .get('/Admin/products')
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

        function createProduct() {
            loading.value = true;
            axios
                .post('/Admin/products', productModel.value)
                .then((res) => {
                    console.log(res.data);
                    products.value.push(res.data);
                })
                .catch((err) => {
                    console.error(err);
                })
                .then(() => {
                    loading.value = false;
                    editing.value = false;
                });
        }

        function updateProduct() {
            loading.value = true;
            axios
                .put('/Admin/products', productModel.value)
                .then((res) => {
                    console.log('Updating object index: ' + objectIndex.value);
                    products.value.splice(objectIndex.value, 1, res.data);
                })
                .catch((err) => {
                    console.error(err);
                })
                .then(() => {
                    loading.value = false;
                    editing.value = false;
                });
        }

        function deleteProduct(id, index) {
            loading.value = true;
            axios
                .delete('/Admin/products/' + id)
                .then((res) => {
                    console.log(res);
                    products.value.splice(index, 1);
                })
                .catch((err) => {
                    console.error(err);
                })
                .then(() => {
                    loading.value = false;
                });
        }

        function newProduct() {
            editing.value = true;
            productModel.value.id = 0;
        }

        function editProduct(id, index) {
            objectIndex.value = index;
            getProduct(id);
            editing.value = true;
        }

        function cancel() {
            editing.value = false;
        }

        return {
            message,
            editing,
            objectIndex,
            productModel,
            getProduct,
            getProducts,
            createProduct,
            editProduct,
            updateProduct,
            deleteProduct,
            products,
            newProduct,
            cancel,
            loading,
        };
    },
    mounted() {
        console.log('Mounted');
        this.getProducts();
    },
})
    .mount('#app');
